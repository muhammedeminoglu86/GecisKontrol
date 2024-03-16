using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GecisKontrol.Domain.Interfaces;
using GecisKontrol.Domain.Model;
using System.Transactions;
using System.Data.Common;

namespace GecisKontrol.DAL.Data
{
	public class DepartmentRepository :IDepartmentRepository
	{
		private readonly DbContext _dbContext;

		public DepartmentRepository(DbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
		{
			using var connection = _dbContext.CreateConnection();
			{
				return await connection.QueryAsync<Department>("SELECT * FROM \"Department\"");
			}
		}

		public async Task<Department> GetDepartmentByIdAsync(int id)
		{
			using var connection = _dbContext.CreateConnection();
			{
				return await connection.QuerySingleOrDefaultAsync<Department>("SELECT * FROM \"Department\" WHERE Id = @Id", new { Id = id });
			}
		}

		public async Task<int> AddDepartmentAsync(Department department)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "INSERT INTO \"Department\" (DepartmentName, DepartmentDescription, CompanyId, IsActive, CreationDate, CreatedUserBy) VALUES (@DepartmentName, @DepartmentDescription, @CompanyId, @IsActive, @CreationDate, @CreatedUserBy) RETURNING Id";
			var insertedId = await connection.ExecuteScalarAsync<int>(query, department);
			return insertedId;
		}

		public async Task UpdateDepartmentAsync(Department department)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "UPDATE \"Department\" SET DepartmentName = @DepartmentName, DepartmentDescription = @DepartmentDescription, CompanyId = @CompanyId, IsActive = @IsActive, CreationDate = @CreationDate, CreatedUserBy = @CreatedUserBy WHERE Id = @Id";
			await connection.ExecuteAsync(query, department);
		}

		public async Task DeleteDepartmentAsync(int id)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "DELETE FROM \"Department\" WHERE Id = @Id";
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
