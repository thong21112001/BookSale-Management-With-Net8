using BookSale.Management.Domain.Entities;

namespace BookSale.Management.Domain.Abstracts
{
	public interface IBookRepository
	{
        Task CreateBook(Book book);
        void DeleteBook(Book book);
        Task<(IEnumerable<T>, int)> GetAllBookByPagination<T>(int pageIndex, int pageSize, string keyword);
        Task<Book?> GetById(int id);
        Task<Book?> GetCodeBook(string code);
        Task SaveBook();
        void UpdateBook(Book book);
    }
}
