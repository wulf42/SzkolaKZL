namespace Szkola.Models.ViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public double LastGradePointAverage { get; set; }
        public string Class { get; set; }
    }
}