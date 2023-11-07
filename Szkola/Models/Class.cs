using System.ComponentModel.DataAnnotations;

namespace Szkola.Models
{
    public class Class
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Student>? Students { get; set; }

        public List<ClassTeacher>? ClassTeachers { get; set; }
    }
}