using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookSale.Management.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
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
        public IActionResult SaveData(string id)
        {
            var accountDTO = new CreateAccountDTO();
            if (string.IsNullOrEmpty(id))
            {
                
                return View(accountDTO);
            }

            return View(accountDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Thêm cái này ngoài form SaveData asp-antiforgery="true"
        public IActionResult SaveData(CreateAccountDTO accountDTO)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var md = accountDTO;

            return View();
        }
    }
}
