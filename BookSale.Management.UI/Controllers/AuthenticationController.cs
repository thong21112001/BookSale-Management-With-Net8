using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpGet]//Default
        public IActionResult Login()
        {
            return View();
        }
    }
}
