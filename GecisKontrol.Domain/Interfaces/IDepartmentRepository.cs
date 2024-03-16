using GecisKontrol.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GecisKontrol.Domain.Interfaces
{
	public interface IDepartmentRepository
	{
		Task<IEnumerable<Department>> GetAllDepartmentsAsync();
		Task<Department> GetDepartmentByIdAsync(int id);
		Task<int> AddDepartmentAsync(Department department);
		Task UpdateDepartmentAsync(Department department);
		Task DeleteDepartmentAsync(int id);
	}
}
