using Dapper;
using GecisKontrol.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GecisKontrol.Domain.Interfaces;
using System.Transactions;
using System.Data.Common;

namespace GecisKontrol.DAL.Data
{
	public class GateRepository : IGateRepository
	{
		private readonly DbContext _dbContext;

		public GateRepository(DbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<Gate>> GetAllGatesAsync()
		{
			using var connection = _dbContext.CreateConnection();
			{
				return await connection.QueryAsync<Gate>("SELECT * FROM \"Gate\"");
			}
		}

		public async Task<Gate> GetGateByIdAsync(int id)
		{
			using var connection = _dbContext.CreateConnection();
			{
				return await connection.QuerySingleOrDefaultAsync<Gate>("SELECT * FROM \"Gate\" WHERE Id = @Id", new { Id = id });
			}
		}

		public async Task<int> AddGateAsync(Gate gate)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "INSERT INTO \"Gate\" (Name, Description, UnitId) VALUES (@Name, @Description, @UnitId) RETURNING Id";
			var insertedId = await connection.ExecuteScalarAsync<int>(query, gate);
			return insertedId;
		}

		public async Task UpdateGateAsync(Gate gate)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "UPDATE \"Gate\" SET Name = @Name, Description = @Description, UnitId = @UnitId WHERE Id = @Id";
			await connection.ExecuteAsync(query, gate);
		}

		public async Task DeleteGateAsync(int id)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "DELETE FROM \"Gate\" WHERE Id = @Id";
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
