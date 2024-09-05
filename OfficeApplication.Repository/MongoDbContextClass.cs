using MongoDB.Bson;
using MongoDB.Driver;
using OfficeApplication.Shared;

namespace OfficeApplication.Repository
{
    public class MongoDbContextClass
    {
        private readonly IMongoDatabase _database;

        public MongoDbContextClass(IMongoClient mongoClient, string databaseName)
        {
            _database = mongoClient.GetDatabase(databaseName);

            // Optionally seed data during initialization
            SeedData();
        }

        public IMongoCollection<EmployeeMongo> Employees => _database.GetCollection<EmployeeMongo>("Employees");
        public IMongoCollection<DepartmentMongo> Departments => _database.GetCollection<DepartmentMongo>("Departments");

        private void SeedData()
        {
            // Check if Departments collection is empty and seed initial data
            if (!Departments.AsQueryable().Any())
            {
                Departments.InsertMany(
                [
                    new DepartmentMongo { DepartmentId = 1, DepartmentName = "IT" },
                    new DepartmentMongo { DepartmentId = 2, DepartmentName = "HR" },
                    new DepartmentMongo { DepartmentId = 3, DepartmentName = "Payroll" },
                    new DepartmentMongo { DepartmentId = 4, DepartmentName = "Admin" }
                ]);
            }

            if (!Employees.Find(e => true).Any())
            {
                var employees = new[]
                {
                    new EmployeeMongo
                    {
                        Id = ObjectId.GenerateNewId().ToString(), // Generate a new ObjectId
                        FirstName = "John",
                        LastName = "Hastings",
                        Email = "David@pragimtech.com",
                        DateOfBirth = new DateTime(1980, 10, 5),
                        Gender = GenderMongo.Male,
                        DepartmentId = 1,
                        PhotoPath = "images/john.png"
                    },
                    new EmployeeMongo
                    {
                        Id = ObjectId.GenerateNewId().ToString(), // Generate a new ObjectId
                        FirstName = "Sam",
                        LastName = "Galloway",
                        Email = "Sam@pragimtech.com",
                        DateOfBirth = new DateTime(1981, 12, 22),
                        Gender = GenderMongo.Male,
                        DepartmentId = 2,
                        PhotoPath = "images/sam.jpg"
                    },
                    new EmployeeMongo
                    {
                        Id = ObjectId.GenerateNewId().ToString(), // Generate a new ObjectId
                        FirstName = "Mary",
                        LastName = "Smith",
                        Email = "mary@pragimtech.com",
                        DateOfBirth = new DateTime(1979, 11, 11),
                        Gender = GenderMongo.Female,
                        DepartmentId = 1,
                        PhotoPath = "images/mary.png"
                    },
                    new EmployeeMongo
                    {
                        Id = ObjectId.GenerateNewId().ToString(), // Generate a new ObjectId
                        FirstName = "Sara",
                        LastName = "Longway",
                        Email = "sara@pragimtech.com",
                        DateOfBirth = new DateTime(1982, 9, 23),
                        Gender = GenderMongo.Female,
                        DepartmentId = 3,
                        PhotoPath = "images/sara.png"
                    }
                };

                Employees.InsertMany(employees);
            }
        }
    }
}
