using GecisKontrol.Domain.Interfaces;
using GecisKontrol.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
	public class DepartmentService
	{
		private readonly IUnitOfWork _unitOfWork;

		public DepartmentService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
		{
			return await _unitOfWork._departmentRepository.GetAllDepartmentsAsync();
		}

		public async Task<Department> GetDepartmentByIdAsync(int id)
		{
			return await _unitOfWork._departmentRepository.GetDepartmentByIdAsync(id);
		}

		public async Task<int> AddDepartmentAsync(Department Department)
		{
			try
			{
				var DepartmentId = await _unitOfWork._departmentRepository.AddDepartmentAsync(Department);
				await _unitOfWork.CommitAsync();
				return DepartmentId;
			}
			catch (Exception)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		public async Task UpdateDepartmentAsync(Department Department)
		{
			try
			{
				await _unitOfWork._departmentRepository.UpdateDepartmentAsync(Department);
				await _unitOfWork.CommitAsync();
			}
			catch (Exception)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		public async Task DeleteDepartmentAsync(int id)
		{
			try
			{
				await _unitOfWork._departmentRepository.DeleteDepartmentAsync(id);
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
