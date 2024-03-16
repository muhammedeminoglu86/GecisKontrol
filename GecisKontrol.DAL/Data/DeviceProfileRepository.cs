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
	public class DeviceProfileRepository : IDeviceProfileRepository
	{
		private readonly DbContext _dbContext;

		public DeviceProfileRepository(DbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<DeviceProfile>> GetAllDeviceProfilesAsync()
		{
			using var connection = _dbContext.CreateConnection();
			{
				return await connection.QueryAsync<DeviceProfile>("SELECT * FROM \"DeviceProfile\"");
			}
		}

		public async Task<DeviceProfile> GetDeviceProfileByIdAsync(int id)
		{
			using var connection = _dbContext.CreateConnection();
			{
				return await connection.QuerySingleOrDefaultAsync<DeviceProfile>("SELECT * FROM \"DeviceProfile\" WHERE Id = @Id", new { Id = id });
			}
		}

		public async Task<int> AddDeviceProfileAsync(DeviceProfile deviceProfile)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "INSERT INTO \"DeviceProfile\" (DeviceId, OfflineMode, WeekendMode, TimeLimited, StarTime, EndTime, IsActive, CreationDate, CreatedUserBy) VALUES (@DeviceId, @OfflineMode, @WeekendMode, @TimeLimited, @StarTime, @EndTime, @IsActive, @CreationDate, @CreatedUserBy) RETURNING Id";
			var insertedId = await connection.ExecuteScalarAsync<int>(query, deviceProfile);
			return insertedId;
		}

		public async Task UpdateDeviceProfileAsync(DeviceProfile deviceProfile)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "UPDATE \"DeviceProfile\" SET DeviceId = @DeviceId, OfflineMode = @OfflineMode, WeekendMode = @WeekendMode, TimeLimited = @TimeLimited, StarTime = @StarTime, EndTime = @EndTime, IsActive = @IsActive, CreationDate = @CreationDate, CreatedUserBy = @CreatedUserBy WHERE Id = @Id";
			await connection.ExecuteAsync(query, deviceProfile);
		}

		public async Task DeleteDeviceProfileAsync(int id)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "DELETE FROM \"DeviceProfile\" WHERE Id = @Id";
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
