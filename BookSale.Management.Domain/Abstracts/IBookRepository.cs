using BookSale.Management.Domain.Entities;

namespace BookSale.Management.Domain.Abstracts
{
	public interface IBookRepository
	{
        Task CreateBook(Book book);
        void DeleteBook(Book book);
        Task<(IEnumerable<Book>, int)> GetAllBookByCustomer(int genreId, int pageIndex, int pageSize = 10);
        Task<(IEnumerable<T>, int)> GetAllBookByPagination<T>(int pageIndex, int pageSize, string keyword);
        Task<Book?> GetBookByCodeAsync(string code);
        Task<IEnumerable<Book>> GetBookByListCodeAsync(string[] codes);
        Task<Book?> GetById(int id);
        Task<Book?> GetCodeBook(string code);
        Task SaveBook();
        void UpdateBook(Book book);
    }
}
