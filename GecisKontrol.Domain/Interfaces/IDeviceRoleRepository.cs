using GecisKontrol.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GecisKontrol.Domain.Interfaces
{
	public interface IDeviceRoleRepository
	{
		Task<IEnumerable<DeviceRole>> GetAllDeviceRolesAsync();
		Task<DeviceRole> GetDeviceRoleByIdAsync(int id);
		Task<int> AddDeviceRoleAsync(DeviceRole deviceRole);
		Task UpdateDeviceRoleAsync(DeviceRole deviceRole);
		Task DeleteDeviceRoleAsync(int id);
	}
}
