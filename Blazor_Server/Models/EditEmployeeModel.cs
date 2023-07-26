using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_Server.Models
{
	public class EditEmployeeModel
	{
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "FirstName is mandatory")]
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public DateTime DateOfBrith { get; set; }
        public Gender Gender { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; } = new Department();
        public string PhotoPath { get; set; }
    }
}
