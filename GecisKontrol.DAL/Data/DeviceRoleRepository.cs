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
	public class DeviceRoleRepository :IDeviceRoleRepository
	{
		private readonly DbContext _dbContext;

		public DeviceRoleRepository(DbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<DeviceRole>> GetAllDeviceRolesAsync()
		{
			using var connection = _dbContext.CreateConnection();
			{
				return await connection.QueryAsync<DeviceRole>("SELECT * FROM \"DeviceRole\"");
			}
		}

		public async Task<DeviceRole> GetDeviceRoleByIdAsync(int id)
		{
			using var connection = _dbContext.CreateConnection();
			{
				return await connection.QuerySingleOrDefaultAsync<DeviceRole>("SELECT * FROM \"DeviceRole\" WHERE Id = @Id", new { Id = id });
			}
		}

		public async Task<int> AddDeviceRoleAsync(DeviceRole deviceRole)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "INSERT INTO \"DeviceRole\" (RoleName, RoleDescription, IsActive, CreationDate) VALUES (@RoleName, @RoleDescription, @IsActive, @CreationDate) RETURNING Id";
			var insertedId = await connection.ExecuteScalarAsync<int>(query, deviceRole);
			return insertedId;
		}

		public async Task UpdateDeviceRoleAsync(DeviceRole deviceRole)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "UPDATE \"DeviceRole\" SET RoleName = @RoleName, RoleDescription = @RoleDescription, IsActive = @IsActive, CreationDate = @CreationDate WHERE Id = @Id";
			await connection.ExecuteAsync(query, deviceRole);
		}

		public async Task DeleteDeviceRoleAsync(int id)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "DELETE FROM \"DeviceRole\" WHERE Id = @Id";
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
