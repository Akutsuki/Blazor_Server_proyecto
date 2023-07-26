using Blazor_Server.Models;
using Blazor_Server.Services.Department_S;
using Blazor_Server.Services.Employee_S;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_Server.Pages
{
	public class EditEmployeeBase : ComponentBase
    { 
        public Employee Employee { get; set; } = new Employee();

      //  public EditEmployeeModel EditEmployeeModel { get; set; } = new EditEmployeeModel();

        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Inject]
        public IDepartmentService DepartmentService { get; set; }

        public List<Department> Departments { get; set; } = new List<Department>();

        public string DepartmentId { get; set; }

        [Parameter]
        public string Id { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public string PageHeader { get; set; }
        protected async override Task OnInitializedAsync()
        {
     

            int.TryParse(Id, out int employeeId);

            if (employeeId != 0)
            {
                PageHeader = "Edit Employee";
                Employee = await EmployeeService.GetEmployee(int.Parse(Id));
            }
            else
            {
                PageHeader = "Create Employee";
                Employee = new Employee
                {
                    DepartmentId = 1,
                    DateOfBrith = DateTime.Now,
                    PhotoPath = "img/nophoto.jpg"
                };
            }
            Departments = (await DepartmentService.GetDepartments()).ToList();
            // DepartmentId = Employee.DepartmentId.ToString();
/*
            EditEmployeeModel.EmployeeId = Employee.EmployeeId;
            EditEmployeeModel.FirstName = Employee.FirstName;
            EditEmployeeModel.LastName = Employee.LastName;
            EditEmployeeModel.Email = Employee.Email;
            EditEmployeeModel.DateOfBrith = Employee.DateOfBrith;
            EditEmployeeModel.Gender = Employee.Gender;
            EditEmployeeModel.PhotoPath = Employee.PhotoPath;
            EditEmployeeModel.DepartmentId = Employee.DepartmentId;
            EditEmployeeModel.Department = Employee.Department;
*/
        }
        protected async Task HandleValidSubmit()
        {
            Employee result = null;

            if (Employee.EmployeeId != 0)
            {
                result = await EmployeeService.UpdateEmployee(Employee);
            }
            else
            {
                result = await EmployeeService.AddEmployee(Employee);
            }
            if (result != null)
            {
                NavigationManager.NavigateTo("/");
            }
        }

        protected async Task Delete_Click()
        {
            await EmployeeService.DeleteEmployee(Employee.EmployeeId);
            NavigationManager.NavigateTo("/");
        }
    }
}
