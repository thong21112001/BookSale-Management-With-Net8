using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookSale.Management.UI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //Mặc định không đăng nhập là false
            var isAuthenticated = User.Identity?.IsAuthenticated ?? false;

            if (isAuthenticated)//Đã đăng nhập
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    var userId = userIdClaim.Value;
                    // Use userId as needed
                    UserProfileDTO userDTO = await _userService.GetUserProfile(userId);

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
    }
}
