using CanchasGambeta.Models;
using CanchasGambeta.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CanchasGambeta.AccesoBD
{
    public class AD_Informe
    {
        public static string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
        public static List<ReservasActivas> obtenerReservasActivas()
        {
            List<ReservasActivas> listaReservas = new List<ReservasActivas>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select nombreCompleto, tipoCancha, h.horario, fecha, servicioAsador, servicioInstrumentos, estado
                                    from Usuario u join Reserva r on u.idUsuario = r.cliente
                                         join Cancha c on c.idCancha = r.cancha
	                                     join Horario h on h.idHorario = r.horario
                                    where fecha >= GETDATE()-1 and estado = 1
                                    order by fecha";

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        ReservasActivas auxiliar = new ReservasActivas();
                        auxiliar.NombreCompleto = lector["nombreCompleto"].ToString();
                        auxiliar.TipoCancha = lector["tipoCancha"].ToString();
                        auxiliar.Horario = lector["horario"].ToString();
                        auxiliar.Fecha = DateTime.Parse(lector["fecha"].ToString());
                        auxiliar.ServicioAsador = bool.Parse(lector["servicioAsador"].ToString());
                        auxiliar.ServicioInstrumento = bool.Parse(lector["servicioInstrumentos"].ToString());
                        listaReservas.Add(auxiliar);
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
            return listaReservas;
        }

        public static List<ReservasCanceladas> obtenerReservasCanceladas()
        {
            List<ReservasCanceladas> listaReservas = new List<ReservasCanceladas>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select nombreCompleto, tipoCancha, h.horario, fecha, servicioAsador, servicioInstrumentos, estado
                                    from Usuario u join Reserva r on u.idUsuario = r.cliente
                                         join Cancha c on c.idCancha = r.cancha
	                                     join Horario h on h.idHorario = r.horario
                                    where fecha >= GETDATE()-1 and estado = 0
                                    order by fecha";

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        ReservasCanceladas auxiliar = new ReservasCanceladas();
                        auxiliar.NombreCompleto = lector["nombreCompleto"].ToString();
                        auxiliar.TipoCancha = lector["tipoCancha"].ToString();
                        auxiliar.Horario = lector["horario"].ToString();
                        auxiliar.Fecha = DateTime.Parse(lector["fecha"].ToString());
                        auxiliar.ServicioAsador = bool.Parse(lector["servicioAsador"].ToString());
                        auxiliar.ServicioInstrumento = bool.Parse(lector["servicioInstrumentos"].ToString());
                        listaReservas.Add(auxiliar);
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
            return listaReservas;
        }

        public static List<CanchasMasReservadasVM> obtenerCanchasMasReservadas()
        {
            List<CanchasMasReservadasVM> listaCanchasMasReservadas = new List<CanchasMasReservadasVM>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select tipoCancha, count(*) 'Veces utilizada'
                                    from Cancha c join Reserva r on r.cancha = c.idCancha
                                    where estado = 1
                                    group by tipoCancha";

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        CanchasMasReservadasVM auxiliar = new CanchasMasReservadasVM();
                        auxiliar.Cancha = lector["tipoCancha"].ToString();
                        auxiliar.Cantidad = int.Parse(lector["Veces utilizada"].ToString());
                        listaCanchasMasReservadas.Add(auxiliar);
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
            return listaCanchasMasReservadas;
        }

        public static List<ClientesConMasReservas> obtenerClientesConMasReservas()
        {
            List<ClientesConMasReservas> listaClientesConMasReservas = new List<ClientesConMasReservas>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select nombreCompleto, email, telefono, count(*) 'Cant reservas'
                                    from Usuario u join Reserva r on u.idUsuario = r.cliente
                                    where estado = 1
                                    group by nombreCompleto, email, telefono";

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        ClientesConMasReservas auxiliar = new ClientesConMasReservas();
                        auxiliar.NombreCompleto = lector["nombreCompleto"].ToString();
                        auxiliar.Email = lector["email"].ToString();
                        auxiliar.Telefono = lector["telefono"].ToString();
                        auxiliar.CantidadReservas = int.Parse(lector["Cant reservas"].ToString());
                        listaClientesConMasReservas.Add(auxiliar);
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
            return listaClientesConMasReservas;
        }
    }
}