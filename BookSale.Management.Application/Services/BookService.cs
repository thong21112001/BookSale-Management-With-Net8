using AutoMapper;
using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Domain.Abstracts;

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

		public async Task<ResponseDataTable<BookDTO>> GetAllBookPaginationAsync(RequestDataTable request)
		{
			var books = await _unitOfWork.BookRepository.GetAllBook();

			var bookDTOs = _mapper.Map<IEnumerable<BookDTO>>(books);

			//Pagination
			int totalRecords = books.Count();

			var result = bookDTOs.Skip(request.SkipItems).Take(request.PageSize).ToList();

			return new ResponseDataTable<BookDTO>
			{
				Draw = request.Draw,
				RecordsTotal = totalRecords,
				RecordsFiltered = totalRecords,
				Data = result
			};
		}
	}
}
