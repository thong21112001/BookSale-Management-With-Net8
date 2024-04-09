using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs;
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
                    UserProfileDTO userDTO = await _userService.GetUserProfile(userId);
                    ViewBag.ListAddress = await _userAddressService.GetAllListAddressUser(userId);


                    return View(userDTO);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(UserProfileDTO userProfileDTO)
        {
            return View();
        }

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
