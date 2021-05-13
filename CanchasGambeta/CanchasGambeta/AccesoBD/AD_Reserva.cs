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
                string consulta = @"select idReserva, fecha, ho.horario, tipoCancha, servicioAsador, servicioInstrumentos, ho.idHorario, idCancha
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
                        auxiliar.IdReserva = int.Parse(lector["idReserva"].ToString());
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

        public static int obtenerReservaPorAtributos(int cancha, int horario, DateTime fecha)
        {
            int idReserva = 0;
            Usuario sesion = (Usuario)HttpContext.Current.Session["User"];
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select idReserva
                                    from Reserva r join Horario h on r.horario = h.idHorario
                                         join Cancha c on c.idCancha = r.cancha
	                                where h.idHorario = @horario and c.idCancha = @cancha and cliente = @idCliente and fecha = @fecha";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@horario", horario);
                comando.Parameters.AddWithValue("@cancha", cancha);
                comando.Parameters.AddWithValue("@idCliente", sesion.idUsuario);
                comando.Parameters.AddWithValue("@fecha", fecha);

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        idReserva = int.Parse(lector["idReserva"].ToString());
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
            return idReserva;
        }

        public static bool insertReservaInsumo(NuevaReservaVM reservaVM, int idReserva, List<int> insumosSeleccionados)
        {
            bool updateStock = false;
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"insert into ReservaInsumos (reserva, insumo, cantidad) values (@idReserva, @insumo, @cantidad)";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;
                conexion.Open();
                comando.Connection = conexion;

                for (int i = 0; i < insumosSeleccionados.Count; i++)
                {
                    if (insumosSeleccionados[i] != 0) { updateStock = true; }
                    comando.Parameters.Clear();
                    comando.Parameters.AddWithValue("@idReserva", idReserva);
                    comando.Parameters.AddWithValue("@insumo", reservaVM.ListaInsumos[i].idInsumo);
                    comando.Parameters.AddWithValue("@cantidad", insumosSeleccionados[i]);

                    comando.ExecuteNonQuery();
                }

                if (updateStock)
                {
                    string consultaUpdateStock = @"update Insumo set stock = stock - @cantidad
                                                   where idInsumo = @idInsumo";
                    comando.CommandText = consultaUpdateStock;
                    for (int i = 0; i < insumosSeleccionados.Count; i++)
                    {
                        comando.Parameters.Clear();
                        comando.Parameters.AddWithValue("@cantidad", insumosSeleccionados[i]);
                        comando.Parameters.AddWithValue("@idInsumo", reservaVM.ListaInsumos[i].idInsumo);

                        comando.ExecuteNonQuery();
                    }
                }

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

        public static Cancha obtenerCanchaPorId(int idReserva)
        {
            Cancha cancha = new Cancha();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select idCancha, tipoCancha from Cancha c join Reserva r on c.idCancha = r.cancha
                                    where idReserva = @idReserva";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idReserva", idReserva);

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        cancha.idCancha = int.Parse(lector["idCancha"].ToString());
                        cancha.tipoCancha = lector["tipoCancha"].ToString();
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
            return cancha;
        }

        public static Horario obtenerHorarioPorId(int idReserva)
        {
            Horario horario = new Horario();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select idHorario, h.horario from Horario h join Reserva r on h.idHorario = r.horario
                                    where idReserva = @idReserva";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idReserva", idReserva);

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        horario.idHorario = int.Parse(lector["idHorario"].ToString());
                        horario.horario1 = lector["horario"].ToString();
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
            return horario;
        }

        public static List<Insumo> obtenerInsumosDeLaReserva(int idReserva)
        {
            List<Insumo> listaInsumosEnReserva = new List<Insumo>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select idInsumo, i.insumo, precio, stock, cantidad 
                                    from Insumo i join ReservaInsumos ri on i.idInsumo = ri.insumo
	                                     join Reserva r on r.idReserva = ri.reserva
                                    where idReserva = @idReserva";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idReserva", idReserva);

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        Insumo insumo = new Insumo();
                        insumo.idInsumo = int.Parse(lector["idInsumo"].ToString());
                        insumo.insumo1 = lector["insumo"].ToString();
                        insumo.precio = decimal.Parse(lector["precio"].ToString());
                        insumo.stock = int.Parse(lector["stock"].ToString());
                        insumo.cantidad = int.Parse(lector["cantidad"].ToString());
                        listaInsumosEnReserva.Add(insumo);
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
            return listaInsumosEnReserva;
        }

        public static ActualizarReservaVM obtenerReservaPorId(int idReserva)
        {
            ActualizarReservaVM datosReserva = new ActualizarReservaVM();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select fecha, servicioAsador, servicioInstrumentos
                                    from Reserva r
                                    where idReserva = @idReserva";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idReserva", idReserva);

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        datosReserva.Fecha = DateTime.Parse(lector["fecha"].ToString());
                        datosReserva.ServicioAsador = bool.Parse(lector["servicioAsador"].ToString());
                        datosReserva.ServicioInstrumento = bool.Parse(lector["servicioInstrumentos"].ToString());
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
            return datosReserva;
        }
    }
}