using ToDoAPI.Models;

namespace ToDoAPI.Repository.Interfaces
{
    public interface ITarea
    {
        IEnumerable<Tarea> listarTareasPorUsuario(long usuarioId);
        Tarea obtenerTareaPorId(long id);
        string agregarTarea(Tarea tarea);
        string actualizarTarea(Tarea tarea);
        string eliminarTarea(long id);
    }
}
