using AutoMapper;
using OfficeApplication.Repository;
using Moq;
using OfficeApplication.Shared;
using AutoFixture;
using OfficeApplication.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using OfficeApplication.Api.Controllers;

namespace OfficeApplication.test
{
    public class EmployeeUnitTest
    {
        private readonly IEmployeeRepository _employeeRepository = Mock.Of<IEmployeeRepository>();
        private readonly IFixture _fixture = new Fixture(); 
        private readonly IMapper _mapper = Mock.Of<IMapper>();


        [Fact]
        public async Task GivenGetListOfAllEmployeesReturnOk200()
        {
            //Arrange 
            var emp = this._fixture.Create<IEnumerable<Employee>>();
            Mock.Get(_employeeRepository).Setup(x => x.GetEmployees()).ReturnsAsync(emp);

            var empController = new EmployeesController(_employeeRepository, _mapper);

            //Act
            var actual = await empController.GetEmployees();

            //Assert
            Assert.NotNull(actual);
            var actualType = Assert.IsType<OkObjectResult>(actual);

        }

        [Fact]
        public async Task GivenGetListOfAllEmployeesReturn500InternalError()
        {
            //Arrange 
            Mock.Get(_employeeRepository).Setup(x => x.GetEmployees()).ThrowsAsync(new Exception());

            var empController = new EmployeesController(_employeeRepository, _mapper);

            //Act
            var actual = await empController.GetEmployees();

            //Assert
            Assert.NotNull(actual);
            var actualType = Assert.IsType<ObjectResult>(actual);

        }

        [Fact]
        public async Task GivenIdOfEmployeeOnGetEmployeeByIdReturnOk200()
        {
            //Arrange 
            var emp = this._fixture.Create<Employee>();
            var empDto = this._fixture.Create<Employee_DTO>();
            
            Mock.Get(_employeeRepository).Setup(x => x.GetEmployee(It.IsAny<int>())).ReturnsAsync(emp);
            Mock.Get(_mapper).Setup(e => e.Map<Employee_DTO>(emp)).Returns(empDto);
            Mock.Get(_mapper).Setup(e => e.Map<Employee>(empDto)).Returns(emp);

            var empController = new EmployeesController(_employeeRepository, _mapper);

            //Act
            var actual = await empController.GetEmployee(4);

            //Assert
            Assert.NotNull(actual);
            var actualType = Assert.IsType<OkObjectResult>(actual);
        }

        [Fact]
        public async Task GivenIdOfEmployeeOnGetEmployeeByIdReturn404NotFound()
        {
            //Arrange 
            var emp = this._fixture.Create<Employee>();
            emp = null;
            Mock.Get(_employeeRepository).Setup(x => x.GetEmployee(It.IsAny<int>())).ReturnsAsync(emp);

            var empController = new EmployeesController(_employeeRepository, _mapper);

            //Act
            var actual = await empController.GetEmployee(4);

            //Assert
            Assert.NotNull(actual);
            var actualType = Assert.IsType<NotFoundResult>(actual);
        }

        [Fact]
        public async Task GivenIdOfEmployeeGetEmployeeByIdReturn500InternalError()
        {
            //Arrange
            Mock.Get(_employeeRepository).Setup(x => x.GetEmployee(It.IsAny<int>())).ThrowsAsync(new Exception());

            var empController = new EmployeesController(_employeeRepository, _mapper);

            //Act
            var actual = await empController.GetEmployee(4);

            //Assert
            Assert.NotNull(actual);
            var actualType = Assert.IsType<ObjectResult>(actual);
        }

        [Fact]
        public async Task GivenEmployeeObjectWhenEmailMatchReturn400BadRequest()
        {
            //Arrange
            var emp = this._fixture.Create<Employee>();
            Mock.Get(_employeeRepository).Setup(x => x.GetEmployeeByEmail(It.IsAny<string>())).ReturnsAsync(emp);

            var empController = new EmployeesController(_employeeRepository, _mapper);

            //Act
            var actual = await empController.CreateEmployee(new Employee());

            //Assert
            Assert.NotNull(actual);
            var actualType = Assert.IsType<BadRequestObjectResult>(actual);
        }

        [Fact]
        public async Task GivenEmployeeObjectCreateEmployeeReturn201ObjectCreated()
        {
            //Arrange
            var emp = this._fixture.Create<Employee>();
            Mock.Get(_employeeRepository).Setup(x => x.AddEmployee(It.IsAny<Employee>())).ReturnsAsync(emp);

            var empController = new EmployeesController(_employeeRepository, _mapper);

            //Act
            var actual = await empController.CreateEmployee(new Employee());

            //Assert
            Assert.NotNull(actual);
            var actualType = Assert.IsType<CreatedAtActionResult>(actual);
        }
    }
}