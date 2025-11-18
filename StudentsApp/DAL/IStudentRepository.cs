using System.Collections.Generic;
using StudentsApp.Models;

namespace StudentsApp.DAL
{
    public interface IStudentRepository
    {
        List<Student> GetAll();
        void SaveAll(List<Student> students);
    }
}
