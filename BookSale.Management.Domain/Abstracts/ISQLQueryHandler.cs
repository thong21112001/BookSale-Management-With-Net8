using Dapper;
using System.Data;

namespace BookSale.Management.Domain.Abstracts
{
	public interface ISQLQueryHandler
	{
		Task ExecuteNonReturnAsync(string qr, DynamicParameters dynamicParameters, IDbTransaction dbTransaction = null);
		Task<T> ExecuteReturnSingleRowAsync<T>(string qr, DynamicParameters dynamicParameters, IDbTransaction dbTransaction = null);
		Task<T?> ExecuteReturnSingleValueScalarAsync<T>(string qr, DynamicParameters dynamicParameters, IDbTransaction dbTransaction = null);
		Task<IEnumerable<T>> ExecuteStoreProcedureReturnListAsync<T>(string storeName, DynamicParameters dynamicParameters, IDbTransaction dbTransaction = null);
	}
}