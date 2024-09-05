Sure! Here’s a `README.md` template for the **OfficeApplication** project based on the structure you provided for Public_API. This README will cover the essential aspects of your project, including features, technologies used, project structure, and more.

# OfficeApplication

## Overview

**OfficeApplication** is an ASP.NET Core application designed to manage employee records, leveraging both SQL Server and MongoDB for data storage. This application provides a comprehensive set of features for handling employee data, including CRUD operations, data synchronization between SQL and MongoDB, and robust error handling. It utilizes best practices in software design, including dependency injection, error management, and unit testing with xUnit and Moq.

## Table of Contents

- [Features](#features)
- [Technologies Used](#technologies-used)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Configuration](#configuration)
- [Usage](#usage)
  - [Running the Application](#running-the-application)
  - [API Endpoints](#api-endpoints)
- [Database Migrations](#database-migrations)
- [Testing](#testing)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)

## Features

- **Employee Management:** CRUD operations for managing employee records.
- **Data Synchronization:** Syncs data between SQL Server and MongoDB.
- **Error Handling:** Comprehensive error handling for robust application behavior.
- **Dependency Injection:** Utilizes dependency injection for modularity and testability.
- **Unit Testing:** Includes unit tests with xUnit and Moq for validating functionality.
- **API Documentation:** Provides Swagger/OpenAPI documentation for easy exploration and testing of API endpoints.

## Technologies Used

- **.NET 6:** Framework for building the Web API.
- **ASP.NET Core:** For creating RESTful API endpoints.
- **Entity Framework Core:** ORM for SQL Server database interactions.
- **SQL Server:** Database for storing employee data.
- **MongoDB:** NoSQL database for additional data storage.
- **HttpClient:** For making HTTP requests to external services if needed.
- **AutoMapper:** For mapping between SQL and MongoDB data models.
- **Swagger/OpenAPI:** For API documentation and testing.
- **xUnit:** Testing framework for unit tests.
- **Moq:** Mocking library for creating mock objects in tests.


## Getting Started

### Prerequisites

Before you begin, ensure you have the following installed on your machine:

- **.NET SDK** (version 8.0 )
- **Visual Studio 2022** or **Visual Studio Code**
- **SQL Server** (Express or full version) or **LocalDB**
- **MongoDB** (for MongoDB operations)

### Installation

1. **Clone the Repository**

   ```bash
   git clone https://github.com/piyushchouhan/OfficeApplication.git
   cd OfficeApplication
   ```

2. **Restore NuGet Packages**

   Navigate to the project directory and restore the required packages:

   ```bash
   dotnet restore
   ```

3. **Build the Solution**

   ```bash
   dotnet build
   ```

### Configuration

1. **Configure SQL Server ane MongoDB Connection String**

   Open `appsettings.json` and update the `ConnectionStrings` section with your SQL Server and MongoDB details:

   ```json
   {
     "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=YOUR_DATABASE_NAME;Trusted_Connection=True;MultipleActiveResultSets=true",
    "MongoDbConnection": "mongodb://localhost:27017",
    "MongoDbDatabase": "YOUR_DATABASE_NAME"
     },
     "Logging": {
       "LogLevel": {
         "Default": "Information",
         "Microsoft.AspNetCore": "Warning"
       }
     },
     "AllowedHosts": "*"
   }
   ```

## Usage

### Running the Application

1. **Create Database Migrations**

   Generate migration scripts for SQL Server:

   ```bash
   dotnet ef migrations add InitialCreate
   ```

2. **Apply Database Migrations**

   Update the database schema:

   ```bash
   dotnet ef database update
   ```

3. **Run the Application**

   ```bash
   dotnet run
   ```

   The API will start, and Swagger UI will be available at `https://localhost:{PORT}/swagger` for exploration and testing.

### API Endpoints

#### Get Employees

- **Endpoint:** `GET /api/employees`
- **Description:** Retrieves a collection of all employees.
- **Response:**
  - `200 OK` with employee data.
  - `500 Internal Server Error` for unexpected errors.

**Example Request:**

```http
GET https://localhost:5001/api/employees
```

**Example Response:**

```json
[
  {
    "employeeId": 1,
    "firstName": "John",
    "lastName": "Hastings",
    "email": "David@pragimtech.com",
    "dateOfBirth": "1980-10-05T00:00:00Z",
    "gender": 0,
    "departmentId": 1,
    "photoPath": "images/john.png",
    "department": null
  }
]
```

#### Get Employee by ID

- **Endpoint:** `GET /api/employees/{id}`
- **Description:** Retrieves an employee by their unique identifier.
- **Parameters:**
  - `id` (path) - Unique identifier of the employee.
- **Response:**
  - `200 OK` with employee data.
  - `404 Not Found` if the employee is not found.
  - `500 Internal Server Error` for unexpected errors.

**Example Request:**

```http
GET https://localhost:5001/api/employees/1
```

**Example Response:**

```json
{
  "employeeId": 1,
  "firstName": "John",
  "lastName": "Hastings",
  "email": "David@pragimtech.com",
  "dateOfBirth": "1980-10-05T00:00:00Z",
  "gender": 0,
  "departmentId": 1,
  "photoPath": "images/john.png",
  "department": null
}
```

## Database Migrations

The project uses Entity Framework Core for database operations. Follow these steps to manage migrations:

1. **Add a New Migration**

   ```bash
   dotnet ef migrations add YourMigrationName
   ```

2. **Update the Database**

   Apply the latest migrations to the database:

   ```bash
   dotnet ef database update
   ```

3. **List Pending Migrations**

   To check for any pending migrations:

   ```bash
   dotnet ef migrations list
   ```

## Testing

The project includes unit tests for various components using xUnit and Moq. The tests ensure functionality and error handling are working correctly.

### Running Tests

1. **Navigate to the Test Project**

   ```bash
   cd OfficeApplication.Tests
   ```

2. **Run Tests**

   ```bash
   dotnet test
   ```

### Test Coverage

- **Success Scenario:** Verifies that API endpoints return the correct status codes and data.
- **Error Handling:** Checks that the application handles errors gracefully and returns appropriate HTTP status codes.

## Contributing

Contributions are welcome! Follow these steps to contribute to the project:

1. **Fork the Repository**

   Click the "Fork" button on the repository page to create your own copy.

2. **Clone Your Fork**

   ```bash
   git clone https://github.com/piyushchouhan/OfficeApplication.git
   cd OfficeApplication
   ```

3. **Create a New Branch**

   ```bash
   git checkout -b feature/YourFeatureName
   ```

4. **Make Changes**

   Implement your feature or bug fix.

5. **Commit Your Changes**

   ```bash
   git commit -m "Add feature: YourFeatureName"
   ```

6. **Push to Your Fork**

   ```bash
   git push origin feature/YourFeatureName
   ```

7. **Create a Pull Request**

   Navigate to the original repository and create a pull request from your fork.

## License

This project is licensed under the [MIT License](LICENSE).

## Contact

For any questions or support, please contact:

**Name:** Piyush Chauhan

**Email:** piyushnchouhan@gmail.com

**GitHub:** [piyushchouhan](https://github.com/piyushchouhan)

---

*This README was generated to provide comprehensive guidance on setting up, using, and contributing to the OfficeApplication project. For further assistance, please refer to the official documentation or contact the maintainer.*
