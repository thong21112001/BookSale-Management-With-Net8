using BookSale.Management.DataAccess.DataAccess;
using BookSale.Management.Domain.Abstracts;
using BookSale.Management.Domain.Entities;
using Dapper;

namespace BookSale.Management.DataAccess.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
		private readonly ISQLQueryHandler _sQLQueryHandler;

		public OrderRepository(BookSaleDbContext context, ISQLQueryHandler sQLQueryHandler) : base(context)
        {
			_sQLQueryHandler = sQLQueryHandler;
		}

        public async Task Save(Order order)
        {
            await base.Create(order);
        }

		public async Task<(IEnumerable<T>, int)> GetAllOrderByPagination<T>(int pageIndex, int pageSize, string keyword)
		{
			DynamicParameters parameters = new DynamicParameters();

			parameters.Add("keyword", keyword, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("pageIndex", pageIndex, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
			parameters.Add("pageSize", pageSize, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
			parameters.Add("totalRecords", 0, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);

			var result = await _sQLQueryHandler.ExecuteStoreProcedureReturnListAsync<T>("spGetAllOrdersByPagination", parameters);

			var totalRecords = parameters.Get<int>("totalRecords");

			return (result, totalRecords);  //Trả về list T từ storeProcedure trong Db và tổng records.
		}

        public async Task<IEnumerable<T>> GetReportOrderByExcelAsync<T>(string fromday, string today, int genreId, int status)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("fromday", fromday, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("today", today, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("genreId", genreId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            parameters.Add("status", status, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

            var result = await _sQLQueryHandler.ExecuteStoreProcedureReturnListAsync<T>("spGetReportOrderByExcelAsync", parameters);

            return result;  //Trả về list T từ storeProcedure trong Db.
        }
    }
}
