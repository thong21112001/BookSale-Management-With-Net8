using BookSale.Management.DataAccess.DataAccess;
using BookSale.Management.Domain.Abstracts;
using BookSale.Management.Domain.Entities;
using Dapper;

namespace BookSale.Management.DataAccess.Repository
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly ISQLQueryHandler _queryHandler;

		public BookRepository(BookSaleDbContext context, ISQLQueryHandler queryHandler) : base(context)
        {
            _queryHandler = queryHandler;
		}

        public async Task<Book?> GetById(int id)
        {
            return await GetSingleAsync(x => x.Id == id);
        }

        public async Task<Book?> GetCodeBook(string code)
        {
            return await GetSingleAsync(x => x.Code == code);
        }

        public async Task<IEnumerable<Book>> GetBookByListCodeAsync(string[] codes)
        {
            return await base.GetALlAsync(x => codes.Contains(x.Code));
        }

        public async Task<(IEnumerable<T>, int)> GetAllBookByPagination<T>(int pageIndex, int pageSize, string keyword)
		{
			DynamicParameters parameters = new DynamicParameters();

			parameters.Add("keyword", keyword,System.Data.DbType.String,System.Data.ParameterDirection.Input);
			parameters.Add("pageIndex", pageIndex,System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
			parameters.Add("pageSize", pageSize, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
			parameters.Add("totalRecords", 0,System.Data.DbType.Int32,System.Data.ParameterDirection.Output);

			var result = await _queryHandler.ExecuteStoreProcedureReturnListAsync<T>("GetAllBookByPagination", parameters);

			var totalRecords = parameters.Get<int>("totalRecords");

			return (result, totalRecords);	//Trả về list T từ storeProcedure trong Db và tổng records.
		}

        public async Task CreateBook(Book book)
        {
            await Create(book);
        }

        public void UpdateBook(Book book)
        {
			//excludeProperties: nameof(Book.CreatedOn) -> bỏ qua trường không cập nhập
			Update(book, excludeProperties: nameof(Book.CreatedOn));
        }

        public void DeleteBook(Book book)
        {
            Delete(book);
        }

        public async Task SaveBook()
        {
            await Commit();
        }

        //Show book to Customer
        public async Task<(IEnumerable<Book>, int)> GetAllBookByCustomer(int genreId, int pageIndex, int pageSize = 10)
        {
            IEnumerable<Book> books;

            books = await base.GetALlAsync(x => genreId == 0 || x.GenreId == genreId);

            var totalRecords = books.Count();

            books = books.Skip((pageIndex - 1) * pageSize).Take(pageIndex * pageSize).ToList();

            return (books, totalRecords);
        }
    }
}
