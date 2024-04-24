using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs.Book;
using BookSale.Management.Application.DTOs.Checkout;
using BookSale.Management.Application.DTOs.VnPay;
using BookSale.Management.Domain.Enums;
using BookSale.Management.UI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Security.Claims;

namespace BookSale.Management.UI.Controllers
{
	public class CheckoutController : Controller
    {
        private readonly IUserAddressService _userAddressService;
        private readonly IBookService _bookService;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly PaypalClientHelper _paypalClient;
        private readonly IVnPayService _vnPayService;
        private bool _isAuthenticated;

        public CheckoutController(IUserAddressService userAddressService, IBookService bookService,
                                    ICartService cartService, IOrderService orderService,
                                    PaypalClientHelper paypalClient, IVnPayService vnPayService)
        {
            _userAddressService = userAddressService;
            _bookService = bookService;
            _cartService = cartService;
            _orderService = orderService;
            _paypalClient = paypalClient;
            _vnPayService = vnPayService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            _isAuthenticated = User?.Identity?.IsAuthenticated ?? false; // Mặc định không đăng nhập là false
        }

        public async  Task<IActionResult> Index(string returnUrl)
        {
            if (_isAuthenticated)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    var userId = userIdClaim.Value;
                    ViewData["ListAddess"] = await _userAddressService.GetAllListAddressUser(userId);
                }

                //Lấy giỏ hàng
                var carts = CartHelper.GetCartItems(HttpContext.Session);

