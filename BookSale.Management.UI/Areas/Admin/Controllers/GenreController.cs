using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.ViewModels;
using BookSale.Management.Application.Services;
using BookSale.Management.UI.Ultility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [Breadscrum("Genre List","Management")]
        public IActionResult Index()
        {
            var genreVM = new GenreViewModel();
            return View(genreVM);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            return Json(await _genreService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> GetGenrePagination(RequestDataTable requestDataTable)
        {
			var data = await _genreService.GetAllGenre(requestDataTable);
			return Json(data);
		}

		[HttpPost]
		[ValidateAntiForgeryToken] //Thêm cái này ngoài form SaveData asp-antiforgery="true"
		public async Task<IActionResult> SaveData(GenreViewModel genreViewModel)
		{
            if (ModelState.IsValid)
            {
                if (genreViewModel.Id == 0) //Thực hiện thêm genre
                {
                    var result = await _genreService.Save(genreViewModel);
                    if (result.Status)
                    {
						return Json(new { status = "success", message = result.Message });
					}
                }
                else //Thực hiện cập nhập genre
                {
                    var result = await _genreService.Save(genreViewModel);
                    if (result.Status)
                    {
						return Json(new { status = "info", message = result.Message });
					}
                }
                
            }
			return Json(new { status = "warning", message = "Lỗi xảy ra !!!" });
		}

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            return Json(await _genreService.Delete(id));
        }
    }
}
