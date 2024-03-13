using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.ViewModels;
using BookSale.Management.Application.Services;
using BookSale.Management.UI.Ultility;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Areas.Admin.Controllers
{
	public class BookController : BaseController
	{
		private readonly IBookService _bookService;

		public BookController(IBookService bookService)
        {
			_bookService = bookService;
		}

		[Breadscrum("Book List", "Management")]
		public IActionResult Index()
		{
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> GetBookPagination(RequestDataTable requestDataTable)
        {
            var data = await _bookService.GetAllBookPaginationAsync(requestDataTable);
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> SaveData(int id)
        {
            BookViewModel bookVM = !(id == 0) ? await _bookService.GetBookById(id) : new();

            ViewBag.Genre = await _bookService.GetGenreForDropDownList();

            return View(bookVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Thêm cái này ngoài form SaveData asp-antiforgery="true"
        public async Task<IActionResult> SaveData(BookViewModel bookVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Genre = await _bookService.GetGenreForDropDownList();
                ModelState.AddModelError("", "Invalid model");
                return View(bookVM);
            }

            //var result = await _userService.Save(accountDTO);

            //if (result.Status)
            //{
            //    if (string.IsNullOrEmpty(accountDTO.Id))
            //    {
            //        TempData["Type"] = "success";
            //        TempData["Message"] = "Thêm thành công !!!";
            //    }
            //    else
            //    {
            //        TempData["Type"] = "info";
            //        TempData["Message"] = "Cập nhập thành công !!!";
            //    }
            //    return RedirectToAction("", "Account");
            //}

            //ModelState.AddModelError("", result.Message);
            //ViewBag.Roles = await _roleService.GetRoleForDropDownList();
            //TempData["Type"] = "warning";
            //TempData["Message"] = "Có lỗi xảy ra !!!";

            return View(bookVM);
        }

    }
}
