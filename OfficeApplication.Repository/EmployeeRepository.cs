using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using OfficeApplication.Shared;
using System;
using System.Diagnostics.CodeAnalysis;

namespace OfficeApplication.Repository
{
    ///<inheritdoc cref="IEmployeeRepository" > 
    [ExcludeFromCodeCoverage]
    public class EmployeeRepository : IEmployeeRepository
    {
        /// <summary>
        /// instance of DbContextClass
        /// </summary>
        private readonly DbContextClass _appDbContext;

        /// <summary>
        /// instance of MongoDbContextClass
        /// </summary>
        private readonly MongoDbContextClass _mongoDbContext;

        /// <summary>
        /// DI of of the DbContextClass in EmployeeRepository
        /// </summary>
        /// <param name="appDbContext">
        /// instance of DbContextClass 
        /// </param>
        public EmployeeRepository(DbContextClass appDbContext, MongoDbContextClass mongoDbContext)
        {
            _appDbContext = appDbContext;
            _mongoDbContext = mongoDbContext;
        }

        /// <inheritdoc/>
        public async Task<Employee> GetEmployee(int employeeId)
        {
            // If we have an employee for an incoming employee Id then return the employee
            return await _appDbContext.Employees
                // To include Dapartment data from underlying "Department" table
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
        }

        /// <inheritdoc/>
        public async Task<EmployeeMongo> GetEmployeeFromMongoAsync(string employeeId)
        {
            return await _mongoDbContext.Employees
                .Find(e => e.Id == employeeId)
                .FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            // It will return all the emplooyes from the Employee Collection
            return await _appDbContext.Employees.
                ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<EmployeeMongo>> GetEmployeesFromMongoAsync()
        {
            return await _mongoDbContext.Employees
                .Find(e => true)
                .ToListAsync();
        }

        /// <inheritdoc/>
        // Add Employee to both SQL Server and MongoDB
        public async Task<Employee> AddEmployee(Employee employee)
        {
            if (employee.Department != null)
            {
                _appDbContext.Entry(employee.Department).State = EntityState.Unchanged;
            }

            // Add to SQL Server
            var result = await _appDbContext.Employees.AddAsync(employee);
            await _appDbContext.SaveChangesAsync();

            // Map to MongoDB-specific Employee model
            var employeeMongo = new EmployeeMongo
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                DateOfBirth = employee.DateOfBirth,
                Gender = (GenderMongo)employee.Gender,
                DepartmentId = employee.DepartmentId,
                PhotoPath = employee.PhotoPath // Assuming you want to include this field as well
            };

            // Add to MongoDB
            await _mongoDbContext.Employees.InsertOneAsync(employeeMongo);

            return result.Entity;
        }


        /// <inheritdoc/>
        public async Task<IEnumerable<Employee>> GetEmployeesFromBothAsync()
        {
            var sqlEmployees = await GetEmployees();
            var mongoEmployees = await GetEmployeesFromMongoAsync();

            // Map MongoDB employees to SQL-like Employee objects
            var mappedMongoEmployees = mongoEmployees.Select(m => new Employee
            {
                EmployeeId = m.EmployeeId, // Assuming 'Id' in MongoDB is string and needs to be converted to int
                DepartmentId = m.DepartmentId,
                FirstName = m.FirstName,
                LastName = m.LastName,
                Email = m.Email,
                DateOfBirth = m.DateOfBirth,
                Gender = (Gender)m.Gender,
                PhotoPath = m.PhotoPath // Include this if it’s relevant
            });

            var combinedEmployees = sqlEmployees.Concat(mappedMongoEmployees).ToList();
            return combinedEmployees;
        }


        /// <inheritdoc/>
        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            // If we have have an employee for a provided email id then return it
            return await _appDbContext.Employees
                .FirstOrDefaultAsync(e => e.Email == email);
        }

    }
}
