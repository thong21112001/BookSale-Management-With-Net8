using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookSale.Management.Application.Abstracts
{
    public interface IBookService
    {
        Task<ResponseDataTable<BookDTO>> GetAllBookPaginationAsync(RequestDataTable request);
        Task<BookViewModel> GetBookById(int id);
        Task<IEnumerable<SelectListItem>> GetGenreForDropDownList();
    }
}
