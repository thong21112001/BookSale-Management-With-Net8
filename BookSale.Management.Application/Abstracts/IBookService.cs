using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookSale.Management.Application.Abstracts
{
    public interface IBookService
    {
        Task<string> GenerateCodeAsync(int number = 8);
        Task<ResponseDataTable<BookDTO>> GetAllBookPaginationAsync(RequestDataTable request);
        Task<BookViewModel> GetBookById(int id);
        Task<IEnumerable<SelectListItem>> GetGenreForDropDownList();
        Task<ResponseModel> SaveAsync(BookViewModel bookVM);
    }
}
