using GecisKontrol.Domain.Interfaces;
using GecisKontrol.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
	public class DeviceRoleService
	{
		private readonly IUnitOfWork _unitOfWork;

		public DeviceRoleService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<DeviceRole>> GetAllDeviceRolesAsync()
		{
			return await _unitOfWork._deviceRoleRepository.GetAllDeviceRolesAsync();
		}

		public async Task<DeviceRole> GetDeviceRoleByIdAsync(int id)
		{
			return await _unitOfWork._deviceRoleRepository.GetDeviceRoleByIdAsync(id);
		}

		public async Task<int> AddDeviceRoleAsync(DeviceRole DeviceRole)
		{
			try
			{
				var DeviceRoleId = await _unitOfWork._deviceRoleRepository.AddDeviceRoleAsync(DeviceRole);
				await _unitOfWork.CommitAsync();
				return DeviceRoleId;
			}
			catch (Exception)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		public async Task UpdateDeviceRoleAsync(DeviceRole DeviceRole)
		{
			try
			{
				await _unitOfWork._deviceRoleRepository.UpdateDeviceRoleAsync(DeviceRole);
				await _unitOfWork.CommitAsync();
			}
			catch (Exception)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		public async Task DeleteDeviceRoleAsync(int id)
		{
			try
			{
				await _unitOfWork._deviceRoleRepository.DeleteDeviceRoleAsync(id);
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
