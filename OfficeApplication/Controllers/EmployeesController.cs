using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OfficeApplication.Repository;
using OfficeApplication.Shared;

namespace OfficeApplication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieve a collection of all employees
        /// </summary>
        /// <returns>
        /// HTTP 200 Ok with the collection fo employees if data retrieve is successful.
        /// HTTP 500 Internal Server Error for unexpected errors during data retrieved.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var emp = (await _employeeRepository.GetEmployees()).Select(e=>_mapper.Map<Employee_DTO>(e));
                return Ok(emp);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        /// <summary>
        /// Retrieve a collection of all employees from MongoDB
        /// </summary>
        /// <returns>
        /// HTTP 200 Ok with the collection of employees if data retrieve is successful.
        /// HTTP 500 Internal Server Error for unexpected errors during data retrieval.
        /// </returns>
        [HttpGet("mongo")]
        public async Task<IActionResult> GetEmployeesFromMongo()
        {
            try
            {
                var emp = (await _employeeRepository.GetEmployeesFromMongoAsync());
                return Ok(emp);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from MongoDB");
            }
        }


        /// <summary>
        /// Retrieves an employee by their unique identifier
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the employee
        /// </param>
        /// <returns>
        /// HTTP 200 Ok with the employees if found.
        /// HTTP 400 Not Found if no employees is found with the specified identifier.
        /// HTTP 500 Internal Server Error for unexpected errors during data retrieve.
        /// </returns>
        [HttpGet("from-sql/{id:int}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            try
            {
                var result = await _employeeRepository.GetEmployee(id);

                if (result == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<Employee_DTO>(result));

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        /// <summary>
        /// Retrieves an employee from MongoDB by their unique identifier
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the employee
        /// </param>
        /// <returns>
        /// HTTP 200 Ok with the employee if found.
        /// HTTP 400 Not Found if no employee is found with the specified identifier.
        /// HTTP 500 Internal Server Error for unexpected errors during data retrieval.
        /// </returns>
        [HttpGet("mongo/{id:int}")]
        public async Task<IActionResult> GetEmployeeFromMongo(string id)
        {
            try
            {
                var result = await _employeeRepository.GetEmployeeFromMongoAsync(id);

                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from MongoDB");
            }
        }


        /// <summary>
        /// Creates a new employee record.
        /// </summary>
        /// <param name="employee">
        /// The employee data to be added.
        /// </param>
        /// <returns>
        /// HTTP 201 Created with the created employee if the record is added successfully.
        /// HTTP 400 Bad Request if the provided employee data is null or partial.
        /// HTTP 400 Bad Request with model state errors if the employee email is already in use.
        /// HTTP 500 Internal Server Error for unexpected errors during the creation process.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(Employee employee)
        {
            var emp = await _employeeRepository.GetEmployeeByEmail(employee.Email);

            if (emp != null)
            {
                ModelState.AddModelError("Email", "Employee email already in use");
                return BadRequest(ModelState);
            }

            var createdEmployee = await _employeeRepository.AddEmployee(employee);

            return CreatedAtAction(nameof(GetEmployee),
                new { id = createdEmployee.EmployeeId }, createdEmployee);

        }

        /// <summary>
        /// Retrieve a collection of all employees from both SQL and MongoDB
        /// </summary>
        /// <returns>
        /// HTTP 200 Ok with the combined collection of employees from both SQL and MongoDB if data retrieve is successful.
        /// HTTP 500 Internal Server Error for unexpected errors during data retrieval.
        /// </returns>
        [HttpGet("combined")]
        public async Task<IActionResult> GetEmployeesFromBothAsync()
        {
            try
            {
                // Call the GetEmployeesFromBothAsync() method from _employeeRepository.
                var combinedEmployees = (await _employeeRepository.GetEmployeesFromBothAsync());

                return Ok(combinedEmployees);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from both SQL and MongoDB.");
            }
        }
    }
}
