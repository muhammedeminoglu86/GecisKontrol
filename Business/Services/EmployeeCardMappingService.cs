using GecisKontrol.Domain.Interfaces;
using GecisKontrol.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
	public class EmployeeCardMappingService
	{
		private readonly IUnitOfWork _unitOfWork;

		public EmployeeCardMappingService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<EmployeeCardMapping>> GetAllEmployeeCardMappingsAsync()
		{
			return await _unitOfWork._employeeCardMappingRepository.GetAllEmployeeCardMappingsAsync();
		}

		public async Task<EmployeeCardMapping> GetEmployeeCardMappingByIdAsync(int id)
		{
			return await _unitOfWork._employeeCardMappingRepository.GetEmployeeCardMappingByIdAsync(id);
		}

		public async Task<int> AddEmployeeCardMappingAsync(EmployeeCardMapping EmployeeCardMapping)
		{
			try
			{
				var EmployeeCardMappingId = await _unitOfWork._employeeCardMappingRepository.AddEmployeeCardMappingAsync(EmployeeCardMapping);
				await _unitOfWork.CommitAsync();
				return EmployeeCardMappingId;
			}
			catch (Exception)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		public async Task UpdateEmployeeCardMappingAsync(EmployeeCardMapping EmployeeCardMapping)
		{
			try
			{
				await _unitOfWork._employeeCardMappingRepository.UpdateEmployeeCardMappingAsync(EmployeeCardMapping);
				await _unitOfWork.CommitAsync();
			}
			catch (Exception)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		public async Task DeleteEmployeeCardMappingAsync(int id)
		{
			try
			{
				await _unitOfWork._employeeCardMappingRepository.DeleteEmployeeCardMappingAsync(id);
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
