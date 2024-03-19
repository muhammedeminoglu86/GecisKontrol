using Dapper;
using GecisKontrol.Domain.Model;
using GecisKontrol.Domain.Interfaces;
using System.Data;


namespace GecisKontrol.DAL.Data
{
	public class UserRepository : IUserRepository
	{
		private readonly DbContext _dbContext;


		public UserRepository(DbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<User>> GetAllUsersAsync(string query, object param)
		{
			using var connection = _dbContext.CreateConnection();
			{
				return await connection.QueryAsync<User>(query,param);
			}
		}

		public async Task<User> GetUserByIdAsync(string query, object param)
		{
			using var connection = _dbContext.CreateConnection();
			{
				return await connection.QuerySingleOrDefaultAsync<User>(query,param);

            }
		}

		public async Task<int> AddUserAsync(User user)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "INSERT INTO \"User\" (Username, Password, Email, IsActive, CreationDate, CreatedUserId) VALUES (@Username, @Password, @Email, @IsActive, @CreationDate, @CreatedUserId) RETURNING Id";
			var insertedId = await connection.ExecuteScalarAsync<int>(query, user);
			return insertedId;
		}
		public async Task UpdateUserAsync(User user)
		{
			using var connection = _dbContext.CreateConnection();
			var query =
					"UPDATE \"User\" SET Username = @Username, Password = @Password, Email = @Email, IsActive = @IsActive, CreationDate = @CreationDate, CreatedUserId = @CreatedUserId WHERE Id = @Id";
			await connection.ExecuteAsync(query, user);
		}

		public async Task DeleteUserAsync(int id)
		{
			using var connection = _dbContext.CreateConnection();
				var query = "DELETE FROM \"User\" WHERE Id = @Id";
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
