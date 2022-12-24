namespace AdminPanel.Models
{
    public class Calender
    {
        public int Id { get; set; }
        public string? Month { get; set; }
        public string? Year { get; set; }
        public ICollection<Events> Events { get; set; }

        public Calender()
        {
            Events = new List<Events>();
        }

    }
}
