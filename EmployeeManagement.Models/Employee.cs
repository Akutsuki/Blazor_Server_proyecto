﻿using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
	public class Employee
	{
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "FirstName is mandatory")]
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
      //  [EmailAddress]
        public string Email { get; set; }
        public DateTime DateOfBrith { get; set; }
        public Gender Gender { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public string PhotoPath { get; set; }
    }
}
