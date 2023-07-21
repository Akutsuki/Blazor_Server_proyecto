using EmployeeManagement.Api.Repositories.E_Department;
using EmployeeManagement.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Repositories.R_Employee
{
	public class EmployeeRepository : IEmployeeRepository
	{
		private IDepartmentRepository departmentRepository;
		public IConfiguration Configuration { get; }
		public EmployeeRepository(IConfiguration _configuration,
		                 	     IDepartmentRepository _departmentRepository)
		{
			Configuration = _configuration;
			departmentRepository = _departmentRepository;

			//connectionString = _configuration.GetConnectionString("BlazorDBConnection");
		}

		public async Task<IEnumerable<Employee>> GetEmployees()
		{
			Employee employee = null;
			List<Employee> listEmployees = new List<Employee>();
			string cs = Configuration["ConnectionStrings:BlazorDBConnection"];
			using (SqlConnection con = new SqlConnection(cs))
			{
				SqlCommand cmd = new SqlCommand("sp_GetEmployees", con);
				cmd.CommandType = CommandType.StoredProcedure;
				await con.OpenAsync();

				SqlDataReader rdr = await cmd.ExecuteReaderAsync();
				//SqlDataReader rdr = cmd.ExecuteReader();
				while (rdr.Read())
				{

					employee = new Employee();
					employee.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
					employee.FirstName = rdr["FirstName"].ToString();
					employee.LastName = rdr["LastName"].ToString();
					employee.Email = rdr["Email"].ToString();
					employee.DateOfBrith = Convert.ToDateTime(rdr["DateOfBirth"]);
					char genderChar = Convert.ToChar(rdr["Gender"]);
					if (Enum.TryParse<Gender>(genderChar.ToString(), out Gender gender))
					{
						if (gender == Gender.M)
						{
							employee.Gender = Gender.M;
						}
						else
						{
							employee.Gender = Gender.F;
						}
					}
					//employee.Gender = (Gender)Convert.ToChar(rdr["Gender"]);
					//char genderChar = Convert.ToChar(rdr["Gender"]);
					//Gender gender = (Gender)genderChar;
					//if (gender == Gender.M)
					//{
					//	employee.Gender = Gender.M;
					//}
					//else
					//{
					//	employee.Gender = Gender.F;
					//}
					//if (Enum.TryParse(genderChar.ToString(), out Gender gender))
					//{
					//	employee.Gender = gender;
					//}
					//else
					//{
					//	// Manejar el valor no válido del género según sea necesario
					//	// Por ejemplo, puedes asignar un valor predeterminado o lanzar una excepción.
					//	employee.Gender = Gender.Unknown; // Asignar un valor predeterminado
					//									  // o
					//	throw new InvalidDataException("Valor no válido para el género."); // Lanzar una excepción
					//}
					employee.PhotoPath = rdr["PhotoPath"].ToString();
					//employee.DepartmentId = Convert.ToInt32(rdr["DepartmentId"]);
					// Cargar el departamento del empleado
					//int departmentId = Convert.ToInt32(rdr["DepartmentId"]);
					//string departmentName = rdr["DepartmentName"].ToString();
					Department department = await departmentRepository.GetDepartment(employee.EmployeeId);

					employee.Department = department;
					//employee.DepartmentName = department.DepartmentName;
					listEmployees.Add(employee);

				}

			}


			return listEmployees;
		}

		public async Task<Employee> AddEmployee(Employee employee)
		{
			string cs = Configuration["ConnectionStrings:BlazorDBConnection"];
			using (SqlConnection connection = new SqlConnection(cs))
			{
				SqlCommand command = new SqlCommand("spCreateEmployee", connection);
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.AddWithValue("@FirstName", employee.FirstName);
				command.Parameters.AddWithValue("@LastName", employee.LastName);
				command.Parameters.AddWithValue("@Email", employee.Email);
				command.Parameters.AddWithValue("@DOB", employee.DateOfBrith);
				command.Parameters.AddWithValue("@Gender", employee.Gender);
				command.Parameters.AddWithValue("@PhotoPath", employee.PhotoPath);
				command.Parameters.AddWithValue("@Id_department", employee.DepartmentId);


				await connection.OpenAsync();
				await command.ExecuteNonQueryAsync();
			}

			return employee;
		}

		public async Task<Employee> UpdateEmployee(Employee employee)
		{
			string cs = Configuration["ConnectionStrings:BlazorDBConnection"];
			using (SqlConnection connection = new SqlConnection(cs))
			{
				SqlCommand command = new SqlCommand("spUpdateEmployee", connection);
				command.CommandType = CommandType.StoredProcedure;

				command.Parameters.AddWithValue("@Id_employee", employee.EmployeeId);
				command.Parameters.AddWithValue("@FirstName", employee.FirstName);
				command.Parameters.AddWithValue("@LastName", employee.LastName);
				command.Parameters.AddWithValue("@Email", employee.Email);
				command.Parameters.AddWithValue("@DOB", employee.DateOfBrith);
				command.Parameters.AddWithValue("@Gender", employee.Gender);
				command.Parameters.AddWithValue("@PhotoPath", employee.PhotoPath);
				command.Parameters.AddWithValue("@Id_department", employee.DepartmentId);


				await connection.OpenAsync();
				await command.ExecuteNonQueryAsync();
			}

			return employee;

		}

		public async Task<Employee> GetEmployee(int employeeId)
		{
			Employee employee = null;
			string cs = Configuration["ConnectionStrings:BlazorDBConnection"];
			using (SqlConnection con = new SqlConnection(cs))
			{
				await con.OpenAsync();
				SqlCommand cmd = new SqlCommand("sp_GetEmployeeById", con);
				cmd.CommandType = CommandType.StoredProcedure;

				cmd.Parameters.AddWithValue("@Id", employeeId);

				SqlDataReader rdr = await cmd.ExecuteReaderAsync();

				if (rdr.Read())
				{
					employee = new Employee();
					employee.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
					employee.FirstName = rdr["FirstName"].ToString();
					employee.LastName = rdr["LastName"].ToString();
					employee.Email = rdr["Email"].ToString();
					employee.DateOfBrith = Convert.ToDateTime(rdr["DateOfBirth"]);
					char genderChar = Convert.ToChar(rdr["Gender"]);
					if (Enum.TryParse<Gender>(genderChar.ToString(), out Gender gender))
					{
						if (gender == Gender.M)
						{
							employee.Gender = Gender.M;
						}
						else
						{
							employee.Gender = Gender.F;
						}
					}
					employee.PhotoPath = rdr["PhotoPath"].ToString();
					Department department = await departmentRepository.GetDepartment(employee.EmployeeId);
					employee.Department = department;




				}

				return employee;
			}
		}

		public async Task DeleteEmployee(int employeeId)
		{
			string cs = Configuration["ConnectionStrings:BlazorDBConnection"];
			using (SqlConnection connection = new SqlConnection(cs))
			{
				SqlCommand command = new SqlCommand("sp_DeleteEmployee", connection);
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.AddWithValue("@Id", employeeId);

				await connection.OpenAsync();
				await command.ExecuteNonQueryAsync();
			}


		}


	}
}
