using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs;
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
    }
}
