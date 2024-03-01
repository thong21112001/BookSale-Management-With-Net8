﻿using BookSale.Management.Application.Abstracts;
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
                var data = genreViewModel;
            }
			return Json(1);
		}
	}
}
