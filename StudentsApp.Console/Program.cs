using System;
using System.Collections.Generic;
using System.IO;
using StudentsApp.BLL;
using StudentsApp.DAL;
using StudentsApp.Models;

namespace StudentsApp.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string dataFile = Path.Combine(AppContext.BaseDirectory, "students.json");
            IStudentRepository repository = new FileStudentRepository(dataFile);
            StudentService service = new StudentService(repository);

            EnsureSampleData(repository);

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("== Меню ==");
                Console.WriteLine("1. Показати всіх студентів");
                Console.WriteLine("2. Показати іноземних відмінників 1-го курсу");
                Console.WriteLine("3. Показати іноземних студентів, які можуть отримати громадянство");
                Console.WriteLine("0. Вихід");
                Console.Write(": ");
                string? choice = Console.ReadLine();

                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ShowAllStudents(service);
                        break;
                    case "2":
                        ShowForeignExcellentFirstCourseStudents(service);
                        break;
                    case "3":
                        ShowCitizenshipEligibleStudents(service);
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Невірний вибір.");
                        break;
                }

                Console.WriteLine();
            }
        }

        private static void EnsureSampleData(IStudentRepository repository)
        {
            var current = repository.GetAll();
            if (current.Count > 0)
            {
                return;
            }

            var sample = new List<Student>
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

            repository.SaveAll(sample);
        }

        private static void ShowAllStudents(StudentService service)
        {
            var students = service.GetAllStudents();
            if (students.Count == 0)
            {
                Console.WriteLine("Список студентів порожній.");
                return;
            }

            foreach (var s in students)
            {
                Console.WriteLine($"{s.LastName}, курс {s.Course}, країна: {s.Country}, середній бал: {s.AverageGrade}");
            }
        }

        private static void ShowForeignExcellentFirstCourseStudents(StudentService service)
        {
            var students = service.GetForeignExcellentFirstCourseStudents();
            Console.WriteLine($"Кількість іноземних студентів-відмінників 1-го курсу: {students.Count}");
            foreach (var s in students)
            {
                Console.WriteLine($"{s.LastName}, країна: {s.Country}, середній бал: {s.AverageGrade}");
            }
        }

        private static void ShowCitizenshipEligibleStudents(StudentService service)
        {
            var students = service.GetCitizenshipEligibleStudents();
            if (students.Count == 0)
            {
                Console.WriteLine("Немає студентів, які можуть отримати громадянство.");
                return;
            }

            Console.WriteLine("Іноземні студенти, які можуть отримати громадянство:");
            foreach (var s in students)
            {
                Console.WriteLine($"{s.LastName}, країна: {s.Country}, років в Україні: {s.YearsInUkraine}");
            }
        }
    }
}
