using Dapper;
using GecisKontrol.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GecisKontrol.Domain.Interfaces;

namespace GecisKontrol.DAL.Data
{
	public class ErrorLogRepository : IErrorLogRepository
	{
		private readonly DbContext _dbContext;

		public ErrorLogRepository(DbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task AddErrorLogAsync(ErrorLog errorLog)
		{
			string query = "INSERT INTO \"ErrorLog\" (userid, controllername, actionname, errorname, parameters, creationdate) VALUES (@UserId, @ControllerName, @ActionName, @ErrorName, @Parameters, @CreationDate)";



			using (var connection = _dbContext.CreateConnection())
			{
				await connection.ExecuteAsync(query, errorLog);
			}
		}

        public async Task<ErrorLog> GetErrorLogByIdAsync(int id)
        {
            string query = "SELECT * FROM \"ErrorLog\" WHERE id = @Id";

            using (var connection = _dbContext.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<ErrorLog>(query, new { Id = id });
            }
        }



        public async Task<IEnumerable<ErrorLog>> GetAllErrorLogsAsync()
        {
            using var connection = _dbContext.CreateConnection();
            {
                return await connection.QueryAsync<ErrorLog>("SELECT * FROM \"ErrorLog\"");
            }
        }

    }
}
