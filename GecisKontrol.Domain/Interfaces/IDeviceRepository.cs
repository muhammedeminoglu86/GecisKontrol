﻿using GecisKontrol.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GecisKontrol.Domain.Interfaces
{
	public interface IDeviceRepository
	{
		Task<IEnumerable<Device>> GetAllDevicesAsync();
		Task<Device> GetDeviceByIdAsync(int id);
		Task<int> AddDeviceAsync(Device device);
		Task UpdateDeviceAsync(Device device);
		Task DeleteDeviceAsync(int id);
	}
}
