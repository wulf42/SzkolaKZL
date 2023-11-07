using System.ComponentModel.DataAnnotations;

namespace Szkola.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public double EmploymentType { get; set; }

        public DateOnly DateOfEmployment { get; set; }

        public List<ClassTeacher>? ClassTeachers { get; set; }
    }
}