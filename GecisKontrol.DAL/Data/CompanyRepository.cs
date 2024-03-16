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
	public class CompanyRepository : ICompanyRepository
	{
		private readonly DbContext _dbContext;

		public CompanyRepository(DbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
		{
			using var connection = _dbContext.CreateConnection();
			{
				return await connection.QueryAsync<Company>("SELECT * FROM \"Company\"");
			}
		}

		public async Task<Company> GetCompanyByIdAsync(int id)
		{
			using var connection = _dbContext.CreateConnection();
			{
				return await connection.QuerySingleOrDefaultAsync<Company>("SELECT * FROM \"Company\" WHERE Id = @Id", new { Id = id });
			}
		}

		public async Task<int> AddCompanyAsync(Company company)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "INSERT INTO \"Company\" (CompanyName, Description, Logo, IsActive, CreationDate, CreatedUserBy) VALUES (@CompanyName, @Description, @Logo, @IsActive, @CreationDate, @CreatedUserBy) RETURNING Id";
			var insertedId = await connection.ExecuteScalarAsync<int>(query, company);
			return insertedId;
		}

		public async Task UpdateCompanyAsync(Company company)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "UPDATE \"Company\" SET CompanyName = @CompanyName, Description = @Description, Logo = @Logo, IsActive = @IsActive, CreationDate = @CreationDate, CreatedUserBy = @CreatedUserBy WHERE Id = @Id";
			await connection.ExecuteAsync(query, company);
		}

		public async Task DeleteCompanyAsync(int id)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "DELETE FROM \"Company\" WHERE Id = @Id";
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
