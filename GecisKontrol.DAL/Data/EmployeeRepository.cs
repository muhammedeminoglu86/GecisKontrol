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
	public class EmployeeRepository : IEmployeeRepository
	{
		private readonly DbContext _dbContext;

		public EmployeeRepository(DbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
		{
			using var connection = _dbContext.CreateConnection();
			{
				return await connection.QueryAsync<Employee>("SELECT * FROM \"Employee\"");
			}
		}

		public async Task<Employee> GetEmployeeByIdAsync(int id)
		{
			using var connection = _dbContext.CreateConnection();
			{
				return await connection.QuerySingleOrDefaultAsync<Employee>("SELECT * FROM \"Employee\" WHERE Id = @Id", new { Id = id });
			}
		}

		public async Task<int> AddEmployeeAsync(Employee employee)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "INSERT INTO \"Employee\" (Name, NationalId, CompanyGeneratedId, Photo, IsActive, CreatedUserBy, CompanyId, CreationDate) VALUES (@Name, @NationalId, @CompanyGeneratedId, @Photo, @IsActive, @CreatedUserBy, @CompanyId, @CreationDate) RETURNING Id";
			var insertedId = await connection.ExecuteScalarAsync<int>(query, employee);
			return insertedId;
		}

		public async Task UpdateEmployeeAsync(Employee employee)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "UPDATE \"Employee\" SET Name = @Name, NationalId = @NationalId, CompanyGeneratedId = @CompanyGeneratedId, Photo = @Photo, IsActive = @IsActive, CreatedUserBy = @CreatedUserBy, CompanyId = @CompanyId, CreationDate = @CreationDate WHERE Id = @Id";
			await connection.ExecuteAsync(query, employee);
		}

		public async Task DeleteEmployeeAsync(int id)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "DELETE FROM \"Employee\" WHERE Id = @Id";
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
