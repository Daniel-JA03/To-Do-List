namespace ToDoAPI.Models
{
    public class Usuario
    {
        public long ide_usr { get; set; }
        public string cor_usr { get; set; } = string.Empty;
        public string pwd_usr { get; set; } = string.Empty;
        public string nom_usr { get; set; } = string.Empty;
        public string ape_usr { get; set; } = string.Empty;
        public DateTime fech_registro { get; set; } = DateTime.UtcNow;
    }
}
