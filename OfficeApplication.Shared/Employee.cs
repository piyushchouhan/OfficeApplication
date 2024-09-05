using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OfficeApplication.Shared
{
    /// <summary>
    /// Properties of Employee
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Unique Id of an Employee
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// First Name of Employee
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name of Employee
        /// </summary>
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

        /// <summary>
        /// Department Name of Employee
        /// </summary>
        public Department Department { get; set; }
    }
}
