using BookSale.Management.Domain.Entities;

namespace BookSale.Management.Domain.Abstracts
{
	public interface IBookRepository
	{
		Task<IEnumerable<Book>> GetAllBook();
	}
}
