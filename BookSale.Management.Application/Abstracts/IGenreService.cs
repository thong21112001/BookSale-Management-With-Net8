using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.Genre;
using BookSale.Management.Application.DTOs.ViewModels;

namespace BookSale.Management.Application.Abstracts
{
    public interface IGenreService
    {
        Task<bool> Delete(int id);
        Task<ResponseDataTable<GenreDTO>> GetAllGenre(RequestDataTable request);
        Task<IEnumerable<GenreDTO>> GetAllGenreForCustomer();
        Task<GenreViewModel> GetById(int id);
        IEnumerable<GenreSiteDTO> GetSumBookOfGenre();
        Task<ResponseModel> Save(GenreViewModel request);
    }
}
