using AutoMapper;
using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.ViewModels;
using BookSale.Management.Domain.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookSale.Management.Application.Services
{
	public class BookService : IBookService
	{
		private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
			_unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SelectListItem>> GetGenreForDropDownList()
        {
            var genres = await _unitOfWork.GenreRepository.GetAllGenre();

            return genres.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
        }

        public async Task<BookViewModel> GetBookById(int id)
        {
            var book = await _unitOfWork.BookRepository.GetById(id);

			var bookDTO = _mapper.Map<BookViewModel>(book);

			return bookDTO;
		}

        public async Task<ResponseDataTable<BookDTO>> GetAllBookPaginationAsync(RequestDataTable request)
		{
			try
			{
				int totalRecords = 0;
				IEnumerable<BookDTO> books;

				(books, totalRecords) = await _unitOfWork.BookRepository.GetAllBookByPagination<BookDTO>(request.SkipItems, request.PageSize, request.keyword);

				return new ResponseDataTable<BookDTO>
				{
					Draw = request.Draw,
					RecordsTotal = totalRecords,
					RecordsFiltered = totalRecords,
					Data = books
				};
			}
			catch (Exception ex)
			{
				// Log or handle the exception here
				Console.WriteLine($"Error in GetAllBookPaginationAsync: {ex.Message}");
				throw; // Rethrow the exception
			}
		}
	}
}
