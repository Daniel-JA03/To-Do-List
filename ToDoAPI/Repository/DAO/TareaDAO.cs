using System.Data;
using System.Data.SqlClient;
using ToDoAPI.Models;
using ToDoAPI.Repository.Interfaces;

namespace ToDoAPI.Repository.DAO
{
    public class TareaDAO : ITarea
    {
        private readonly string _connectionString;

        public TareaDAO()
        {
            _connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json")
            .Build().GetConnectionString("cn") ?? throw new NullReferenceException();
        }

        public IEnumerable<Tarea> listarTareasPorUsuario(long usuarioId)
        {
            List<Tarea> tareas = new();
            SqlConnection cn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_listarTareasPorUsuario", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ide_usr", usuarioId);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                tareas.Add(new Tarea
                {
                    ide_tar = Convert.ToInt64(dr["ide_tar"]),
                    titulo = dr["titulo"].ToString()!,
                    descripcion = dr["descripcion"].ToString(),
                    estado = dr["estado"].ToString()!,
                    fecha_limite = dr["fecha_limite"] as DateTime?,
                    usuario_id = Convert.ToInt64(dr["usuario_id"]),
                    fecha_creacion = Convert.ToDateTime(dr["fecha_creacion"]),
                    fecha_actualizacion = Convert.ToDateTime(dr["fecha_actualizacion"])
                });
            }

            return tareas;
        }

        public Tarea obtenerTareaPorId(long id)
        {
            Tarea tarea = new Tarea();

            SqlConnection cn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_obtenerTareaPorId", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                tarea = new Tarea
                {
                    ide_tar = Convert.ToInt64(dr["ide_tar"]),
                    titulo = dr["titulo"].ToString()!,
                    descripcion = dr["descripcion"].ToString(),
                    estado = dr["estado"].ToString()!,
                    fecha_limite = dr["fecha_limite"] as DateTime?,
                    usuario_id = Convert.ToInt64(dr["usuario_id"]),
                    fecha_creacion = Convert.ToDateTime(dr["fecha_creacion"]),
                    fecha_actualizacion = Convert.ToDateTime(dr["fecha_actualizacion"])
                };
            }

            return tarea;
        }

        public string agregarTarea(Tarea tarea)
        {
            string mensaje = "";
            SqlConnection cn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_agregarTarea", cn);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@titulo", tarea.titulo);
                cmd.Parameters.AddWithValue("@descripcion", tarea.descripcion ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@fecha_limite", tarea.fecha_limite ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@usuario_id", tarea.usuario_id);
                cn.Open();
                cmd.ExecuteNonQuery();
                mensaje = "Tarea guardada correctamente";
            }
            catch (Exception ex)
            {
                mensaje = "Error al guardar tarea" + ex.Message;
            }
            finally
            {
                cn.Close();
            }
            return mensaje;
        }

        public string actualizarTarea(Tarea tarea)
        {
            string mensaje = "";
            SqlConnection cn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_actualizarTarea", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", tarea.ide_tar);
            cmd.Parameters.AddWithValue("@titulo", tarea.titulo);
            cmd.Parameters.AddWithValue("@descripcion", tarea.descripcion ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@estado", tarea.estado);
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                mensaje = "Tarea actualizada correctamente";
            }
            catch (Exception ex)
            {
                mensaje = "Error al actualizar tarea: " + ex.Message;
            }
            finally
            {
                cn.Close();
            }
            return mensaje;
        }

        public string eliminarTarea(long id)
        {
            string mensaje = "";
            SqlConnection cn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_eliminarTarea", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                mensaje = "Tarea eliminado correctamente";
            }
            catch (Exception ex)
            {
                mensaje = "Tarea eliminado correctamente";
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                cn.Close();
            }
            return mensaje;
        }
    }
}
