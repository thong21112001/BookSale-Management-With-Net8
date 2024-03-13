using BookSale.Management.Domain.Entities;

namespace BookSale.Management.Domain.Abstracts
{
	public interface IBookRepository
	{
		Task<(IEnumerable<T>, int)> GetAllBookByPagination<T>(int pageIndex, int pageSize, string keyword);
        Task<Book> GetById(int id);
    }
}
