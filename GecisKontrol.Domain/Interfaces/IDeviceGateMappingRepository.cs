using GecisKontrol.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GecisKontrol.Domain.Interfaces
{
	public interface IDeviceGateMappingRepository
	{
		Task<IEnumerable<DeviceGateMapping>> GetAllDeviceGateMappingsAsync();
		Task<DeviceGateMapping> GetDeviceGateMappingByIdAsync(int id);
		Task<int> AddDeviceGateMappingAsync(DeviceGateMapping deviceGateMapping);
		Task UpdateDeviceGateMappingAsync(DeviceGateMapping deviceGateMapping);
		Task DeleteDeviceGateMappingAsync(int id);
	}
}
