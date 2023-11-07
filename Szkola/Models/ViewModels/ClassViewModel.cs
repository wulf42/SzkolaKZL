namespace Szkola.Models.ViewModels
{
    public class ClassViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Students { get; set; }
        public List<string> Teachers { get; set; }
    }
}