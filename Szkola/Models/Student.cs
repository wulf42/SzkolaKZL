using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Szkola.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public double LastGradePointAverage { get; set; }

        public int IdClass { get; set; }

        [ForeignKey("IdClass")]
        public Class? Class { get; set; }
    }
}