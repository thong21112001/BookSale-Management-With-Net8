using BookSale.Management.Application.DTOs;

namespace BookSale.Management.Application.Abstracts
{
    public interface IBookService
    {
        Task<ResponseDataTable<BookDTO>> GetAllBookPaginationAsync(RequestDataTable request);
    }
}
