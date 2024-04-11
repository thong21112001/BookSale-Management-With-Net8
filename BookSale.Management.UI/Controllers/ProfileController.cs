using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace BookSale.Management.UI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserAddressService _userAddressService;
        private bool _isAuthenticated;

        public ProfileController(IUserService userService, IUserAddressService userAddressService)
        {
            _userService = userService;
            _userAddressService = userAddressService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            _isAuthenticated = User?.Identity?.IsAuthenticated ?? false; // Mặc định không đăng nhập là false
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (_isAuthenticated)//Đã đăng nhập
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    var userId = userIdClaim.Value;
                    // Use userId as needed
                    UserProfileViewModel userVM = await _userService.GetUserProfileViewModel(userId);//Trả về model chính của view
                    ViewData["UserProfileDTO"] = await _userService.GetUserProfileDTO(userId);//Trả về model phụ cho view

                    ViewBag.ListAddress = await _userAddressService.GetAllListAddressUser(userId);

                    return View(userVM);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }
        }

        //Cập nhập địa chỉ mới vào tài khoản
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(UserProfileViewModel userProfileVM)
        {
            if (_isAuthenticated)//Đã đăng nhập
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    var userId = userIdClaim.Value;
                    var result = await _userService.UpdateProfileUser(userProfileVM, userId);

                    if (result == true)
                    {
                        UserProfileViewModel userVM = await _userService.GetUserProfileViewModel(userId);//Trả về model chính của view
                        ViewData["UserProfileDTO"] = await _userService.GetUserProfileDTO(userId);//Trả về model phụ cho view

                        ViewBag.ListAddress = await _userAddressService.GetAllListAddressUser(userId);

                        return View(userVM);
                    }
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login", "Authentication");
        }

        //Thêm địa chỉ mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAddress(UserProfileDTO userProfileDTO)
        {
            if (_isAuthenticated)//Đã đăng nhập
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    var userId = userIdClaim.Value;
                    // Use userId as needed
                    var result = await _userAddressService.Save(userProfileDTO, userId);

                    if (result.Status)
                    {
                        return Json(new { status = "success", message = result.Message });
                    }
                    else
                    {
                        return Json(new { status = "warning", message = "Lỗi xảy ra !!!" });
                    }
                }
            }

            return Json(new { status = "warning", message = "Lỗi xảy ra !!!" });
        }
    }
}
