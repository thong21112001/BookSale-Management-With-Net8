using BookSale.Management.Application.DTOs;

namespace BookSale.Management.Application.Abstracts
{
    public interface IGenreService
    {
        Task<ResponseDataTable<GenreDTO>> GetAllGenre(RequestDataTable request);
    }
}
