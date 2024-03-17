using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.ViewModels;
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
            var bookVM = new BookViewModel();

            ViewBag.Genre = await _bookService.GetGenreForDropDownList();

            string code = await _bookService.GenerateCodeAsync();
            bookVM.Code = code;

            if (id != 0)
            {
                bookVM = await _bookService.GetBookById(id);
            }

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

            var result = await _bookService.SaveAsync(bookVM);

            if (result.Status)
            {
                if (bookVM.Id == 0)
                {
                    TempData["Type"] = "success";
                    TempData["Message"] = result.Message;
                }
                else
                {
                    TempData["Type"] = "info";
                    TempData["Message"] = result.Message;
                }
                return RedirectToAction("", "Book");
            }

            ModelState.AddModelError("", "Invalid model");
            ViewBag.Genre = await _bookService.GetGenreForDropDownList();
            TempData["Type"] = "warning";
            TempData["Message"] = "Có lỗi xảy ra !!!";

            return View(bookVM);
        }

        [HttpGet]
        public async Task<IActionResult> GenerateCodeBook()
        {
            var result = await _bookService.GenerateCodeAsync();
            return Json(result);
        }
    }
}
