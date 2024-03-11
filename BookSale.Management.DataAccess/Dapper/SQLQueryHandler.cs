using BookSale.Management.Domain.Abstracts;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using static Dapper.SqlMapper;

namespace BookSale.Management.DataAccess.Dapper
{
	//Dùng để xử lý các câu query bằng dapper
	public class SQLQueryHandler : ISQLQueryHandler
	{
		private readonly string _connectionString = string.Empty;
		public SQLQueryHandler(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
		}

		//Không trả về bất cứ giá trị nào
		public async Task ExecuteNonReturnAsync(string qr, DynamicParameters dynamicParameters,
														IDbTransaction dbTransaction = null)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.ExecuteAsync(qr, param: dynamicParameters, dbTransaction);
			}
		}

		//Trả về 1 giá trị đơn
		public async Task<T?> ExecuteReturnSingleValueScalarAsync<T>(string qr, DynamicParameters dynamicParameters,
														IDbTransaction dbTransaction = null)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				return await connection.ExecuteScalarAsync<T>(qr, param: dynamicParameters, dbTransaction);
			}
		}

		//Trả về 1 dòng dữ liệu
		public async Task<T> ExecuteReturnSingleRowAsync<T>(string qr, DynamicParameters dynamicParameters,
																IDbTransaction dbTransaction = null)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				return await connection.QuerySingleAsync<T>(qr, param: dynamicParameters, dbTransaction);
			}
		}

		//Trả về 1 list object vs storedprocedure
		public async Task<IEnumerable<T>> ExecuteStoreProcedureReturnListAsync<T>(string storeName,
										DynamicParameters dynamicParameters, IDbTransaction dbTransaction = null)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				return await connection.QueryAsync<T>(storeName, dynamicParameters, dbTransaction, commandType: CommandType.StoredProcedure);
			}
		}
	}
}
