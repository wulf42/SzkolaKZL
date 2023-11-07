namespace Szkola.Models.ViewModels
{
    public class TeacherViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double EmploymentType { get; set; }
        public DateOnly DateOfEmployment { get; set; }
        public List<string> Classes { get; set; }
    }
}