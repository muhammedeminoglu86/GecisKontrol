using GecisKontrol.Domain.Interfaces;
using GecisKontrol.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
	public class EmployeeService
	{
		private readonly IUnitOfWork _unitOfWork;

		public EmployeeService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
		{
			return await _unitOfWork._employeeRepository.GetAllEmployeesAsync();
		}

		public async Task<Employee> GetEmployeeByIdAsync(int id)
		{
			return await _unitOfWork._employeeRepository.GetEmployeeByIdAsync(id);
		}

		public async Task<int> AddEmployeeAsync(Employee Employee)
		{
			try
			{
				var EmployeeId = await _unitOfWork._employeeRepository.AddEmployeeAsync(Employee);
				await _unitOfWork.CommitAsync();
				return EmployeeId;
			}
			catch (Exception)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		public async Task UpdateEmployeeAsync(Employee Employee)
		{
			try
			{
				await _unitOfWork._employeeRepository.UpdateEmployeeAsync(Employee);
				await _unitOfWork.CommitAsync();
			}
			catch (Exception)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		public async Task DeleteEmployeeAsync(int id)
		{
			try
			{
				await _unitOfWork._employeeRepository.DeleteEmployeeAsync(id);
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
