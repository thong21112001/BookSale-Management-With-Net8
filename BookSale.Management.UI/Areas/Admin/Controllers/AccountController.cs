using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs;
using BookSale.Management.UI.Ultility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookSale.Management.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public AccountController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [Breadscrum("Account List","Management")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetAccountPagination(RequestDataTable requestDataTable)
        {
            var data = await _userService.GetAllUser(requestDataTable);
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> SaveData(string? id)
        {
            CreateAccountDTO accountDTO = !string.IsNullOrEmpty(id) ? await _userService.GetUserById(id) : new();

            ViewBag.Roles = await _roleService.GetRoleForDropDownList();
            ViewBag.IdUser = id;

            return View(accountDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Thêm cái này ngoài form SaveData asp-antiforgery="true"
        public async Task<IActionResult> SaveData(CreateAccountDTO accountDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = await _roleService.GetRoleForDropDownList();
                ModelState.AddModelError("", "Invalid model");

                return View(accountDTO);
            }

            var result = await _userService.Save(accountDTO);

            if (result.Status)
            {
                TempData["Type"] = "success";
                TempData["Message"] = "Updated successfully!!!";
                return RedirectToAction("", "Account");
            }

            ModelState.AddModelError("", result.Message);
            ViewBag.Roles = await _roleService.GetRoleForDropDownList();
            
            return View(accountDTO);
        }

		[HttpPost]
		public async Task<IActionResult> Delete(string id)
		{
            return Json(await _userService.Delete(id));
		}
	}
}
