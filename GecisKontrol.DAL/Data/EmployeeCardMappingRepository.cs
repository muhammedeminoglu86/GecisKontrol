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
	public class EmployeeCardMappingRepository : IEmployeeCardMappingRepository
	{
		private readonly DbContext _dbContext;

		public EmployeeCardMappingRepository(DbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<EmployeeCardMapping>> GetAllEmployeeCardMappingsAsync()
		{
			using var connection = _dbContext.CreateConnection();
			{
				return await connection.QueryAsync<EmployeeCardMapping>("SELECT * FROM \"EmployeeCardMapping\"");
			}
		}

		public async Task<EmployeeCardMapping> GetEmployeeCardMappingByIdAsync(int id)
		{
			using var connection = _dbContext.CreateConnection();
			{
				return await connection.QuerySingleOrDefaultAsync<EmployeeCardMapping>("SELECT * FROM \"EmployeeCardMapping\" WHERE Id = @Id", new { Id = id });
			}
		}

		public async Task<int> AddEmployeeCardMappingAsync(EmployeeCardMapping employeeCardMapping)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "INSERT INTO \"EmployeeCardMapping\" (CardId, EmployeeId, IsActive, CreatedUserById, CreationDate) VALUES (@CardId, @EmployeeId, @IsActive, @CreatedUserById, @CreationDate) RETURNING Id";
			var insertedId = await connection.ExecuteScalarAsync<int>(query, employeeCardMapping);
			return insertedId;
		}

		public async Task UpdateEmployeeCardMappingAsync(EmployeeCardMapping employeeCardMapping)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "UPDATE \"EmployeeCardMapping\" SET CardId = @CardId, EmployeeId = @EmployeeId, IsActive = @IsActive, CreatedUserById = @CreatedUserById, CreationDate = @CreationDate WHERE Id = @Id";
			await connection.ExecuteAsync(query, employeeCardMapping);
		}

		public async Task DeleteEmployeeCardMappingAsync(int id)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "DELETE FROM \"EmployeeCardMapping\" WHERE Id = @Id";
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
