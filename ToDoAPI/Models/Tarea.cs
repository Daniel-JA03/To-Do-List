namespace ToDoAPI.Models
{
    public class Tarea 
    {
        public long ide_tar { get; set; }
        public string titulo { get; set; } = string.Empty;
        public string? descripcion { get; set; }
        public string estado { get; set; } = "PENDIENTE";
        public DateTime? fecha_limite { get; set; }
        public DateTime fecha_creacion { get; set; } = DateTime.UtcNow;
        public DateTime fecha_actualizacion { get; set; } = DateTime.UtcNow;
        public long usuario_id { get; set; }
        public Usuario? usuario { get; set; }
    }
}
