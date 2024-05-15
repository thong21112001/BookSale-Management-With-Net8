using BookSale.Management.Application.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Controllers
{
    public class ProductDetailController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly IBookService _bookService;

        public ProductDetailController(IGenreService genreService, IBookService bookService)
        {
            _genreService = genreService;
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string code)
        {
            var getBook = await _bookService.GetBookViewByUser(code);

            var getGenreForBook = await _genreService.GetGenreForBookVM(getBook.GenreId);

            var genres = _genreService.GetSumBookOfGenre();

            var result = await _bookService.GetAllBookByCustomer(0, 1, 12);

            var books = result.Books.OrderBy(x => Guid.NewGuid()).Take(5);

            var getBooksByGenre = result.Books.Where(x=> x.GenreId == getBook.GenreId).OrderBy(x => Guid.NewGuid()).Take(3);

            ViewBag.GenresProductDetail = genres;
            ViewBag.GetSuggesBook = books;
            ViewBag.GenreForBook = getGenreForBook;
            ViewBag.GetBooksByGenre = getBooksByGenre;
            ViewData["GenreListProductDetail"] = await _genreService.GetAllGenreForCustomer();

            return View(getBook);
        }
    }
}
