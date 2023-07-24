using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Repositories.R_Employee
{
	public interface IEmployeeRepository
	{
		Task<IEnumerable<Employee>> GetEmployees();
		Task<Employee> GetEmployee(int employeeId);
		Task<Employee> AddEmployee(Employee employee);
		Task<Employee> UpdateEmployee(Employee employee);
		Task DeleteEmployee(int employeeId);
		Task<IEnumerable<Employee>> Search(string name, Gender? gender);
	}
}
