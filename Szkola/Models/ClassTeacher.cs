using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Szkola.Models
{
    public class ClassTeacher
    {
        [Key]
        public int Id { get; set; }

        public int ClassId { get; set; }

        [ForeignKey("ClassId")]
        public Class? Class { get; set; }

        public int TeacherId { get; set; }

        [ForeignKey("TeacherId")]
        public Teacher? Teacher { get; set; }
    }
}