using GecisKontrol.Domain.Interfaces;
using GecisKontrol.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
	public class DeviceEmployeeMappingService
	{
		private readonly IUnitOfWork _unitOfWork;

		public DeviceEmployeeMappingService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<DeviceEmployeeMapping>> GetAllDeviceEmployeeMappingsAsync()
		{
			return await _unitOfWork._deviceEmployeeMappingRepository.GetAllDeviceEmployeeMappingsAsync();
		}

		public async Task<DeviceEmployeeMapping> GetDeviceEmployeeMappingByIdAsync(int id)
		{
			return await _unitOfWork._deviceEmployeeMappingRepository.GetDeviceEmployeeMappingByIdAsync(id);
		}

		public async Task<int> AddDeviceEmployeeMappingAsync(DeviceEmployeeMapping DeviceEmployeeMapping)
		{
			try
			{
				var DeviceEmployeeMappingId = await _unitOfWork._deviceEmployeeMappingRepository.AddDeviceEmployeeMappingAsync(DeviceEmployeeMapping);
				await _unitOfWork.CommitAsync();
				return DeviceEmployeeMappingId;
			}
			catch (Exception)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		public async Task UpdateDeviceEmployeeMappingAsync(DeviceEmployeeMapping DeviceEmployeeMapping)
		{
			try
			{
				await _unitOfWork._deviceEmployeeMappingRepository.UpdateDeviceEmployeeMappingAsync(DeviceEmployeeMapping);
				await _unitOfWork.CommitAsync();
			}
			catch (Exception)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		public async Task DeleteDeviceEmployeeMappingAsync(int id)
		{
			try
			{
				await _unitOfWork._deviceEmployeeMappingRepository.DeleteDeviceEmployeeMappingAsync(id);
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
