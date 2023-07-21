using EmployeeManagement.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Repositories.E_Department
{
	public class DepartmentRepository : IDepartmentRepository
	{
		public IConfiguration Configuration { get; }
		// string connectionString = string.Empty;

		public DepartmentRepository(IConfiguration _configuration)
		{
			Configuration = _configuration;

			//connectionString = _configuration.GetConnectionString("BlazorDBConnection");
		}

		public async Task<Department> GetDepartment(int departmentId)
		{
			string cs = Configuration["ConnectionStrings:BlazorDBConnection"];
			using (SqlConnection con = new SqlConnection(cs))
			{
				await con.OpenAsync();
				SqlCommand cmd = new SqlCommand("sp_GetDepartmentByEmployeeId", con);
				cmd.CommandType = CommandType.StoredProcedure;

				cmd.Parameters.AddWithValue("@EmployeeId", departmentId);

				SqlDataReader rdr = await cmd.ExecuteReaderAsync();

				if (rdr.Read())
				{
					Department department = new Department
					{
						DepartmentId = Convert.ToInt32(rdr["DepartmentId"]),
						DepartmentName = rdr["DepartmentName"].ToString()
						// Asignar otras propiedades del objeto Department aquí
					};

					return department;
				}

				return null;
			}
		}

		public async Task<IEnumerable<Department>> GetDepartments()
		{
			Department department = null;
			List<Department> listDepartments = new List<Department>();
			string cs = Configuration["ConnectionStrings:BlazorDBConnection"];
			using (SqlConnection con = new SqlConnection(cs))
			{
				SqlCommand cmd = new SqlCommand("sp_GetDepartments", con);
				cmd.CommandType = CommandType.StoredProcedure;
				await con.OpenAsync();

				SqlDataReader rdr = await cmd.ExecuteReaderAsync();
				//SqlDataReader rdr = cmd.ExecuteReader();
				while (rdr.Read())
				{

					department = new Department();
					department.DepartmentId = Convert.ToInt32(rdr["DepartmentId"]);
					department.DepartmentName = rdr["DepartmentName"].ToString();

					listDepartments.Add(department);

				}
			}

			return listDepartments;
		}


	}
}
