
namespace AdminPanel.Models
{
    public class File
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? FilePath { get; set; }
        public int NewsId { get; set; } //foreign key
        public int TeacherId { get; set; } = 0; //foreign key
        public News? News { get; set; } //навигац поле
        public Teachers? Teachers { get; set; } //навигац поле
        public bool IsMain { get; set; }

    }
}
