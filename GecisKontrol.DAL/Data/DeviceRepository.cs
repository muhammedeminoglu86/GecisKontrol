using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GecisKontrol.Domain.Model;
using System.Transactions;
using System.Data.Common;
using GecisKontrol.Domain.Interfaces;

namespace GecisKontrol.DAL.Data
{
	public class DeviceRepository : IDeviceRepository
	{
		private readonly DbContext _dbContext;

		public DeviceRepository(DbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<Device>> GetAllDevicesAsync()
		{
			using var connection = _dbContext.CreateConnection();
			{
				return await connection.QueryAsync<Device>("SELECT * FROM \"Device\"");
			}
		}

		public async Task<Device> GetDeviceByIdAsync(int id)
		{
			using var connection = _dbContext.CreateConnection();
			{
				return await connection.QuerySingleOrDefaultAsync<Device>("SELECT * FROM \"Device\" WHERE Id = @Id", new { Id = id });
			}
		}

		public async Task<int> AddDeviceAsync(Device device)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "INSERT INTO \"Device\" (Name, Description, HostName, MacAddress, ProtocolType, DeviceRoleId, IsActive, CreationDate, CreatedUserBy) VALUES (@Name, @Description, @HostName, @MacAddress, @ProtocolType, @DeviceRoleId, @IsActive, @CreationDate, @CreatedUserBy) RETURNING Id";
			var insertedId = await connection.ExecuteScalarAsync<int>(query, device);
			return insertedId;
		}

		public async Task UpdateDeviceAsync(Device device)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "UPDATE \"Device\" SET Name = @Name, Description = @Description, HostName = @HostName, MacAddress = @MacAddress, ProtocolType = @ProtocolType, DeviceRoleId = @DeviceRoleId, IsActive = @IsActive, CreationDate = @CreationDate, CreatedUserBy = @CreatedUserBy WHERE Id = @Id";
			await connection.ExecuteAsync(query, device);
		}

		public async Task DeleteDeviceAsync(int id)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "DELETE FROM \"Device\" WHERE Id = @Id";
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
