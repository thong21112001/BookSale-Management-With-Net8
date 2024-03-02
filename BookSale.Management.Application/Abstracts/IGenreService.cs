using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.ViewModels;

namespace BookSale.Management.Application.Abstracts
{
    public interface IGenreService
    {
        Task<ResponseDataTable<GenreDTO>> GetAllGenre(RequestDataTable request);
        Task<GenreViewModel> GetById(int id);
    }
}
