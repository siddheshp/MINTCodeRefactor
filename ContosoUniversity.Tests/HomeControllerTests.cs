using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ContosoUniversity.Controllers;
using ContosoUniversity.Models;
using ContosoUniversity.Persistence.SQL;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ContosoUniversity.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        Mock<SchoolContext> _mockSchoolContext;
        Mock<StudentRepository> _mockStudentRepository;
        Mock<UnitOfWork> _mockUow;
        Mock<DbSet<Student>> _mockStudentDbSet;
        IQueryable<Student> _data;

        [TestInitialize]
        public void Initialize()
        {
            _data = new List<Student> {
                new Student { ID = 1, EnrollmentDate = DateTime.Now.AddYears(-5), FirstMidName = "A", LastName = "A", Enrollments = null },
                new Student { ID = 2, EnrollmentDate = DateTime.Now.AddYears(-4), FirstMidName = "B", LastName = "B", Enrollments = null },
                new Student { ID = 3, EnrollmentDate = DateTime.Now.AddYears(-3), FirstMidName = "C", LastName = "C", Enrollments = null },
                new Student { ID = 4, EnrollmentDate = DateTime.Now.AddYears(-2), FirstMidName = "D", LastName = "D", Enrollments = null },
                new Student { ID = 5, EnrollmentDate = DateTime.Now.AddYears(-1), FirstMidName = "E", LastName = "E", Enrollments = null },
            }.AsQueryable();

            _mockStudentDbSet = new Mock<DbSet<Student>>();
            _mockStudentDbSet.As<IQueryable<Student>>().Setup(q => q.Provider).Returns(_data.Provider);
            _mockStudentDbSet.As<IQueryable<Student>>().Setup(q => q.ElementType).Returns(_data.ElementType);
            _mockStudentDbSet.As<IQueryable<Student>>().Setup(q => q.Expression).Returns(_data.Expression);
            _mockStudentDbSet.As<IQueryable<Student>>().Setup(q => q.GetEnumerator()).Returns(_data.GetEnumerator());

            _mockSchoolContext = new Mock<SchoolContext>("Dummy connection name/string");
            _mockSchoolContext.Setup(context => context.Students).Returns(_mockStudentDbSet.Object);

            _mockStudentRepository = new Mock<StudentRepository>(_mockSchoolContext.Object);
            _mockUow = new Mock<UnitOfWork>(_mockSchoolContext.Object, _mockStudentRepository.Object);
        }

        [TestMethod]
        public void HomeControllerAboutShouldReturnFiveRows()
        {
            // Arrange
            var homeController = new HomeController(_mockUow.Object);
            var expected = _data.
                GroupBy(x => x.EnrollmentDate).
                Select(e => new StudentEnrollment
                {
                    EnrollmentDate = e.Key,
                    StudentCount = e.Count()
                })
                .OrderBy(s => s.EnrollmentDate);

            // Act
            var actionResult = homeController.About() as ViewResult;
            var actual = actionResult.ViewData.Model as IEnumerable<StudentEnrollment>;

            // Assert
            actual.Should().HaveCount(expected.Count())
                .And.BeEquivalentTo(expected);
        }
    }
}
