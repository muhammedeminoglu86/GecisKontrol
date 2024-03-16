using GecisKontrol.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GecisKontrol.Domain.Interfaces
{
	public interface IEmployeeCardMappingRepository
	{
		Task<IEnumerable<EmployeeCardMapping>> GetAllEmployeeCardMappingsAsync();
		Task<EmployeeCardMapping> GetEmployeeCardMappingByIdAsync(int id);
		Task<int> AddEmployeeCardMappingAsync(EmployeeCardMapping employeeCardMapping);
		Task UpdateEmployeeCardMappingAsync(EmployeeCardMapping employeeCardMapping);
		Task DeleteEmployeeCardMappingAsync(int id);
	}
}
