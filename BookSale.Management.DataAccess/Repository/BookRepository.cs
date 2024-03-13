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

        public async Task<Book> GetById(int id)
        {
            return await GetSingleAsync(x => x.Id == id);
        }

        public async Task<(IEnumerable<T>, int)> GetAllBookByPagination<T>(int pageIndex, int pageSize, string keyword)
		{
			DynamicParameters parameters = new DynamicParameters();

			parameters.Add("keyword", keyword,System.Data.DbType.String,System.Data.ParameterDirection.Input);
			parameters.Add("pageIndex", pageIndex,System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
			parameters.Add("pageSize", pageSize, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
			parameters.Add("totalRecords", 0,System.Data.DbType.Int32,System.Data.ParameterDirection.Output);

			var result = await _queryHandler.ExecuteStoreProcedureReturnListAsync<T>("GetAllBookByPagination", parameters);

			var totalRecords = parameters.Get<int>("totalRecords");

			return (result, totalRecords);	//Trả về list T từ storeProcedure trong Db và tổng records.
		}
	}
}
