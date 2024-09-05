using Microsoft.AspNetCore.Mvc;
using OfficeApplication.Repository;
using OfficeApplication.Shared;

namespace OfficeApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        /// <summary>
        /// Instance of the IDepartmentRepository for DI.
        /// </summary>
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            this._departmentRepository = departmentRepository;
        }

        /// <summary>
        /// Retrieves a collection of all departments.
        /// </summary>
        /// <returns>
        /// HTTP 200 OK with the collection of departments if data retrievel is successful.
        /// HTTP 500 Internal Server Error for unexpected errors during data retrivel
        /// </returns>
        [HttpGet("from-sql")]
        public async Task<ActionResult> GetDepartments()
        {
            try
            {
                return Ok(await _departmentRepository.GetDepartments());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the sqlserver");
            }
        }

         /// <summary>
        /// Retrieves a collection of all departments from MongoDB.
        /// </summary>
        /// <returns>
        /// HTTP 200 OK with the collection of departments if data retrieval is successful.
        /// HTTP 500 Internal Server Error for unexpected errors during data retrieval.
        /// </returns>
        [HttpGet("from-mongo")]
        public async Task<ActionResult> GetDepartmentsFromMongo()
        {
            try
            {
                return Ok(await _departmentRepository.GetDepartmentsFromMongoAsync());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from MongoDB");
            }
        }

        /// <summary>
        /// Retrieves a collection of departments from both SQL Server and MongoDB combined.
        /// </summary>
        /// <returns>
        /// HTTP 200 OK with the combined collection of departments.
        /// HTTP 500 Internal Server Error for unexpected errors during data retrieval.
        /// </returns>
        [HttpGet("combined")]
        public async Task<ActionResult> GetDepartmentsCombined()
        {
            try
            {
                return Ok(await _departmentRepository.GetDepartmentsFromBothAsync());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from both SQL Server and MongoDB");
            }
        }

        /// <summary>
        /// Retrieves a department by its unique identifier.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the department.
        /// </param>
        /// <returns>
        /// HTTP 200 OK with the deparment if found.
        /// HTTP 400 Not Found if no deparment is found with the specified identifier.
        /// HTTP 500 Internal Server Error for unexpected erros during data retrievel.
        /// </returns>
        [HttpGet("from-sql/{id:int}")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            try
            {
                var result = await _departmentRepository.GetDepartment(id);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the sql server");
            }
        }

        /// <summary>
        /// Retrieves a department by its unique identifier from MongoDB.
        /// </summary>
        /// <param name="id">The unique identifier of the department.</param>
        /// <returns>
        /// HTTP 200 OK with the department if found.
        /// HTTP 404 Not Found if no department is found with the specified identifier.
        /// HTTP 500 Internal Server Error for unexpected errors during data retrieval.
        /// </returns>
        [HttpGet("from-mongo/{id:int}")]
        public async Task<IActionResult> GetDepartmentFromMongo(int id)
        {
            try
            {
                var result = await _departmentRepository.GetDepartmentFromMongoAsync(id);

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
        /// Posts a new department to both SQL Server and MongoDB.
        /// </summary>
        /// <param name="department">The department to add.</param>
        /// <returns>
        /// HTTP 200 OK if the department is successfully added.
        /// HTTP 500 Internal Server Error if there is an error during the save.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] Department department)
        {
            try
            {
                await _departmentRepository.AddDepartmentAsync(department);
                return Ok(department);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error adding data to SQL Server and MongoDB");
            }
        }
    }
}
