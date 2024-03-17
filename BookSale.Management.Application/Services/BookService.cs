using AutoMapper;
using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.ViewModels;
using BookSale.Management.Domain.Abstracts;
using BookSale.Management.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookSale.Management.Application.Services
{
	public class BookService : IBookService
	{
		private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICommonService _commonService;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper, ICommonService commonService)
        {
			_unitOfWork = unitOfWork;
            _mapper = mapper;
            _commonService = commonService;
        }

		//Hàm lấy tất cả các thể loại hiển thị lên dropdown list ở Book/SaveData
		public async Task<IEnumerable<SelectListItem>> GetGenreForDropDownList()
        {
            var genres = await _unitOfWork.GenreRepository.GetAllGenre();

            return genres.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
        }

		//Hàm lấy 1 sách khi truyền vào Id của book
		public async Task<BookViewModel> GetBookById(int id)
        {
            var book = await _unitOfWork.BookRepository.GetById(id);

			var bookDTO = _mapper.Map<BookViewModel>(book);

			return bookDTO;
		}

		//Hàm phân trang, hiển thị danh sách book lên Book/Index
		public async Task<ResponseDataTable<BookDTO>> GetAllBookPaginationAsync(RequestDataTable request)
		{
			try
			{
				int totalRecords = 0;
				IEnumerable<BookDTO> books;

				(books, totalRecords) = await _unitOfWork.BookRepository.GetAllBookByPagination<BookDTO>((request.SkipItems/request.PageSize) + 1, request.PageSize, request.keyword);

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

		//Hàm tạo mới và cập nhập sách
		public async Task<ResponseModel> SaveAsync(BookViewModel bookVM)
		{
			var book = _mapper.Map<Book>(bookVM);

			if (bookVM.Id == 0)
			{
				book.CreatedOn = DateTime.Now;
				book.IsActive = true;
                book.Code = bookVM.Code;

                await _unitOfWork.BookRepository.CreateBook(book);
            }
			else
			{
                _unitOfWork.BookRepository.UpdateBook(book);
            }

			await _unitOfWork.BookRepository.SaveBook();

			return new ResponseModel
			{
				Action = Domain.Enums.ActionType.Update,
				Message = $"{(bookVM.Id == 0 ? "Thêm mới" : "Cập nhập")} thành công !!!",
				Status = true
			};
		}

		//Hàm tạo code random khi bấm button tạo mới cho book
		public async Task<string> GenerateCodeAsync(int number = 8)
        {
            //Tránh trùng code book
            string newCode = string.Empty;

            while (true)
            {
                newCode = _commonService.GenerateRandomCode(number);
                var bookCode = await _unitOfWork.BookRepository.GetCodeBook(newCode);

                if (bookCode is null)
                {
                    break;
                }
            };

            return newCode;
        }
	}
}
