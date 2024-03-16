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
	public class UnitRepository : IUnitRepository
	{
		private readonly DbContext _dbContext;

		public UnitRepository(DbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<Unit>> GetAllUnitsAsync()
		{
			using var connection = _dbContext.CreateConnection();
			{
				return await connection.QueryAsync<Unit>("SELECT * FROM \"Unit\"");
			}
		}

		public async Task<Unit> GetUnitByIdAsync(long id)
		{
			using var connection = _dbContext.CreateConnection();
			{
				return await connection.QuerySingleOrDefaultAsync<Unit>("SELECT * FROM \"Unit\" WHERE Id = @Id", new { Id = id });
			}
		}

		public async Task<long> AddUnitAsync(Unit unit)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "INSERT INTO \"Unit\" (UnitName, UnitDesc, DepartmentId, IsActive, CreationDate, CreatedUserBy) VALUES (@UnitName, @UnitDesc, @DepartmentId, @IsActive, @CreationDate, @CreatedUserBy) RETURNING Id";
			var insertedId = await connection.ExecuteScalarAsync<long>(query, unit);
			return insertedId;
		}

		public async Task UpdateUnitAsync(Unit unit)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "UPDATE \"Unit\" SET UnitName = @UnitName, UnitDesc = @UnitDesc, DepartmentId = @DepartmentId, IsActive = @IsActive, CreationDate = @CreationDate, CreatedUserBy = @CreatedUserBy WHERE Id = @Id";
			await connection.ExecuteAsync(query, unit);
		}

		public async Task DeleteUnitAsync(long id)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "DELETE FROM \"Unit\" WHERE Id = @Id";
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
