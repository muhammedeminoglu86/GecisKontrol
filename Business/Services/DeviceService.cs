using GecisKontrol.Domain.Interfaces;
using GecisKontrol.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
	public class DeviceService
	{
		private readonly IUnitOfWork _unitOfWork;

		public DeviceService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<Device>> GetAllDevicesAsync()
		{
			return await _unitOfWork._deviceRepository.GetAllDevicesAsync();
		}

		public async Task<Device> GetDeviceByIdAsync(int id)
		{
			return await _unitOfWork._deviceRepository.GetDeviceByIdAsync(id);
		}

		public async Task<int> AddDeviceAsync(Device Device)
		{
			try
			{
				var DeviceId = await _unitOfWork._deviceRepository.AddDeviceAsync(Device);
				await _unitOfWork.CommitAsync();
				return DeviceId;
			}
			catch (Exception)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		public async Task UpdateDeviceAsync(Device Device)
		{
			try
			{
				await _unitOfWork._deviceRepository.UpdateDeviceAsync(Device);
				await _unitOfWork.CommitAsync();
			}
			catch (Exception)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		public async Task DeleteDeviceAsync(int id)
		{
			try
			{
				await _unitOfWork._deviceRepository.DeleteDeviceAsync(id);
				await _unitOfWork.CommitAsync();
			}
			catch (Exception)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}
	}
}
