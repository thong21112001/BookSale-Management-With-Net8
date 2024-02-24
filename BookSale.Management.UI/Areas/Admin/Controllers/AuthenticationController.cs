using BookSale.Management.UI.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Areas.Admin.Controllers
{
    //domain/Admin/Authentication/{Action}
    [Area("Admin")]
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]//Default
        public IActionResult Login()
        {
            var mdLogin = new LoginModel();
            return View(mdLogin);
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            return View();
        }
    }
}
