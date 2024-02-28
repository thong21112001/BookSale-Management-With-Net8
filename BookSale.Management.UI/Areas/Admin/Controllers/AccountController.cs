using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var accountDTO = new CreateAccountDTO();

            var roles = await _roleService.GetRoleForDropDownList();
            ViewBag.Roles = roles;
            ViewBag.IdUser = id;

            if (string.IsNullOrEmpty(id))
            {
                
                return View(accountDTO);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Thêm cái này ngoài form SaveData asp-antiforgery="true"
        public async Task<IActionResult> SaveData(CreateAccountDTO accountDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.Save(accountDTO);

                if (result.Status)
                {
                    return RedirectToAction("", "Account");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }
            else
            {
                ModelState.AddModelError("", "");
            }

            return View();
        }
    }
}
