using OfficeApplication.Shared;

namespace OfficeApplication.Repository
{
    /// <summary>
    /// Defines the methods required to implement a repository for the Employee
    /// </summary>
    public interface IEmployeeRepository
    {
        /// <summary>
        /// To find all the Employee from the Employee table
        /// </summary>
        /// <returns>Upon completion it will return all the employees</returns>
        Task<IEnumerable<Employee>> GetEmployees();

        /// <summary>
        /// To find a particular Employee based on the Employee Id
        /// </summary>
        /// <param name="employeeId">
        /// Employee Id of a particular Employee
        /// </param>
        /// <returns> Upon completion it will return an employee for an empoloyee Id</returns>
        Task<Employee> GetEmployee(int employeeId);

        /// <summary>
        /// To find a particular Employee based on the Employee Email
        /// </summary>
        /// <param name="email">
        /// Email of a particular Employee
        /// </param>
        /// <returns>Upon completion it will return an employee for an email </returns>
        Task<Employee> GetEmployeeByEmail(string email);

        /// <summary>
        /// To insert a new Employee in the Employee Table
        /// </summary>
        /// <param name="employee">
        /// Incoming employee instance which we want to insert in the table 
        /// </param>
        /// <returns> Upon completion it will insert a new row of incoming employee in the table </returns>
        Task<Employee> AddEmployee(Employee employee);

        /// <summary>
        /// MongoDB GET method
        /// </summary>
        /// <param name="employeeId">
        /// Employee Id of a particular Employee
        /// </param>
        /// <returns></returns>
        Task<EmployeeMongo> GetEmployeeFromMongoAsync(string employeeId);

        /// <summary>
        /// MongoDB GET all method
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<EmployeeMongo>> GetEmployeesFromMongoAsync();

        /// <summary>
        /// Combined SQL Server and MongoDB
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Employee>> GetEmployeesFromBothAsync();

    }
}
