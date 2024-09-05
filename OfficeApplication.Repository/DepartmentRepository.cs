using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using OfficeApplication.Shared;
using System;
using System.Diagnostics.CodeAnalysis;

namespace OfficeApplication.Repository
{
    ///<inheritdoc cref="IDepartmentRepository" > 
    [ExcludeFromCodeCoverage]
    public class DepartmentRepository : IDepartmentRepository
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
        /// 
        /// </param>
        public DepartmentRepository(DbContextClass appDbContext, MongoDbContextClass mongoDbContext)
        {
            _appDbContext = appDbContext;
            _mongoDbContext = mongoDbContext;
        }

        /// <inheritdoc/>
        public async Task<Department> GetDepartment(int departmentId)
        {
            return await _appDbContext.Departments
                .FirstOrDefaultAsync(d => d.DepartmentId == departmentId);
        }

        /// <inheritdoc/>
        public async Task<DepartmentMongo> GetDepartmentFromMongoAsync(int departmentId)
        {
            return await _mongoDbContext.Departments
                .Find(d => d.DepartmentId == departmentId)
                .FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await _appDbContext.Departments.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<DepartmentMongo>> GetDepartmentsFromMongoAsync()
        {
            return await _mongoDbContext.Departments.Find(d => true).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Department>> GetDepartmentsFromBothAsync()
        {
            var sqlDepartments = await GetDepartments();
            var mongoDepartments = await GetDepartmentsFromMongoAsync();

            // Map MongoDB departments to SQL-like Department objects
            var mappedMongoDepartments = mongoDepartments.Select(m => new Department
            {
                DepartmentId = m.DepartmentId,   // Mapping MongoDB's DepartmentId
                DepartmentName = m.DepartmentName // Mapping MongoDB's DepartmentName
            });

            // Combine the two lists
            var combinedDepartments = sqlDepartments.Concat(mappedMongoDepartments).ToList();

            return combinedDepartments;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Department>> GetDepartmentsAggregatedByCriterionAsync(string departmentName)
        {
            var sqlDepartments = await _appDbContext.Departments
                .Where(d => d.DepartmentName.Contains(departmentName))
                .ToListAsync();

            var mongoDepartments = await _mongoDbContext.Departments
                .Find(d => d.DepartmentName.Contains(departmentName))
                .ToListAsync();

            var mappedMongoDepartments = mongoDepartments.Select(m => new Department
            {
                DepartmentId = m.DepartmentId,   // Mapping MongoDB's DepartmentId
                DepartmentName = m.DepartmentName // Mapping MongoDB's DepartmentName
            });

            var aggregatedDepartments = sqlDepartments.Concat(mappedMongoDepartments).ToList();

            return aggregatedDepartments;
        }

        /// <inheritdoc/>
        public async Task AddDepartmentAsync(Department department)
        {
            // Ensure the department object has the correct structure for both databases.
            if (department == null)
            {
                throw new ArgumentNullException(nameof(department), "Department cannot be null.");
            }

            // Add to SQL Server
            _appDbContext.Departments.Add(department);
            await _appDbContext.SaveChangesAsync();

            // Map to MongoDB-specific model if needed
            var departmentMongo = new DepartmentMongo
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName
                // Map other properties if needed
            };

            // Add to MongoDB
            await _mongoDbContext.Departments.InsertOneAsync(departmentMongo);
        }
    }
}
