using System;

namespace StudentsApp.Models
{
    public class Student
    {
        public string LastName { get; set; } = "";
        public int Course { get; set; }
        public string StudentCard { get; set; } = "";
        public double AverageGrade { get; set; }
        public string Country { get; set; } = "";
        public string ForeignPassportNumber { get; set; } = "";
        public int YearsInUkraine { get; set; }

        public bool IsForeign()
        {
            return !string.Equals(Country, "Україна", StringComparison.OrdinalIgnoreCase);
        }

        public bool IsExcellent()
        {
            return AverageGrade >= 90.0;
        }

        public bool CanGetCitizenship()
        {
            return IsForeign() && YearsInUkraine >= 5;
        }
    }
}
