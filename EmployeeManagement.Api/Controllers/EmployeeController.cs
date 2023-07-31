using EmployeeManagement.Api.Repositories.R_Employee;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		private readonly IEmployeeRepository employeeRepository;

		public object Configuration { get; private set; }

		public EmployeeController(IEmployeeRepository employeeRepository)
		{
			this.employeeRepository = employeeRepository;
		}

		[HttpGet]
		public async Task<ActionResult> GetEmployees()
		{
			try
			{
				return Ok(await employeeRepository.GetEmployees());
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error retrieving data from the database");
			}
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<Employee>> GetEmployee(int id)
		{
			try
			{
				var result = await employeeRepository.GetEmployee(id);

				if (result == null)
				{
					return NotFound();
				}

				return result;
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error retrieving data from the database");
			}
		}
		[HttpPost]
		public async Task<ActionResult<Employee>> Add(Employee employee)
		{
			try
			{
				if (employee == null)
					return BadRequest();

				

				var createdEmployee = await employeeRepository.AddEmployee(employee);
				//return Ok(createdEmployee);
				return CreatedAtAction(nameof(GetEmployee),
			    new { id = createdEmployee.EmployeeId }, createdEmployee);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error creating new employee record");
			}
		}

		[HttpPut()]
		public async Task<ActionResult<Employee>> UpdateEmployee( Employee employee)
		{
			try
			{
				//if (id != employee.EmployeeId)
				//	return BadRequest("Employee ID mismatch");

				var employeeToUpdate = await employeeRepository.GetEmployee(employee.EmployeeId);

				if (employeeToUpdate == null)
				{
					return NotFound($"Employee with Id = {employee.EmployeeId} not found");
				}

				return await employeeRepository.UpdateEmployee(employee);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error updating employee record");
			}
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult> DeleteEmployee(int id)
		{
			try
			{
				var employeeToDelete = await employeeRepository.GetEmployee(id);

				if (employeeToDelete == null)
				{
					return NotFound($"Employee with Id = {id} not found");
				}

				await employeeRepository.DeleteEmployee(id);

				return Ok($"Employee with Id = {id} deleted");
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error deleting employee record");
			}
		}
		[HttpGet("{search}")]
		public async Task<ActionResult<IEnumerable<Employee>>> Search(string name, Gender? gender)
		{
			try
			{
				var result = await employeeRepository.Search(name, gender);

				if (result.Any())
				{
					return Ok(result);
				}

				return NotFound();
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
				"Error retrieving data from the database");
			}
		}

		/*[HttpPatch("{id:int}")]
		public async Task<ActionResult<Employee>> PatchEmployee(Employee employee)
		{
			try
			{
				//if (id != employee.EmployeeId)
				//	return BadRequest("Employee ID mismatch");

				var employeeToUpdate = await employeeRepository.GetEmployee(employee.EmployeeId);

				if (employeeToUpdate == null)
				{
					return NotFound($"Employee with Id = {employee.EmployeeId} not found");
				}

				return await employeeRepository.UpdateEmployee(employee);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error updating employee record");
			}
		}*/

	}
}
