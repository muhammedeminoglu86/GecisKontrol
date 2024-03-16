using GecisKontrol.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using GecisKontrol.Domain.Interfaces;
using System.Transactions;
using System.Data.Common;

namespace GecisKontrol.DAL.Data
{
	public class DeviceEmployeeMappingRepository : IDeviceEmployeeMappingRepository
	{
		private readonly DbContext _dbContext;

		public DeviceEmployeeMappingRepository(DbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<DeviceEmployeeMapping>> GetAllDeviceEmployeeMappingsAsync()
		{
			using var connection = _dbContext.CreateConnection();
			{
				return await connection.QueryAsync<DeviceEmployeeMapping>("SELECT * FROM \"DeviceEmployeeMapping\"");
			}
		}

		public async Task<DeviceEmployeeMapping> GetDeviceEmployeeMappingByIdAsync(int id)
		{
			using var connection = _dbContext.CreateConnection();
			{
				return await connection.QuerySingleOrDefaultAsync<DeviceEmployeeMapping>("SELECT * FROM \"DeviceEmployeeMapping\" WHERE Id = @Id", new { Id = id });
			}
		}

		public async Task<int> AddDeviceEmployeeMappingAsync(DeviceEmployeeMapping deviceEmployeeMapping)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "INSERT INTO \"DeviceEmployeeMapping\" (Name, Description, HostName, MacAddress, ProtocolType, DeviceRoleId, IsActive, CreationDate, CreatedUserBy) VALUES (@Name, @Description, @HostName, @MacAddress, @ProtocolType, @DeviceRoleId, @IsActive, @CreationDate, @CreatedUserBy) RETURNING Id";
			var insertedId = await connection.ExecuteScalarAsync<int>(query, deviceEmployeeMapping);
			return insertedId;
		}

		public async Task UpdateDeviceEmployeeMappingAsync(DeviceEmployeeMapping deviceEmployeeMapping)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "UPDATE \"DeviceEmployeeMapping\" SET Name = @Name, Description = @Description, HostName = @HostName, MacAddress = @MacAddress, ProtocolType = @ProtocolType, DeviceRoleId = @DeviceRoleId, IsActive = @IsActive, CreationDate = @CreationDate, CreatedUserBy = @CreatedUserBy WHERE Id = @Id";
			await connection.ExecuteAsync(query, deviceEmployeeMapping);
		}

		public async Task DeleteDeviceEmployeeMappingAsync(int id)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "DELETE FROM \"DeviceEmployeeMapping\" WHERE Id = @Id";
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
