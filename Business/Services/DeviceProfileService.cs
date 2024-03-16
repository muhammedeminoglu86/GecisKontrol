using GecisKontrol.Domain.Interfaces;
using GecisKontrol.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
	public class DeviceProfileService
	{
		private readonly IUnitOfWork _unitOfWork;

		public DeviceProfileService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<DeviceProfile>> GetAllDeviceProfilesAsync()
		{
			return await _unitOfWork._deviceProfileRepository.GetAllDeviceProfilesAsync();
		}

		public async Task<DeviceProfile> GetDeviceProfileByIdAsync(int id)
		{
			return await _unitOfWork._deviceProfileRepository.GetDeviceProfileByIdAsync(id);
		}

		public async Task<int> AddDeviceProfileAsync(DeviceProfile DeviceProfile)
		{
			try
			{
				var DeviceProfileId = await _unitOfWork._deviceProfileRepository.AddDeviceProfileAsync(DeviceProfile);
				await _unitOfWork.CommitAsync();
				return DeviceProfileId;
			}
			catch (Exception)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		public async Task UpdateDeviceProfileAsync(DeviceProfile DeviceProfile)
		{
			try
			{
				await _unitOfWork._deviceProfileRepository.UpdateDeviceProfileAsync(DeviceProfile);
				await _unitOfWork.CommitAsync();
			}
			catch (Exception)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		public async Task DeleteDeviceProfileAsync(int id)
		{
			try
			{
				await _unitOfWork._deviceProfileRepository.DeleteDeviceProfileAsync(id);
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
