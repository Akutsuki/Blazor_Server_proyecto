using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace Blazor_Server.Services.Employee_S
{
	public class EmployeeService : IEmployeeService
	{

		private readonly HttpClient httpClient;

		public EmployeeService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}
		public async Task<Employee> AddEmployee(Employee employee)
		{
			var response = await httpClient.PostAsJsonAsync("api/employee", employee);
			return await response.Content.ReadFromJsonAsync<Employee>();
		}


		public async Task DeleteEmployee(int employeeId)
		{
			await httpClient.DeleteAsync($"/api/employee/{employeeId}");
		}

		public async Task<Employee> GetEmployee(int employeeId)
		{
			return await httpClient.GetFromJsonAsync<Employee>($"/api/employee/{employeeId}");
		}

		public Task<Employee> GetEmployeeByEmail(string email)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Employee>> GetEmployees()
		{
			return await httpClient.GetFromJsonAsync<IEnumerable<Employee>>("api/employee");
			// return await httpClient.GetJsonAsync<Employee[]>("api/employee");
		}

		public Task<IEnumerable<Employee>> Search(string name, Gender? gender)
		{
			throw new NotImplementedException();
		}

		public async Task<Employee> UpdateEmployee(Employee employee)
		{
			var response = await httpClient
		   .PutAsJsonAsync<Employee>($"/api/employee/{employee.EmployeeId}", employee);
			return await response.Content.ReadFromJsonAsync<Employee>();
		}

	}
}
