namespace AdminPanel.Models
{
    public class Events
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? CalenderId {get; set; }//foreign key
        public Calender? Calender { get; set; }//навигац поле
        public string Day { get; internal set; }
    }
}
