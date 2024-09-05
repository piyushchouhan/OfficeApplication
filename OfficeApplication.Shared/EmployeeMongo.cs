using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace OfficeApplication.Shared
{
    public class EmployeeMongo
    {
        /// <summary>
        /// Unique MongoDB Id for Employee
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

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
        public GenderMongo Gender { get; set; }

        /// <summary>
        /// Department Id of Employee
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Path of the image of Employee
        /// </summary>
        public string PhotoPath { get; set; }

        /// <summary>
        /// MongoDB-specific Department object of Employee
        /// </summary>
        public DepartmentMongo Department { get; set; }
    }
}
