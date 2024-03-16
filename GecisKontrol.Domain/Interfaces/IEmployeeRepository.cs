﻿using GecisKontrol.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GecisKontrol.Domain.Interfaces
{
	public interface IEmployeeRepository
	{
		Task<IEnumerable<Employee>> GetAllEmployeesAsync();
		Task<Employee> GetEmployeeByIdAsync(int id);
		Task<int> AddEmployeeAsync(Employee employee);
		Task UpdateEmployeeAsync(Employee employee);
		Task DeleteEmployeeAsync(int id);
	}
}
