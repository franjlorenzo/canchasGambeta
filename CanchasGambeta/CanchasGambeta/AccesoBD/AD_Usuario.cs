using CanchasGambeta.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data.SqlTypes;

namespace CanchasGambeta.AccesoBD
{
    public class AD_Usuario
    {
        public static string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();

        public static bool nuevoUsuario(Usuario usuario)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = "insert into Usuario(nombreCompleto, email, telefono, password, rol) values (@nombreCompleto, @email, @telefono, @password, @rol)";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@nombreCompleto", usuario.nombreCompleto);
                comando.Parameters.AddWithValue("@email", usuario.email);
                comando.Parameters.AddWithValue("@telefono", usuario.telefono);
                comando.Parameters.AddWithValue("@password", usuario.password);
                comando.Parameters.AddWithValue("@rol", 2);

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;
                comando.ExecuteNonQuery();
                resultado = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
            return resultado;
        }

        public static Usuario obtenerUsuario(int idUsuario)
        {
            Usuario resultado = new Usuario();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = "SELECT idUsuario, nombreCompleto, email, telefono, password, equipo, rol FROM Usuario WHERE idUsuario = @idUsuario";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idUsuario", idUsuario);

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        resultado.idUsuario = int.Parse(lector["idUsuario"].ToString());
                        resultado.nombreCompleto = lector["nombreCompleto"].ToString();
                        resultado.email = lector["email"].ToString();
                        resultado.telefono = lector["telefono"].ToString();
                        resultado.password = lector["password"].ToString();
                        if (!string.IsNullOrEmpty(lector["equipo"].ToString())) resultado.equipo = int.Parse(lector["equipo"].ToString());
                        resultado.rol = int.Parse(lector["rol"].ToString());
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
            return resultado;
        }

        public static bool actualizarUsuario(Usuario usuario)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"update Usuario SET nombreCompleto = @nombreCompleto,
                                                       email = @email,
                                                       telefono = @telefono,
                                                       password = @password
                                                       WHERE idUsuario = @idUsuario";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idUsuario", usuario.idUsuario);
                comando.Parameters.AddWithValue("@nombreCompleto", usuario.nombreCompleto);
                comando.Parameters.AddWithValue("@email", usuario.email);
                comando.Parameters.AddWithValue("@telefono", usuario.telefono);
                comando.Parameters.AddWithValue("@password", usuario.password);

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;
                comando.ExecuteNonQuery();
                resultado = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
            return resultado;
        }

        public static bool existeEmailUsuario(string email)
        {
            bool resultado = false;
            List<string> listaEmails = new List<string>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = "select email from Usuario";
                comando.Parameters.Clear();

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        string nuevo = lector["email"].ToString();
                        listaEmails.Add(nuevo);
                    }
                }

                foreach (string nombreEnLista in listaEmails) if (nombreEnLista == email) return resultado = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
            return resultado;
        }
    }
}