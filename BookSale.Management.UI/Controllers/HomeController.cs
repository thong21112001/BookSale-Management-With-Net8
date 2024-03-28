using BookSale.Management.Application.Abstracts;
using BookSale.Management.Domain.Settings;
using BookSale.Management.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookSale.Management.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IGenreService _genreService;

        public HomeController(IBookService bookService, IGenreService genreService)
        {
            _bookService = bookService;
            _genreService = genreService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _bookService.GetAllBookByCustomer(0, 1, 12);

            var books = result.Books.OrderBy(x => Guid.NewGuid()).Take(5);

            ViewData["GenreList"] = await _genreService.GetAllGenreForCustomer();

            return View(books);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
