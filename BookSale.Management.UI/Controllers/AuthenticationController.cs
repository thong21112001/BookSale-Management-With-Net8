﻿using BookSale.Management.Application.Abstracts;
using BookSale.Management.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
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
    }
}
