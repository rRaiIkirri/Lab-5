using System.Collections.Generic;
using StudentsApp.BLL;
using StudentsApp.DAL;
using StudentsApp.Models;
using Xunit;

namespace StudentsApp.Tests
{
    public class StudentServiceTests
    {
        [Fact]
        public void CountForeignExcellentFirstCourseStudents_ReturnsExpectedNumber()
        {
            
            var repository = new FakeStudentRepository();
            var service = new StudentService(repository);

            
            int result = service.CountForeignExcellentFirstCourseStudents();

            
            Assert.Equal(2, result);
        }

        [Fact]
        public void GetForeignExcellentFirstCourseStudents_ReturnsOnlyMatchingStudents()
        {
            
            var repository = new FakeStudentRepository();
            var service = new StudentService(repository);

            
            var result = service.GetForeignExcellentFirstCourseStudents();

            
            Assert.All(result, s =>
            {
                Assert.True(s.IsForeign());
                Assert.True(s.IsExcellent());
                Assert.Equal(1, s.Course);
            });
        }

        [Fact]
        public void GetCitizenshipEligibleStudents_ReturnsOnlyStudentsWith5OrMoreYears()
        {            
            var repository = new FakeStudentRepository();
            var service = new StudentService(repository);
            
            var result = service.GetCitizenshipEligibleStudents();
            
            Assert.All(result, s =>
            {
                Assert.True(s.IsForeign());
                Assert.True(s.YearsInUkraine >= 5);
            });
        }

        [Fact]
        public void GetAllStudents_ReturnsAllFromRepository()
        {            
            var repository = new FakeStudentRepository();
            var service = new StudentService(repository);
    
            var result = service.GetAllStudents();
        
            Assert.Equal(repository.Students.Count, result.Count);
        }
    }

    internal class FakeStudentRepository : IStudentRepository
    {
        public List<Student> Students { get; }

        public FakeStudentRepository()
        {
            Students = new List<Student>
            {
                new Student
                {
                    LastName = "Феоктістов",
                    Course = 1,
                    StudentCard = "SC001",
                    AverageGrade = 85,
                    Country = "Польща",
                    ForeignPassportNumber = "P123456",
                    YearsInUkraine = 3
                },
                new Student
                {
                    LastName = "Ключник",
                    Course = 1,
                    StudentCard = "SC002",
                    AverageGrade = 92,
                    Country = "Німеччина",
                    ForeignPassportNumber = "N987654",
                    YearsInUkraine = 6
                },
                new Student
                {
                    LastName = "Вафлєрчук",
                    Course = 2,
                    StudentCard = "SC003",
                    AverageGrade = 88,
                    Country = "Україна",
                    ForeignPassportNumber = "",
                    YearsInUkraine = 0
                },
                new Student
                {
                    LastName = "Smith",
                    Course = 1,
                    StudentCard = "SC004",
                    AverageGrade = 91,
                    Country = "США",
                    ForeignPassportNumber = "US555555",
                    YearsInUkraine = 5
                }
            };
        }

        public List<Student> GetAll()
        {
            return new List<Student>(Students);
        }

        public void SaveAll(List<Student> students)
        {
            Students.Clear();
            Students.AddRange(students);
        }
    }
}
