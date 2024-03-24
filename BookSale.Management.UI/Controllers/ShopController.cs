using BookSale.Management.Application.Abstracts;
using BookSale.Management.Domain.Settings;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Controllers
{
    public class ShopController : Controller
	{
		private readonly IGenreService _genreService;
		private readonly IBookService _bookService;

		public ShopController(IGenreService genreService, IBookService bookService)
        {
			_genreService = genreService;
			_bookService = bookService;
		}
        
		public async Task<IActionResult> Index(int g = 0, int idx = 1)
		{
            var genres = _genreService.GetSumBookOfGenre();

            ViewBag.Genres = genres;
			ViewBag.CurrentGenres = g;
			ViewBag.CurrentPageIdx = idx;

            var result = await _bookService.GetAllBookByCustomer(g, idx, CommonConstant.BookPageSize);

            return View(result);
		}

		[HttpGet]
        public async Task<IActionResult> GetBookByPagination(int genre, int pageIndex)
        {
            var result = await _bookService.GetAllBookByCustomer(genre, pageIndex, CommonConstant.BookPageSize);

            return Json(result);
        }
    }
}
