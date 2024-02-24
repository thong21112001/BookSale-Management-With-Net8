using BookSale.Management.Application.Abstracts;
using BookSale.Management.UI.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Areas.Admin.Controllers
{
    //domain/Admin/Authentication/{Action}
    [Area("Admin")]
    public class AuthenticationController : Controller
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

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
        [ValidateAntiForgeryToken] //Thêm cái này ngoài form Login asp-antiforgery="true"
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            //Coi thử có thoả điều kiện trong LoginModel không, nếu không trả về false
            if (ModelState.IsValid)
            {
               bool result = await _userService.CheckLogin(loginModel.Username,loginModel.Password);

                if (!result) //false
                {
                    ViewBag.Error = "Username hoặc Password không đúng :<";
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                //Lấy tất cả lỗi ở trong ModelState trả về dưới dạng ToList
                var errors = ModelState.Values.SelectMany(x=>x.Errors).Select(x=>x.ErrorMessage).ToList();

                ViewBag.Error = string.Join("<br/>", errors);
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _userService.SignOut();

            return RedirectToAction("Login", "Authentication");
        }
    }
}
