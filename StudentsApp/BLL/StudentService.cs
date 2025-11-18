using System.Collections.Generic;
using System.Linq;
using StudentsApp.DAL;
using StudentsApp.Models;

namespace StudentsApp.BLL
{
    public class StudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public List<Student> GetAllStudents()
        {
            return _repository.GetAll();
        }

        public int CountForeignExcellentFirstCourseStudents()
        {
            return GetForeignExcellentFirstCourseStudents().Count;
        }

        public List<Student> GetForeignExcellentFirstCourseStudents()
        {
            var students = _repository.GetAll();

            var query = students
                .Where(s => s.IsForeign())
                .Where(s => s.IsExcellent())
                .Where(s => s.Course == 1);

            return query.ToList();
        }

        public List<Student> GetCitizenshipEligibleStudents()
        {
            var students = _repository.GetAll();

            return students
                .Where(s => s.CanGetCitizenship())
                .ToList();
        }
    }
}
