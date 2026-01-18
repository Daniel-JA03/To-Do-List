using ToDoAPI.Models;

namespace ToDoAPI.Repository.Interfaces
{
    public interface IUsuario
    {
        string verificarLogin(string correo, string password);
        string registrarUsuario(Usuario usuario);
        string obtenerIdUsuario(string correo);
        bool existeCorreo(string correo);
    }
}
