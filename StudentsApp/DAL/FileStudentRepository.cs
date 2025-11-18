using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using StudentsApp.Models;

namespace StudentsApp.DAL
{
    public class FileStudentRepository : IStudentRepository
    {
        private readonly string _filePath;

        public FileStudentRepository(string filePath)
        {
            _filePath = filePath;
        }

        public List<Student> GetAll()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Student>();
            }

            var json = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<Student>();
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var students = JsonSerializer.Deserialize<List<Student>>(json, options);
            return students ?? new List<Student>();
        }

        public void SaveAll(List<Student> students)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(students, options);
            File.WriteAllText(_filePath, json);
        }
    }
}
