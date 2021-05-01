using CanchasGambeta.ViewModels;
using CanchasGambeta.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CanchasGambeta.AccesoBD
{
    public class AD_Equipo
    {
        public static string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();

        public static bool nuevoEquipo(string nombre)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {                
                string consultaInsertEquipo = "insert into Equipo(nombreEquipo) values (@nombreEquipo)";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@nombreEquipo", nombre);
                

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consultaInsertEquipo;

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

        public static int obtenerEquipo(string nombreEquipo)
        {
            int idEquiponuevo = 0;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = "SELECT idEquipo from Equipo where nombreEquipo = @nombreEquipo";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@nombreEquipo", nombreEquipo);

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        idEquiponuevo = int.Parse(lector["idEquipo"].ToString());
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
            return idEquiponuevo;
        }

        public static bool existeEquipo(string nombreEquipo)
        {
            bool resultado = false;
            List<string> listaNombres = new List<string>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = "SELECT nombreEquipo from Equipo";
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
                        string nombre = lector["nombreEquipo"].ToString();
                        listaNombres.Add(nombre);
                    }
                }

                foreach (string nombreEnLista in listaNombres)
                {
                    if(nombreEnLista == nombreEquipo)
                    {
                        resultado = true;
                        return resultado;
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

        public static bool insertarEquipoEnUsuario(string nombreEquipo)
        {
            bool resultado = false;
            Usuario sesion = (Usuario)HttpContext.Current.Session["User"];
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consultaInsertarEquipoUsuario = @"update Usuario set equipo = @idEquipo
                                                       where idUsuario = @idUsuario";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idEquipo", obtenerEquipo(nombreEquipo));
                comando.Parameters.AddWithValue("@idUsuario", sesion.idUsuario);

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consultaInsertarEquipoUsuario;

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

        public static List<MailEquipoVM> obtenerMailsEquipo(int idEquipo)
        {
            List<MailEquipoVM> lista = new List<MailEquipoVM>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select eq.nombreEquipo, em.email, em.idEmail
                                    from EquipoMails eqma join Equipo eq on eqma.equipo = eq.idEquipo join Email em on em.idEmail = eqma.email
                                    where eq.idEquipo = @idEquipo ";

                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idEquipo", idEquipo);

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        MailEquipoVM auxiliar = new MailEquipoVM();
                        auxiliar.NombreEquipo = lector["nombreEquipo"].ToString();
                        auxiliar.Email = lector["email"].ToString();
                        auxiliar.IdEmail = int.Parse(lector["idEmail"].ToString());

                        lista.Add(auxiliar);
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
            return lista;
        }

        public static int obtenerEquiporPorId()
        {
            int idEquipo = 0;
            Usuario sesion = (Usuario)HttpContext.Current.Session["User"];
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = "  select equipo from Usuario where idUsuario = @idUsuario";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idUsuario", sesion.idUsuario);

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        idEquipo = int.Parse(lector["equipo"].ToString());
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
            return idEquipo;
        }

        public static string obtenerNombreEquipo()
        {
            string nombreEquipo = "";
            Usuario sesion = (Usuario)HttpContext.Current.Session["User"];
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select nombreEquipo
                                   from Equipo e join Usuario u on e.idEquipo = @idEquipo";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idEquipo", sesion.equipo);

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        nombreEquipo = lector["nombreEquipo"].ToString();
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
            return nombreEquipo;
        }

        public static bool agregarNuevoIntegrante(string email)
        {
            bool resultado = false;
            Usuario sesion = (Usuario)HttpContext.Current.Session["User"];
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                if (existeEmail(email))
                {
                    string consultaInsertMailEnEquipo = @"insert into EquipoMails (email, equipo) values (@email, @equipo)";
                    comando.Parameters.Clear();
                    comando.Parameters.AddWithValue("@email", obtenerIdEmail(email));
                    comando.Parameters.AddWithValue("@equipo", sesion.equipo);

                    comando.CommandType = System.Data.CommandType.Text;
                    comando.CommandText = consultaInsertMailEnEquipo;

                    conexion.Open();
                    comando.Connection = conexion; 
                    comando.ExecuteNonQuery();
                    resultado = true;

                }
                else
                {
                    string consultaInsertNuevoEmail = @"insert into Email (email) values (@email)";
                    comando.Parameters.Clear();
                    comando.Parameters.AddWithValue("@email", email);

                    comando.CommandType = System.Data.CommandType.Text;
                    comando.CommandText = consultaInsertNuevoEmail;

                    conexion.Open();
                    comando.Connection = conexion;
                    comando.ExecuteNonQuery();

                    string consultaInsertMailEnEquipo = @"insert into EquipoMails (email, equipo) values (@email, @equipo)";
                    comando.Parameters.Clear();
                    comando.Parameters.AddWithValue("@email", obtenerIdEmail(email));
                    comando.Parameters.AddWithValue("@equipo", sesion.equipo);

                    comando.CommandText = consultaInsertMailEnEquipo;

                    comando.ExecuteNonQuery();
                    resultado = true;
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

        public static bool existeEmail(string email)
        {
            bool resultado = false;
            List<string> listaEmails = new List<string>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = "SELECT email from Email";
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

                foreach (string nombreEnLista in listaEmails)
                {
                    if (nombreEnLista == email)
                    {
                        resultado = true;
                        return resultado;
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

        public static int obtenerIdEmail(string email)
        {
            int idEmail = 0;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {               
                string consulta = "select idEmail from Email where email = @email";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@email", email);

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        idEmail = int.Parse(lector["idEmail"].ToString());
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
            return idEmail;
        }

        public static bool modificarIntegrante(Email email)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"update Email set email = @email
                                    where idEmail = @idEmail";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@email", email.email1);
                comando.Parameters.AddWithValue("@idEmail", email.idEmail);

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

        public static bool eliminarIntegrante(Email email)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consultaEliminarEquipoMails = "delete from EquipoMails where email = @email";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@email", email.idEmail);

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consultaEliminarEquipoMails;

                conexion.Open();
                comando.Connection = conexion;
                comando.ExecuteNonQuery();

                string consultaEliminarEmail = "delete from Email where idEmail = @idEmail";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idEmail", email.idEmail);

                comando.CommandText = consultaEliminarEmail;
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
    }
}