                if (carts is not null && carts.Count() > 0)
                {
                    var getCodes = carts.Select(x => x.CodeBook).ToArray();

                    var books = await _bookService.GetListBookByCode(getCodes);

                    books = books.Select(book =>
                    {
                        var item = carts.FirstOrDefault(x => x.CodeBook == book.Code);

                        if (item is not null)
                        {
                            book.Quantity = item.Quantity;
                        }

                        return book;
                    });

                    ViewBag.ListBook = books;
                    ViewBag.PaypalAppId = _paypalClient.AppId;

                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Shop");
                }
            }
            return RedirectToAction("Login", "Authentication", new {ReturnUrl = returnUrl});
        }
       

        #region Paypal Method
        [HttpPost("/Checkout/create-paypal-order")]
        public async Task<IActionResult> CreatePaypalOrder(CancellationToken cancellationToken)
        {
            double cartTotal = 0;
            var carts = CartHelper.GetCartItems(HttpContext.Session);
            //Tính tổng giá tiền trong giỏ hàng
            foreach (var item in carts)
            {
                var bookPrice = await GetBookPriceAsync(item.CodeBook);
                cartTotal += item.Quantity * bookPrice;
            }

            var sumMoneyCartVND = cartTotal;
            var sumMoneyCartUSD = ConvertVNDToUSD(sumMoneyCartVND).ToString("0");

            var currencyUSD = "USD";
            var billOrder = "ORDER" + DateTime.Now.ToString("ddMMyyyyhhmmss");

            try
            {
                var response = await _paypalClient.CreateOrder(sumMoneyCartUSD, currencyUSD, billOrder);

                return Ok(response);
            }
            catch (Exception ex)
            {
                var error = new
                {
                    ex.GetBaseException().Message
                };

                return BadRequest(error);
            }
        }

        [HttpPost("/Checkout/capture-paypal-order")]
        public async Task<IActionResult> CapturePaypalOrder(string orderID, UserCheckoutDTO userCheckout, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _paypalClient.CaptureOrder(orderID);

                await SaveData(userCheckout, orderID);

                return RedirectToAction(nameof(PaymentSuccess), new { orderId = orderID });
			}
            catch (Exception ex)
            {
                var error = new
                {
                    ex.GetBaseException().Message
                };

                return BadRequest(error);
            }
        }
        #endregion



        #region VnPay Method
        [HttpPost]
        public IActionResult PaymentOrder(UserCheckoutDTO userCheckout)
        {
            if (userCheckout.PaymentMethod == PaymentMethod.VnPay)
            {
                var vnPayModel = new VnPaymentRequestModel
                {
                    OrderId = new Random().Next(1000,100000),
                    CreatedDate = DateTime.Now,
                    Amount = userCheckout.TotalAmount
                };

                // Lưu UserCheckoutDTO vào TempData
                TempData["UserCheckout"] = JsonConvert.SerializeObject(userCheckout);
                return Redirect(_vnPayService.CreatePaymentUrl(HttpContext, vnPayModel));
            }
            TempData["Fail"] = "Lỗi khi tiến hành thanh toán !!!";
            return RedirectToAction(nameof(PaymentFail));
        }
        #endregion


        #region Gọi về view sau khi thanh toán xong
        public async Task<IActionResult> PaymentSuccess(string orderId)
        {
			if (string.IsNullOrEmpty(orderId))
            {
                var response = _vnPayService.PaymentExcute(Request.Query);
                if (response == null || response.VnPayResponseCode != "00")
                {
                    TempData["Fail"] = "Lỗi thanh toán !!!";
                    return RedirectToAction(nameof(PaymentFail));
                }

                // Lấy UserCheckoutDTO từ TempData
                var userCheckoutJson = TempData["UserCheckout"]?.ToString();
                var userCheckout = userCheckoutJson != null ? JsonConvert.DeserializeObject<UserCheckoutDTO>(userCheckoutJson) : null;

                // Xử lý dữ liệu và lưu đơn hàng
                if (userCheckout != null)
                {
                    // Lưu đơn hàng vào data:
                    await SaveData(userCheckout, response.OrderId);
                }

                TempData["OrderId"] = response.OrderId;
                return View();
            }
            else //Dành cho paypal và cod
            {
				TempData["OrderId"] = orderId;
				return View();
            }
        }

        public IActionResult PaymentFail()
        {
            return View();
        }
        #endregion


        #region Các hàm xử lý
        //Tiến hành lưu giỏ hàng và order vào db
        private async Task SaveData(UserCheckoutDTO userCheckout,string orderID)
        {
            //Lưu data đơn hàng
            string codeOrder = $"ORDER_${DateTime.Now.ToString("ddMMyyyyhhmmss")}";
            var booksInCart = await GetCartFromSessionAsync();
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string getOrderId = string.Empty;

            if (userCheckout.PaymentMethod == PaymentMethod.Paypal || userCheckout.PaymentMethod == PaymentMethod.VnPay)
            {
                getOrderId = orderID;
            }
            else
            {
                getOrderId = Guid.NewGuid().ToString();
            }

            if (userIdClaim != null)
            {
                var userId = userIdClaim.Value;

                var cart = new CartRequestDTO
                {
                    Code = $"CART_${DateTime.Now.ToString("ddMMyyyyhhmmss")}",
                    CreatedOn = DateTime.Now,
                    Note = "Giao nhanh",
                    Status = StatusProcessing.New,
                    IsActive = true,
                    UserId = userId,
                    BookForCarts = booksInCart.ToList()
                };

                await _cartService.Save(cart);

                var order = new OrderRequestDTO
                {
                    Id = getOrderId,
                    Code = codeOrder,
                    CreatedOn = DateTime.Now,
                    Status = StatusProcessing.New,
                    PaymentMethod = userCheckout.PaymentMethod,
                    UserId = userId,
                    BookForCarts = booksInCart.ToList(),
                    TotalAmount = userCheckout.TotalAmount
                };

                await _orderService.Save(order);
            }
            //Xoá mặt hàng khỏi giỏ sau khi lưu toàn bộ vào data
            CartHelper.ClearCartSession(HttpContext.Session);
        }

        // Hàm chuyển đổi từ VND sang USD
        private double ConvertVNDToUSD(double vndAmount)
        {
            decimal exchangeRate = 0.000043m; // Ví dụ: tỷ giá hối đoái cố định
            decimal usdAmount = (decimal)vndAmount * exchangeRate;
            double doubleResult = (double)Math.Floor(usdAmount) + 1;
            return doubleResult;
        }

        //Lấy giá tiền của book
        private async Task<double> GetBookPriceAsync(string codeBook)
        {
            var book = await _bookService.GetBookByCode(codeBook);
            return Convert.ToDouble(book.Price);
        }

        //Lấy sách từ giỏ hàng
        public async Task<IEnumerable<BookForCart>> GetCartFromSessionAsync()
        {
            List<BookForCart> bookForCarts = new List<BookForCart>();

            var carts = CartHelper.GetCartItems(HttpContext.Session);

            if (carts is not null && carts.Count() > 0)
            {
                var getCodes = carts.Select(x => x.CodeBook).ToArray();

                var books = await _bookService.GetListBookByCode(getCodes);

                books = books.Select(book =>
                {
                    var item = carts.FirstOrDefault(x => x.CodeBook == book.Code);

                    if (item is not null)
                    {
                        book.Quantity = item.Quantity;
                    }

                    return book;
                });

                bookForCarts = books.ToList();
            }

            return bookForCarts;
        }
        #endregion
    }
}
