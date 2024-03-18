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

			int pageSize = CommonConstant.BookPageSize;
			var books = await _bookService.GetAllBookByCustomer(g, idx, pageSize);
			
			//Tra ve Item1 = books, Item2 = totalRecords
			ViewBag.TotalBook = books.Item2;

            return View(books.Item1);
		}
	}
}
