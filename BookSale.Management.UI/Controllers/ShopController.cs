using BookSale.Management.Application.Abstracts;
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
        
		public IActionResult Index()
		{
			return View();
		}
	}
}
