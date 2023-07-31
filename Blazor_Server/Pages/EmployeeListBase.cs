using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using Blazor_Server.Services.Employee_S;

namespace Blazor_Server.Pages
{
	public class EmployeeListBase : ComponentBase
	{
		[Inject]
		public IEmployeeService EmployeeService { get; set; }
		[Inject]
		public NavigationManager NavigationManager { get; set; }
		public IEnumerable<Employee> Employees { get; set; }
		[Parameter]
		public Employee Employee { get; set; }//= new Employee();
		public EventCallback<int> OnEmployeeDeleted { get; set; }

		[Parameter]
		public string Id { get; set; }
		protected override async Task OnInitializedAsync()
		{
			
			Employees = (await EmployeeService.GetEmployees()).ToList();
			//await Task.Run(LoadEmployees);
		}

		protected async Task Delete_Click(int id)
		{
			int.TryParse(Id, out int employeeId);
			//await EmployeeService.DeleteEmployee(Employee.EmployeeId);
			await EmployeeService.DeleteEmployee(id);
			//await OnEmployeeDeleted.InvokeAsync(Employee.EmployeeId);
			NavigationManager.NavigateTo("/",true);
		}
		//protected async Task EmployeeDeleted()
		//{
		//	Employees = (await EmployeeService.GetEmployees()).ToList();
		//}

		/*	private void LoadEmployees()
			{
				System.Threading.Thread.Sleep(2000);
				Employee e1 = new Employee
				{
					EmployeeId = 1,
					FirstName = "John",
					LastName = "Hastings",
					Email = "David@pragimtech.com",
					DateOfBrith = new DateTime(1980, 10, 5),
					Gender = Gender.M,
					Department = new Department { DepartmentId = 1, DepartmentName = "IT" },
					PhotoPath = "img/2.png"
				};

				Employee e2 = new Employee
				{
					EmployeeId = 2,
					FirstName = "Sam",
					LastName = "Galloway",
					Email = "Sam@pragimtech.com",
					DateOfBrith = new DateTime(1981, 12, 22),
					Gender = Gender.M,
					Department = new Department { DepartmentId = 2, DepartmentName = "HR" },
					PhotoPath = "img/3.jpg"
				};

				Employee e3 = new Employee
				{
					EmployeeId = 3,
					FirstName = "Mary",
					LastName = "Smith",
					Email = "mary@pragimtech.com",
					DateOfBrith = new DateTime(1979, 11, 11),
					Gender = Gender.F,
					Department = new Department { DepartmentId = 1, DepartmentName = "IT" },
					PhotoPath = "img/1.png"
				};

				Employee e4 = new Employee
				{
					EmployeeId = 4,
					FirstName = "Sara",
					LastName = "Longway",
					Email = "sara@pragimtech.com",
					DateOfBrith = new DateTime(1982, 9, 23),
					Gender = Gender.F,
					Department = new Department { DepartmentId = 3, DepartmentName = "Payroll" },
					PhotoPath = "img/4.png"
				};

				Employees = new List<Employee> { e1, e2, e3, e4 };
			}*/
	}
}
