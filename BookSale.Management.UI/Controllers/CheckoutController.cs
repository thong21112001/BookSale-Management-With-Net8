using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs.Book;
using BookSale.Management.Application.DTOs.Checkout;
using BookSale.Management.Domain.Enums;
using BookSale.Management.UI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace BookSale.Management.UI.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IUserAddressService _userAddressService;
        private readonly IBookService _bookService;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private bool _isAuthenticated;

        public CheckoutController(IUserAddressService userAddressService, IBookService bookService,
                                    ICartService cartService, IOrderService orderService)
        {
            _userAddressService = userAddressService;
            _bookService = bookService;
            _cartService = cartService;
            _orderService = orderService;
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

                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Shop");
                }
            }
            return RedirectToAction("Login", "Authentication", new {ReturnUrl = returnUrl});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompleteCart(UserCheckoutDTO userCheckoutDTO)
        {
            string codeOrder = $"ORDER_${DateTime.Now.ToString("ddMMyyyyhhmmss")}";

            if (ModelState.IsValid)
            {
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
                        Id = userCheckoutDTO.PaymentMethod == PaymentMethod.Paypal ? userCheckoutDTO.OrderId : Guid.NewGuid().ToString(),
                        Code = codeOrder,
                        CreatedOn = DateTime.Now,
                        Status = StatusProcessing.New,
                        PaymentMethod = userCheckoutDTO.PaymentMethod,
                        UserId = userId,
                        BookForCarts = booksInCart.ToList(),
                        TotalAmount = userCheckoutDTO.TotalAmount
                    };

                    await _orderService.Save(order);

                    ViewBag.OrderCode = codeOrder;
                }
            }

            return View();
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

    }
}
