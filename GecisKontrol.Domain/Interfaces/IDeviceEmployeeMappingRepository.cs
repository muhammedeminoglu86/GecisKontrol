using GecisKontrol.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GecisKontrol.Domain.Interfaces
{
	public interface IDeviceEmployeeMappingRepository
	{
		Task<IEnumerable<DeviceEmployeeMapping>> GetAllDeviceEmployeeMappingsAsync();
		Task<DeviceEmployeeMapping> GetDeviceEmployeeMappingByIdAsync(int id);
		Task<int> AddDeviceEmployeeMappingAsync(DeviceEmployeeMapping deviceEmployeeMapping);
		Task UpdateDeviceEmployeeMappingAsync(DeviceEmployeeMapping deviceEmployeeMapping);
		Task DeleteDeviceEmployeeMappingAsync(int id);
	}
}
