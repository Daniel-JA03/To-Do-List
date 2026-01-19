using ToDoAPI.Models;
using ToDoAPI.Repository.Interfaces;

using System.Data;
using System.Data.SqlClient;

namespace ToDoAPI.Repository.DAO
{
    public class UsuarioDAO : IUsuario
    {
        private readonly string _connectionString;

        public UsuarioDAO()
        {
            _connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json")
            .Build().GetConnectionString("cn") ?? throw new NullReferenceException();
        }

        public string verificarLogin(string correo, string password)
        {
            string resultado = "denied";
            SqlConnection cn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_verificarLogin", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@correo", correo);
            cmd.Parameters.AddWithValue("@contraseña", password);
            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    throw new Exception("No se ha encontrado el usuario");
                }
                if (dr.Read())
                {
                    resultado = dr.GetString(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cn.Close();
            }
            return resultado;
        }

        public string obtenerIdUsuario(string correo)
        {
            string resultado = "denied";
            SqlConnection cn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_obtenerIdUsuario", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@correo", correo);
            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    throw new Exception("No se ha encontrado id con el correo");
                }
                if (dr.Read())
                {
                    resultado = dr[0].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cn.Close();
            }
            return resultado;
        }

        public string registrarUsuario(Usuario usuario)
        {
            try
            {
                SqlConnection cn = new SqlConnection(_connectionString);
                SqlCommand cmd = new SqlCommand("sp_registrarUsuario", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@correo", usuario.cor_usr);
                cmd.Parameters.AddWithValue("@contraseña", usuario.pwd_usr);
                cmd.Parameters.AddWithValue("@nombre", usuario.nom_usr);
                cmd.Parameters.AddWithValue("@apellido", usuario.ape_usr);

                cn.Open();
                cmd.ExecuteNonQuery();

                return "Usuario registrado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al registrar usuario: " +  ex.Message;
            }
        }

        public bool existeCorreo(string correo)
        {
            bool existe = false;

            SqlConnection cn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_existeCorreo", cn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@correo", correo);

            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                existe = Convert.ToInt32(dr[0]) == 1;
            }

            return existe;
        }
    }
}
