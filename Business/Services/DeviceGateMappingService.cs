using GecisKontrol.Domain.Interfaces;
using GecisKontrol.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
	public class DeviceGateMappingService
	{
		private readonly IUnitOfWork _unitOfWork;

		public DeviceGateMappingService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<DeviceGateMapping>> GetAllDeviceGateMappingsAsync()
		{
			return await _unitOfWork._deviceGateMappingRepository.GetAllDeviceGateMappingsAsync();
		}

		public async Task<DeviceGateMapping> GetDeviceGateMappingByIdAsync(int id)
		{
			return await _unitOfWork._deviceGateMappingRepository.GetDeviceGateMappingByIdAsync(id);
		}

		public async Task<int> AddDeviceGateMappingAsync(DeviceGateMapping DeviceGateMapping)
		{
			try
			{
				var DeviceGateMappingId = await _unitOfWork._deviceGateMappingRepository.AddDeviceGateMappingAsync(DeviceGateMapping);
				await _unitOfWork.CommitAsync();
				return DeviceGateMappingId;
			}
			catch (Exception)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		public async Task UpdateDeviceGateMappingAsync(DeviceGateMapping DeviceGateMapping)
		{
			try
			{
				await _unitOfWork._deviceGateMappingRepository.UpdateDeviceGateMappingAsync(DeviceGateMapping);
				await _unitOfWork.CommitAsync();
			}
			catch (Exception)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		public async Task DeleteDeviceGateMappingAsync(int id)
		{
			try
			{
				await _unitOfWork._deviceGateMappingRepository.DeleteDeviceGateMappingAsync(id);
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
