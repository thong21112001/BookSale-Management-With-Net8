using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookSale.Management.UI.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index(string returnUrl)
        {
            var isAuthenticated = User.Identity?.IsAuthenticated ?? false;

            if (isAuthenticated)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            }
            else
            {
                return RedirectToAction("Login", "Authentication", new {ReturnUrl = returnUrl});
            }

            return View();
        }
    }
}
