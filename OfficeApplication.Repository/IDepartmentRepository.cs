using OfficeApplication.Shared;
using System.Diagnostics.CodeAnalysis;

namespace OfficeApplication.Repository
{
    /// <summary>
    /// Defines the methods required to implement a repository for the department
    /// </summary>
    public interface IDepartmentRepository
    {
        /// <summary>
        /// To find all the departments in the databse 
        /// </summary>
        /// <returns>Upon completion it will return all the dapartment exists </returns>
        Task<IEnumerable<Department>> GetDepartments();

        /// <summary>
        /// To get a department corresponding to a specific department Id
        /// </summary>
        /// <param name="departmentId">
        /// Department Id of a particular department
        /// </param>
        /// <returns> Upon completion it will return the A single deparment </returns>
        Task<Department> GetDepartment(int departmentId);

        /// <summary>
        /// To get a department corresponding to a specific department Id
        /// </summary>
        /// <param name="departmentId">
        /// Department Id of a particular department
        /// </param>
        /// <returns>
        /// Upon completion it will return the A single deparment from mongoDB
        /// </returns>
        Task<DepartmentMongo> GetDepartmentFromMongoAsync(int departmentId);

        /// <summary>
        /// To find all the departments in the databse 
        /// </summary>
        /// <returns>Upon completion it will return all the dapartment exists </returns>
        Task<IEnumerable<DepartmentMongo>> GetDepartmentsFromMongoAsync();

        /// <summary>
        /// Fetch and combine departments from both SQL Server and MongoDB.
        /// </summary>
        Task<IEnumerable<Department>> GetDepartmentsFromBothAsync();

        /// <summary>
        /// Fetch and aggregate departments based on a specific criterion.
        /// In this example, we'll aggregate by department name.
        /// </summary>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        Task<IEnumerable<Department>> GetDepartmentsAggregatedByCriterionAsync(string departmentName);

        /// <summary>
        /// Manually add a department to both SQL Server and MongoDB.
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        Task AddDepartmentAsync(Department department);

    }
}
