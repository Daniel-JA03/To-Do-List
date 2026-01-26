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
            // Obtener la tarea actual desde la base de datos usando el id de la tarea recibida
            var tareaActual = await Task.Run(() => new TareaDAO().obtenerTareaPorId(tarea.ide_tar));

            if (tarea.estado == "COMPLETADO" && tareaActual != null && tareaActual.estado != "COMPLETADO")
            {
                bool conRetraso = false;
                if (tareaActual.fecha_limite.HasValue)
                {
                    // Comparar en UTC para evitar problemas de zona horaria
                    if (DateTime.UtcNow > tareaActual.fecha_limite.Value)
                    {
                        conRetraso = true;
                    }
                }

                // Aquí podrías guardar "conRetraso" en la base de datos
                // o simplemente usarlo para notificaciones
            }

            // Actualiza la tarea
            return Ok(await Task.Run(() => new TareaDAO().actualizarTarea(tarea)));
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
