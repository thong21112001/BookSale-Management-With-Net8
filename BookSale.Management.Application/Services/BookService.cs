using AutoMapper;
using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.ViewModels;
using BookSale.Management.Domain.Abstracts;
using BookSale.Management.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using System.Reflection;

namespace BookSale.Management.Application.Services
{
	public class BookService : IBookService
	{
		private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICommonService _commonService;
        private readonly IImageService _imageService;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper, ICommonService commonService, IImageService imageService)
        {
			_unitOfWork = unitOfWork;
            _mapper = mapper;
            _commonService = commonService;
            _imageService = imageService;
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

        public async Task<string> GetStringImage(int id)
        {
            var book = await _unitOfWork.BookRepository.GetById(id);
            string imageBook = string.Empty;

            if (book is not null)
            {
                imageBook = book.Image.ToString();
                _unitOfWork.Detach(book);
            }

            return imageBook;
        }

        //Hàm lấy 1 sách khi truyền vào Id của book
        public async Task<BookViewModel> GetBookById(int id)
        {
            var book = await _unitOfWork.BookRepository.GetById(id);

			var bookVM = _mapper.Map<BookViewModel>(book);

			return bookVM;
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
        public async Task<ResponseModel> SaveAsync(BookViewModel bookVM , string oldImage)
		{
            //Map dữ liệu từ bookVM sang Class Book
            var book = _mapper.Map<Book>(bookVM);
            
            if (bookVM.Id == 0) //Tạo mới dữ liệu
			{
				book.CreatedOn = DateTime.Now;
				book.IsActive = true;
                book.Code = bookVM.Code;

                // Thêm mới dữ liệu
                await _unitOfWork.BookRepository.CreateBook(book);

                // Kiểm tra nếu người dùng cung cấp hình ảnh
                if (!string.IsNullOrEmpty(bookVM.ImageText)) // Chọn lưu bằng địa chỉ ngoài
                {
                    book.Image = bookVM.ImageText;
                }
                else if (bookVM.Image != null) //Chọn lưu vào thư mục
                {
                    string guidFileName = $"{Guid.NewGuid()}.png";

                    bool check = await _imageService.SaveImage(new List<IFormFile> { bookVM.Image }, "images/books", guidFileName);
                    
                    if (check == true)
                    {
                        book.Image = guidFileName;
                    }
                }
                else //Không chọn hình ảnh
                {
                    book.Image = "";
                }
            }
			else
			{
                // Cập nhập dữ liệu
                _unitOfWork.BookRepository.UpdateBook(book);

                //Nếu không chọn hình thức lưu ảnh nào 2 cái đều null, oldImage không có "" (rỗng)
                //Trường hợp lúc tạo book nhưng không thêm ảnh, và việc cập nhập cũng không chọn ảnh nào
                //Db từ ảnh cũng rỗng nên chuyển sang else
                if (!string.IsNullOrEmpty(bookVM.ImageText) || bookVM.Image != null || !string.IsNullOrEmpty(oldImage))
                {
                    if (!string.IsNullOrEmpty(bookVM.ImageText)) // Chọn lưu bằng địa chỉ ngoài
                    {
                        //Nếu ảnh không bắt đầu bằng https và oldImage bằng rỗng thì không xử lý
                        if (!oldImage.StartsWith("https:") && !string.IsNullOrEmpty(oldImage))
                        {
                            //Lấy đường dẫn gốc đến wwwroot
                            string webRootPath = _imageService.UrlSaveImg();

                            //Cộnng với đường dẫn gốc thành ../wwwroot/images/books
                            string imageDirectoryPath = Path.Combine(webRootPath, "images", "books");

                            // Xoá ảnh cũ nếu tồn tại
                            string oldImagePath = Path.Combine(imageDirectoryPath, oldImage);
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        //Chuyển sang lưu ảnh text vào Image Db
                        book.Image = bookVM.ImageText;
                    }
                    else if (bookVM.Image != null) // Chọn lưu bằng file ảnh tải lên
                    {
                        //Nếu ảnh không bắt đầu bằng https và oldImage bằng rỗng thì không xử lý
                        if (!oldImage.StartsWith("https:") && !string.IsNullOrEmpty(oldImage))
                        {
                            //Lấy đường dẫn gốc đến wwwroot
                            string webRootPath = _imageService.UrlSaveImg();

                            //Cộnng với đường dẫn gốc thành ../wwwroot/images/books
                            string imageDirectoryPath = Path.Combine(webRootPath, "images", "books");

                            // Xoá ảnh cũ nếu tồn tại
                            string oldImagePath = Path.Combine(imageDirectoryPath, oldImage);
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        //Tạo Guid để không bị trùng, lấy thành tên ảnh đuôi png
                        string guidFileName = $"{Guid.NewGuid()}.png";  

                        //Tiến hành lưu ảnh mới
                        bool check = await _imageService.SaveImage(new List<IFormFile> { bookVM.Image }, "images/books", guidFileName);

                        if (check == true)
                        {
                            // Cập nhật tên hình vào db
                            book.Image = guidFileName;
                        }
                    }
                    else //Nếu không chọn bắt kì việc lưu ảnh nào mà ảnh gốc vẫn còn thì gán lại tránh lỗi
                    {
                        book.Image = oldImage;
                    }
                }
                else //Không chọn việc lưu ảnh và ảnh ""(rỗng) trong Db
                {
                    book.Image = "";
                }
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

        //Lấy toàn bộ sách hiển thị ra bên ngoài cho KH
        public async Task<(IEnumerable<BookDTO>,int)> GetAllBookByCustomer(int genreId, int pageIndex, int pageSize = 10)
        {
            try
            {
                IEnumerable<Book> books;
                int totalRecords = 0;

                (books, totalRecords) = await _unitOfWork.BookRepository.GetAllBookByCustomer(genreId, pageIndex, pageSize);

                var bookDTOs = _mapper.Map<IEnumerable<BookDTO>>(books);

                return (bookDTOs,totalRecords);
            }
            catch (Exception ex)
            {
                // Log or handle the exception here
                Console.WriteLine($"Error in GetAllBookByCustomer: {ex.Message}");
                throw; // Rethrow the exception
            }
        }
    }
}
