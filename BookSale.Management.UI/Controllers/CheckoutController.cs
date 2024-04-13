using BookSale.Management.Application.Abstracts;
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
        private bool _isAuthenticated;

        public CheckoutController(IUserAddressService userAddressService, IBookService bookService)
        {
            _userAddressService = userAddressService;
            _bookService = bookService;
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

                    return View(books);
                }
                else
                {
                    return RedirectToAction("Index", "Shop");
                }
            }
            return RedirectToAction("Login", "Authentication", new {ReturnUrl = returnUrl});
        }
    }
}
