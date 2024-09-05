using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeApplication.Shared
{
    public class Employee_DTO
    {
        /// <summary>
        /// Unique Id of an Employee
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// First Name of Employee
        /// </summary>
        [Required]
        [MinLength(2, ErrorMessage = "FirstName must contains at least 2 charcters")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name of Employee
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Email of Employee
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Date of Birth of Employee
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gender of Employee
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Department Id of Employee
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Path of the image of Employee
        /// </summary>
        public string PhotoPath { get; set; }
    }
}
