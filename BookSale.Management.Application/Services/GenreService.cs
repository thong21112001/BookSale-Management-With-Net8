using AutoMapper;
using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.ViewModels;
using BookSale.Management.Domain.Abstracts;
using Microsoft.AspNetCore.Identity;

namespace BookSale.Management.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

		public async Task<GenreViewModel> GetById(int id)
		{
			 var genre = await _unitOfWork.GenreRepository.GetById(id);

			//Map dữ liệu get đc thông qua Id sau đó map từ genre -> GenreDTO
			return _mapper.Map<GenreViewModel>(genre);
		}

		public async Task<ResponseDataTable<GenreDTO>> GetAllGenre(RequestDataTable request)
		{
			var genres = await _unitOfWork.GenreRepository.GetAllGenre();

			var genresDTO = _mapper.Map<IEnumerable<GenreDTO>>(genres);

			//Pagination
			int totalRecords = genres.Count();

			var result = genresDTO.Skip(request.SkipItems).Take(request.PageSize).ToList();

			return new ResponseDataTable<GenreDTO>
			{
				Draw = request.Draw,
				RecordsTotal = totalRecords,
				RecordsFiltered = totalRecords,
				Data = result
			};
		}
	}
}
