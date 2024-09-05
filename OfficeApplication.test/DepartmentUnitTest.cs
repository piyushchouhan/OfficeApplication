using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OfficeApplication.Controllers;
using OfficeApplication.Repository;
using OfficeApplication.Shared;

namespace OfficeApplication.test
{
    public class DepartmentUnitTest
    {
        private readonly IDepartmentRepository _departmentRepository = Mock.Of<IDepartmentRepository>();
        private readonly IFixture _fixture = new Fixture();

        [Fact]
        public async Task GivenGetAllDepartmentReturn200Ok()
        {
            //Arrange
            var dept = this._fixture.Create<IEnumerable<Department>>();
            Mock.Get(_departmentRepository).Setup(x => x.GetDepartments()).ReturnsAsync(dept);

            var deptController = new DepartmentsController(_departmentRepository);

            //Act 
            var actual = await deptController.GetDepartments();

            //Assert
            Assert.NotNull(actual);
            var actualType = Assert.IsType<OkObjectResult>(actual);

        }

        [Fact]
        public async Task GivenGetAllDepartmentOnExceptionReturn500InternalServerError()
        {
            //Arrange
            var dept = this._fixture.Create<IEnumerable<Department>>();
            Mock.Get(_departmentRepository).Setup(x => x.GetDepartments()).ThrowsAsync(new Exception());

            var deptController = new DepartmentsController(_departmentRepository);

            //Act 
            var actual = await deptController.GetDepartments();

            //Assert
            Assert.NotNull(actual);
            var actualType = Assert.IsType<ObjectResult>(actual);

        }


        [Fact]
        public async Task GivenDepartmentIdOnGetDepartmentReturn200Ok()
        {
            //Arrange
            var dept = this._fixture.Create<Department>();
            Mock.Get(_departmentRepository).Setup(x => x.GetDepartment(It.IsAny<int>())).ReturnsAsync(dept);

            var deptController = new DepartmentsController(_departmentRepository);

            //Act 
            var actual = await deptController.GetDepartment(4);

            //Assert
            Assert.NotNull(actual);
            var actualType = Assert.IsType<OkObjectResult>(actual);

        }

        [Fact]
        public async Task GivenDepartmentIdOnGetDepartmentReturn400NotFound()
        {
            //Arrange
            var dept = this._fixture.Create<Department>();
            dept = null;
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            Mock.Get(_departmentRepository)
                .Setup(x => x.GetDepartment(It.IsAny<int>())).ReturnsAsync(dept);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

            var deptController = new DepartmentsController(_departmentRepository);

            //Act 
            var actual = await deptController.GetDepartment(4);

            //Assert
            Assert.NotNull(actual);
            var actualType = Assert.IsType<NotFoundResult>(actual);
        }

        [Fact]
        public async Task GivenDepartmentId_OnGetDepartment_Return500InternalServerError()
        {
            //Arrange
            Mock.Get(_departmentRepository).Setup(x => x.GetDepartment(It.IsAny<int>())).ThrowsAsync(new Exception());

            var deptController = new DepartmentsController(_departmentRepository);

            //Act 
            var actual = await deptController.GetDepartment(4);

            //Assert
            Assert.NotNull(actual);
            var actualType = Assert.IsType<ObjectResult>(actual);
        }


    }
}
