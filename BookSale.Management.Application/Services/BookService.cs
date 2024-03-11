using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Domain.Abstracts;

namespace BookSale.Management.Application.Services
{
	public class BookService : IBookService
	{
		private readonly IUnitOfWork _unitOfWork;

		public BookService(IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
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
