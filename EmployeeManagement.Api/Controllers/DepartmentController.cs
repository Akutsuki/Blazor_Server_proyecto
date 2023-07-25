using EmployeeManagement.Api.Repositories.E_Department;
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
	public class DepartmentController : ControllerBase
	{
		private readonly IDepartmentRepository departmentRepository;

		public DepartmentController(IDepartmentRepository departmentRepository)
		{
			this.departmentRepository = departmentRepository;
		}
		[HttpGet]
		public async Task<ActionResult> GetDepartments()
		{
			try
			{
				return Ok(await departmentRepository.GetDepartments());
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error retrieving data from the database");
			}
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<Department>> GetDepartment(int id)
		{
			try
			{
				var result = await departmentRepository.GetDepartment(id);

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
	}
}
