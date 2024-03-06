using BookSale.Management.Application.Abstracts;
using BookSale.Management.UI.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Areas.Admin.Controllers
{
    //domain/Admin/Authentication/{Action}
    [Area("Admin")]
    [AllowAnonymous]    //Ai vao cx dc ko can chung thuc
	public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
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
            if (!ModelState.IsValid)
            {
                //Lấy tất cả lỗi ở trong ModelState trả về dưới dạng ToList

                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                TempData["Errors"] = string.Join("<br/>", errors);

                return View();
            }

            var result = await _authenticationService.CheckLogin(loginModel.Username, loginModel.Password, loginModel.HasRememberMe);

            if (result.Status) //true
                return RedirectToAction("Index", "Home");

            TempData["Errors"] = result.Message;

            return View(loginModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _authenticationService.SignOut();

            return RedirectToAction("Login", "Authentication");
        }
    }
}
