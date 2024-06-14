using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs.AuthenticationUser;
using BookSale.Management.Application.DTOs.ViewModels;
using BookSale.Management.Domain.Entities;
using BookSale.Management.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookSale.Management.UI.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IErrorMessageService _errorMessageService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserStore<ApplicationUser> _userStore;

        public AuthenticationController(IAuthenticationService authenticationService, IUserService userService,
                                        UserManager<ApplicationUser> userManager, IErrorMessageService errorMessageService,
                                        SignInManager<ApplicationUser> signInManager, IUserStore<ApplicationUser> userStore)
        {
            _authenticationService = authenticationService;
            _userService = userService;
            _userManager = userManager;
            _errorMessageService = errorMessageService;
            _signInManager = signInManager;
            _userStore = userStore;
        }

        #region Login Function
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
        #endregion


        #region Logout Function
        public async Task<IActionResult> Logout()
        {
            await _authenticationService.SignOut();

            return RedirectToAction("Index", "Home");
        }
        #endregion


        #region Register Function
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
        #endregion


        #region Login With Google
        public IActionResult External(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = $"/authentication/callbackexternal?handler=callback&returnUrl={returnUrl}&remoteError=";
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> CallBackExternal(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (remoteError != null)
            {
                //ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                //ErrorMessage = "Error loading external login information.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                //_logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);
                return LocalRedirect(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }
            else
            {
                string email = string.Empty;
                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    email = info.Principal.FindFirstValue(ClaimTypes.Email);
                }

                var md = new ExternalLoginModel
                {
                    Provider = info.ProviderDisplayName,
                    Email = email
                };
                return View(md);
            }
        }

        public async Task<IActionResult> ConfirmationExternal(ExternalLoginModel externalLogin)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToPage("./Login", new { ReturnUrl = externalLogin.ReturnUrl });
            }

            if (ModelState.IsValid)
            {
                // Kiểm tra xem email đã tồn tại chưa
                var existingUser = await _userManager.FindByEmailAsync(externalLogin.Email);
                if (existingUser != null)
                {
                    // Xử lý trường hợp email đã tồn tại
                    ModelState.AddModelError(string.Empty, "Email này đã được sử dụng. Vui lòng sử dụng email khác.");
                    TempData["Errors"] = string.Join("<br/>", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    return View(externalLogin);
                }

                var user = new ApplicationUser
                {
                    Fullname = "",
                    Email = externalLogin.Email,
                    IsActive = true,
                    PhoneNumber = "",
                    MobilePhone = "",
                    Address = "",
                };

                await _userStore.SetUserNameAsync(user, externalLogin.Email, CancellationToken.None);

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false, info.LoginProvider);
                        return RedirectToAction("Index", "Home");
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                TempData["Errors"] = string.Join("<br/>", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                return View(externalLogin);
            }

            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
