using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.Book;
using BookSale.Management.Application.DTOs.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookSale.Management.Application.Abstracts
{
    public interface IBookService
    {
        Task<string> GenerateCodeAsync(int number = 8);
        Task<BookForSiteDTO> GetAllBookByCustomer(int genreId, int pageIndex, int pageSize = 12);
        Task<ResponseDataTable<BookDTO>> GetAllBookPaginationAsync(RequestDataTable request);
        Task<BookViewModel> GetBookById(int id);
        Task<IEnumerable<SelectListItem>> GetGenreForDropDownList();
        Task<string> GetStringImage(int id);
        Task<ResponseModel> SaveAsync(BookViewModel bookVM, string oldImage);
    }
}
