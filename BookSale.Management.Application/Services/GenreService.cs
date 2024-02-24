using BookSale.Management.Application.Abstracts;
using BookSale.Management.Domain.Abstracts;

namespace BookSale.Management.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

    }
}
