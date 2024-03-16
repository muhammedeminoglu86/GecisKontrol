using GecisKontrol.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GecisKontrol.Domain.Interfaces
{
	public interface IDeviceProfileRepository
	{
		Task<IEnumerable<DeviceProfile>> GetAllDeviceProfilesAsync();
		Task<DeviceProfile> GetDeviceProfileByIdAsync(int id);
		Task<int> AddDeviceProfileAsync(DeviceProfile deviceProfile);
		Task UpdateDeviceProfileAsync(DeviceProfile deviceProfile);
		Task DeleteDeviceProfileAsync(int id);
	}
}
