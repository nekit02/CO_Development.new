namespace AdminPanel.Models
{
    public class Teachers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Lessons { get; set; }
        public int WorkExp { get; set; }
        public string? Description { get; set; }
        public string? TeacherPicturePath { get; set; }
        public ICollection<File> Teacherpictures { get; set; }
        public Teachers()
        {
            Teacherpictures = new List<File>();
        }
    }
}
