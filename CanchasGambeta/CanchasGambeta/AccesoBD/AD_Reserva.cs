using CanchasGambeta.Models;
using CanchasGambeta.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CanchasGambeta.AccesoBD
{
    public class AD_Reserva
    {
        public static string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();

        public static List<Cancha> obtenerCanchas()
        {
            List<Cancha> lista = new List<Cancha>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select idCancha, tipoCancha from Cancha";

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        Cancha auxiliar = new Cancha();
                        auxiliar.idCancha = int.Parse(lector["idCancha"].ToString());
                        auxiliar.tipoCancha = lector["tipoCancha"].ToString();
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

        public static List<Horario> obtenerHorarios()
        {
            List<Horario> lista = new List<Horario>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select idHorario, horario from Horario";

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        Horario auxiliar = new Horario();
                        auxiliar.idHorario = int.Parse(lector["idHorario"].ToString());
                        auxiliar.horario1 = lector["horario"].ToString();
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

        public static bool nuevaReserva(NuevaReservaVM reservaVM)
        {
            bool resultado = false;
            Usuario sesion = (Usuario)HttpContext.Current.Session["User"];
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"insert into Reserva(cliente, cancha, horario, fecha, servicioAsador, servicioInstrumentos, estado)
                                    values (@cliente, @cancha, @horario, @fecha, @servicioAsador, @servicioInstrumentos, @estado)";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@cliente", sesion.idUsuario);
                comando.Parameters.AddWithValue("@cancha", reservaVM.IdCancha);
                comando.Parameters.AddWithValue("@horario", reservaVM.IdHorario);
                comando.Parameters.AddWithValue("@fecha", reservaVM.Fecha);
                comando.Parameters.AddWithValue("@servicioAsador", reservaVM.ServicioAsador);
                comando.Parameters.AddWithValue("@servicioInstrumentos", reservaVM.ServicioInstrumento);
                comando.Parameters.AddWithValue("@estado", true);

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

        public static List<TablaReservaVM> obtenerReservasDelCliente()
        {
            List<TablaReservaVM> lista = new List<TablaReservaVM>();
            Usuario sesion = (Usuario)HttpContext.Current.Session["User"];
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select fecha, ho.horario, tipoCancha, servicioAsador, servicioInstrumentos, ho.idHorario, idCancha
                                    from Reserva r join Horario ho on ho.idHorario = r.horario
                                         join Cancha c on c.idCancha = r.cancha
                                    where cliente = @idUsuario and day(fecha) >= day(getdate())";
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
                        TablaReservaVM auxiliar = new TablaReservaVM();
                        auxiliar.Fecha = DateTime.Parse(lector["fecha"].ToString());
                        //auxiliar.Fecha = auxiliar.Fecha.Date;
                        auxiliar.Horario = lector["horario"].ToString();
                        auxiliar.Cancha = lector["tipoCancha"].ToString();
                        auxiliar.ServicioAsador = bool.Parse(lector["servicioAsador"].ToString());
                        auxiliar.ServicioInstrumento = bool.Parse(lector["servicioInstrumentos"].ToString());
                        auxiliar.IdHorario = int.Parse(lector["idHorario"].ToString());
                        auxiliar.IdCancha = int.Parse(lector["idCancha"].ToString());
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
    }
}