using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs.AuthenticationUser;
using BookSale.Management.Domain.Entities;
using BookSale.Management.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IErrorMessageService _errorMessageService;

        public AuthenticationController(IAuthenticationService authenticationService, IUserService userService,
                                        UserManager<ApplicationUser> userManager, IErrorMessageService errorMessageService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
            _userManager = userManager;
            _errorMessageService = errorMessageService;
        }

        [HttpGet]//Default
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Thêm cái này ngoài form Login asp-antiforgery="true"
        public async Task<IActionResult> Login(UserLoginModel userLoginModel)
        {
            //Coi thử có thoả điều kiện trong LoginModel không, nếu không trả về false
            if (!ModelState.IsValid)
            {
                //Lấy tất cả lỗi ở trong ModelState trả về dưới dạng ToList

                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                TempData["Errors"] = string.Join("<br/>", errors);

                return View();
            }

            var result = await _authenticationService.CheckLogin(userLoginModel.Username, userLoginModel.Password, userLoginModel.HasRememberMe);

            if (result.Status)
            {
                string returnUrl = userLoginModel.ReturnUrl;
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1
                    && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//")
                    &&!returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            TempData["Errors"] = result.Message;

            return View(userLoginModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _authenticationService.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisterDTO requestRegister)
        {
            // Kiểm tra số điện thoại
            if (requestRegister.Phone != null)
            {
                if (!_errorMessageService.IsValidPhoneNumber(requestRegister.Phone))
                {
                    ModelState.AddModelError(nameof(requestRegister.Phone), _errorMessageService.GetErrorMessage("InvalidPhoneNumber"));
                }
            }

            // Kiểm tra email và username
            var user = new ApplicationUser
            {
                UserName = requestRegister.Username,
                Email = requestRegister.Email,
            };
            var userValidator = new UserValidator<ApplicationUser>();
            var userValidationResult = await userValidator.ValidateAsync(_userManager, user);

            // Kiểm tra mật khẩu
            var passwordValidator = new PasswordValidator<ApplicationUser>();
            var passwordValidationResult = await passwordValidator.ValidateAsync(_userManager, null, requestRegister.Password);

            if (!userValidationResult.Succeeded || !passwordValidationResult.Succeeded)
            {
                foreach (var error in userValidationResult.Errors)
                {
                    string errorMessage = _errorMessageService.GetErrorMessage(error.Code);
                    if (errorMessage != null)
                    {
                        ModelState.AddModelError(
                            error.Code.Contains("Email") ? nameof(requestRegister.Email) :
                            error.Code.Contains("Username") ? nameof(requestRegister.Username) :
                            string.Empty, errorMessage);
                    }
                    else
                    {
                        ModelState.AddModelError(
                            error.Code.Contains("Email") ? nameof(requestRegister.Email) :
                            error.Code.Contains("Username") ? nameof(requestRegister.Username) :
                            string.Empty, error.Description);
                    }
                }

                foreach (var error in passwordValidationResult.Errors)
                {
                    string errorMessage = _errorMessageService.GetErrorMessage(error.Code);
                    if (errorMessage != null)
                    {
                        ModelState.AddModelError(nameof(requestRegister.Password), errorMessage);
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(requestRegister.Password), error.Description);
                    }
                }

                TempData["Errors"] = string.Join("<br/>", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                return View(requestRegister);
            }

            if (ModelState.IsValid)
            {
                bool response = await _userService.RegisterAsync(requestRegister);

                TempData["Errors"] = response
                    ? "ĐĂNG KÝ THÀNH CÔNG, QUAY LẠI ĐĂNG NHẬP !"
                    : "CÓ LỖI XẢY RA VUI LÒNG CHẠY LẠI WEB !!!";

                return View();
            }

            TempData["Errors"] = string.Join("<br/>", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            return View();
        }
    }
}
