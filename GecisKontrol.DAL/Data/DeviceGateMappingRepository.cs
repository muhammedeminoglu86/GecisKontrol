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
	public class DeviceGateMappingRepository : IDeviceGateMappingRepository
	{
		private readonly DbContext _dbContext;

		public DeviceGateMappingRepository(DbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<DeviceGateMapping>> GetAllDeviceGateMappingsAsync()
		{
			using var connection = _dbContext.CreateConnection();
			{
				return await connection.QueryAsync<DeviceGateMapping>("SELECT * FROM \"DeviceGateMapping\"");
			}
		}

		public async Task<DeviceGateMapping> GetDeviceGateMappingByIdAsync(int id)
		{
			using var connection = _dbContext.CreateConnection();
			{
				return await connection.QuerySingleOrDefaultAsync<DeviceGateMapping>("SELECT * FROM \"DeviceGateMapping\" WHERE Id = @Id", new { Id = id });
			}
		}

		public async Task<int> AddDeviceGateMappingAsync(DeviceGateMapping deviceGateMapping)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "INSERT INTO \"DeviceGateMapping\" (DeviceId, GateId, IpAddress, SessionGuid, IsActive, LastOperation, CreatedUserBy, CreationDate, DeviceRoleId) VALUES (@DeviceId, @GateId, @IpAddress, @SessionGuid, @IsActive, @LastOperation, @CreatedUserBy, @CreationDate, @DeviceRoleId) RETURNING Id";
			var insertedId = await connection.ExecuteScalarAsync<int>(query, deviceGateMapping);
			return insertedId;
		}

		public async Task UpdateDeviceGateMappingAsync(DeviceGateMapping deviceGateMapping)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "UPDATE \"DeviceGateMapping\" SET DeviceId = @DeviceId, GateId = @GateId, IpAddress = @IpAddress, SessionGuid = @SessionGuid, IsActive = @IsActive, LastOperation = @LastOperation, CreatedUserBy = @CreatedUserBy, CreationDate = @CreationDate, DeviceRoleId = @DeviceRoleId WHERE Id = @Id";
			await connection.ExecuteAsync(query, deviceGateMapping);
		}

		public async Task DeleteDeviceGateMappingAsync(int id)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "DELETE FROM \"DeviceGateMapping\" WHERE Id = @Id";
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
