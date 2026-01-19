using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Models;
using ToDoAPI.Repository.DAO;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        [HttpGet("ListarTareasPorUsuario/{usuarioId}")]
        public async Task<ActionResult<List<Tarea>>> ListarTareasPorUsuario(long usuarioId)
        {
            var lista = await Task.Run(() =>
                new TareaDAO().listarTareasPorUsuario(usuarioId));

            return Ok(lista);
        }

        [HttpGet("ObtenerTareaPorId/{id}")]
        public async Task<ActionResult<Tarea>> ObtenerTareaPorId(long id)
        {
            var tarea = await Task.Run(() =>
                new TareaDAO().obtenerTareaPorId(id));

            return Ok(tarea);
        }

        [HttpPost("AgregarTarea")]
        public async Task<ActionResult<string>> AgregarTarea([FromBody] Tarea tarea)
        {
            var mensaje = await Task.Run(() =>
                new TareaDAO().agregarTarea(tarea));

            return Ok(mensaje);
        }

        [HttpPut("ActualizarTarea")]
        public async Task<ActionResult<string>> ActualizarTarea([FromBody] Tarea tarea)
        {
            var mensaje = await Task.Run(() =>
                new TareaDAO().actualizarTarea(tarea));

            return Ok(mensaje);
        }

        [HttpDelete("EliminarTarea/{id}")]
        public async Task<ActionResult<string>> EliminarTarea(long id)
        {
            var mensaje = await Task.Run(() =>
                new TareaDAO().eliminarTarea(id));

            return Ok(mensaje);
        }
    }
}
