using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs.Book;
using BookSale.Management.Application.DTOs.Checkout;
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
        private bool _isAuthenticated;

        public CheckoutController(IUserAddressService userAddressService, IBookService bookService,
                                    ICartService cartService, IOrderService orderService,
                                    PaypalClientHelper paypalClient)
        {
            _userAddressService = userAddressService;
            _bookService = bookService;
            _cartService = cartService;
            _orderService = orderService;
            _paypalClient = paypalClient;
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

                //Lưu data đơn hàng
                string codeOrder = $"ORDER_${DateTime.Now.ToString("ddMMyyyyhhmmss")}";
                var booksInCart = await GetCartFromSessionAsync();
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

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
                        Id = userCheckout.PaymentMethod == PaymentMethod.Paypal ? orderID : Guid.NewGuid().ToString(),
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

        public IActionResult PaymentSuccess()
        {
            return View();
        }

        // Hàm chuyển đổi từ VND sang USD
        private double ConvertVNDToUSD(double vndAmount)
        {
            decimal exchangeRate = 0.000043m; // Ví dụ: tỷ giá hối đoái cố định
            decimal usdAmount = (decimal)vndAmount * exchangeRate;
            double doubleResult = (double)Math.Floor(usdAmount) + 1;
            return doubleResult;
        }
        #endregion


        #region Các hàm xử lý
        //Lấy giá tiền của book
        private async Task<double> GetBookPriceAsync(string codeBook)
        {
            var book = await _bookService.GetBookByCode(codeBook);
            return Convert.ToDouble(book.Price);
        }

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
