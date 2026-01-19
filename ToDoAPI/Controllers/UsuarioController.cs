using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoAPI.Models;
using ToDoAPI.Repository.DAO;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        [HttpGet("[action]")]
        public async Task<ActionResult<string>> VerificarLogin(string correo, string password)
        {
            var resultado = await Task.Run(() 
                => new UsuarioDAO().verificarLogin(correo, password));
            return Ok(resultado);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<string>> ObtenerIdUsuario(string correo)
        {
            var resultado = await Task.Run(() 
                => new UsuarioDAO().obtenerIdUsuario(correo));
            return Ok(resultado);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<string>> RegistrarUsuario([FromBody] Usuario usuario)
        {
            var resultado = await Task.Run(() => 
                new UsuarioDAO().registrarUsuario(usuario));

            return Ok(resultado);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<bool>> ExisteCorreo(string correo)
        {
            var existe = await Task.Run(() 
                => new UsuarioDAO().existeCorreo(correo));

            return Ok(existe);
        }

    }
}
