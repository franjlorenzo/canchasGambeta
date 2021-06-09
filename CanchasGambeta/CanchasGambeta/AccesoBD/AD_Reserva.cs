using CanchasGambeta.Models;
using CanchasGambeta.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

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
                comando.Parameters.AddWithValue("@cancha", reservaVM.idCancha);
                comando.Parameters.AddWithValue("@horario", reservaVM.idHorario);
                comando.Parameters.AddWithValue("@fecha", reservaVM.fecha);
                comando.Parameters.AddWithValue("@servicioAsador", reservaVM.servicioAsador);
                comando.Parameters.AddWithValue("@servicioInstrumentos", reservaVM.servicioInstrumento);
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
                string consulta = @"select idReserva, fecha, ho.horario, tipoCancha, servicioAsador, servicioInstrumentos, ho.idHorario, idCancha, estado
                                    from Reserva r join Horario ho on ho.idHorario = r.horario
                                         join Cancha c on c.idCancha = r.cancha
                                    where cliente = @idUsuario and fecha >= CURRENT_TIMESTAMP-1 and estado = 1
                                    order by 2, 3";
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
                        auxiliar.Horario = lector["horario"].ToString();
                        auxiliar.Cancha = lector["tipoCancha"].ToString();
                        auxiliar.ServicioAsador = bool.Parse(lector["servicioAsador"].ToString());
                        auxiliar.ServicioInstrumento = bool.Parse(lector["servicioInstrumentos"].ToString());
                        auxiliar.IdHorario = int.Parse(lector["idHorario"].ToString());
                        auxiliar.IdCancha = int.Parse(lector["idCancha"].ToString());
                        auxiliar.Estado = bool.Parse(lector["estado"].ToString());
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
                if (lector != null) while (lector.Read()) idReserva = int.Parse(lector["idReserva"].ToString());
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

        public static bool insertReservaInsumo(NuevaReservaVM reservaVM, List<InsumosAPedir> insumosSeleccionados)
        {
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

                foreach (var lista in insumosSeleccionados)
                {
                    comando.Parameters.Clear();
                    comando.Parameters.AddWithValue("@idReserva", reservaVM.idReserva);
                    comando.Parameters.AddWithValue("@insumo", lista.IdInsumo);
                    comando.Parameters.AddWithValue("@cantidad", lista.Cantidad);
                    comando.ExecuteNonQuery();
                }

                string consultaUpdateStock = @"update Insumo set stock = stock - @cantidad
                                                   where idInsumo = @idInsumo";
                comando.CommandText = consultaUpdateStock;

                foreach (var lista in insumosSeleccionados)
                {
                    comando.Parameters.Clear();
                    comando.Parameters.AddWithValue("@cantidad", lista.Cantidad);
                    comando.Parameters.AddWithValue("@idInsumo", lista.IdInsumo);
                    comando.ExecuteNonQuery();
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
                        insumo.precio = Math.Round(decimal.Parse(lector["precio"].ToString()), 2);
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
                string consulta = @"select fecha, cancha, r.horario, h.horario 'Hora', servicioAsador, servicioInstrumentos
                                    from Reserva r join Horario h on h.idHorario = r.horario
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
                        datosReserva.IdCancha = int.Parse(lector["cancha"].ToString());
                        datosReserva.IdHorario = int.Parse(lector["horario"].ToString());
                        datosReserva.Horario = lector["Hora"].ToString();
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

        public static bool modificarReserva(ActualizarReservaVM actualizarReservaVM, List<int> cantidadesNuevas)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();
            actualizarReservaVM.ListaInsumosEnLaReserva = null;
            actualizarReservaVM.ListaInsumosEnLaReserva = obtenerInsumosDeLaReserva(actualizarReservaVM.IdReserva); //obtengo las cantidades originales para compararlas con las cantidades nuevas

            try
            {
                string consultaUpdateReserva = @"update Reserva set cancha = @idCancha,
				                                                    horario = @idHorario,
				                                                    fecha = @fecha,
				                                                    servicioAsador = @servicioAsador,
				                                                    servicioInstrumentos = @servicioInstrumentos
                                                 where idReserva = @idReserva";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idCancha", actualizarReservaVM.IdCancha);
                comando.Parameters.AddWithValue("@idHorario", actualizarReservaVM.IdHorario);
                comando.Parameters.AddWithValue("@fecha", actualizarReservaVM.Fecha);
                comando.Parameters.AddWithValue("@servicioAsador", actualizarReservaVM.ServicioAsador);
                comando.Parameters.AddWithValue("@servicioInstrumentos", actualizarReservaVM.ServicioInstrumento);
                comando.Parameters.AddWithValue("@idReserva", actualizarReservaVM.IdReserva);

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consultaUpdateReserva;
                
                conexion.Open();
                comando.Connection = conexion;
                comando.ExecuteNonQuery();

                string updateStock = @"update Insumo set stock = stock + @cantidadUpdate
                                       where idInsumo = @idInsumo";

                string consultaUpdateReservaInsumo = @"update ReservaInsumos set cantidad = @cantidadNueva
                                                       where reserva = @idReserva and insumo = @idInsumo";

                bool remover = false;
                foreach (var cantidadVieja in actualizarReservaVM.ListaInsumosEnLaReserva)
                {
                    if(remover) cantidadesNuevas.RemoveAt(0);
                    bool banderaInsumo = true;
                    foreach (var cantidadNueva in cantidadesNuevas)
                    {
                        remover = true;
                        if (cantidadVieja.idInsumo == cantidadVieja.idInsumo && banderaInsumo == true)
                        {
                            banderaInsumo = false;
                            if (cantidadVieja.cantidad > cantidadNueva || cantidadVieja.cantidad < cantidadNueva)
                            {
                                comando.CommandText = updateStock;
                                comando.Parameters.Clear();
                                comando.Parameters.AddWithValue("@cantidadUpdate", cantidadVieja.cantidad - cantidadNueva);
                                comando.Parameters.AddWithValue("@idInsumo", cantidadVieja.idInsumo);
                                comando.ExecuteNonQuery();

                                comando.CommandText = consultaUpdateReservaInsumo;
                                comando.Parameters.Clear();
                                comando.Parameters.AddWithValue("@cantidadNueva", cantidadNueva);
                                comando.Parameters.AddWithValue("@idReserva", actualizarReservaVM.IdReserva);
                                comando.Parameters.AddWithValue("@idInsumo", cantidadVieja.idInsumo);
                                comando.ExecuteNonQuery();
                            }                           
                        }
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

        public static bool eliminarReserva(int idReserva)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consultaEliminarReserva = @"update Reserva set estado = 0
                                                  where idReserva = @idReserva";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idReserva", idReserva);

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consultaEliminarReserva;

                conexion.Open();
                comando.Connection = conexion;
                comando.ExecuteNonQuery();

                string consultaEliminarInsumosDeReserva = @"delete from ReservaInsumos 
                                                            where reserva = @idReserva and insumo = @idInsumo";
                string consultaSumarStockInsumo = @"update Insumo set stock = stock + @cantidad
                                                    where idInsumo = @idInsumo";
                List<Insumo> insumosEnReserva = obtenerInsumosDeLaReserva(idReserva);

                comando.CommandText = consultaEliminarInsumosDeReserva;
                foreach (var insumo in insumosEnReserva)
                {
                    comando.Parameters.Clear();
                    comando.Parameters.AddWithValue("@idReserva", idReserva);
                    comando.Parameters.AddWithValue("@idInsumo", insumo.idInsumo);
                    comando.ExecuteNonQuery();
                }

                comando.CommandText = consultaSumarStockInsumo;
                foreach (var insumo in insumosEnReserva)
                {
                    comando.Parameters.Clear();
                    comando.Parameters.AddWithValue("@cantidad", insumo.cantidad);
                    comando.Parameters.AddWithValue("@idInsumo", insumo.idInsumo);
                    comando.ExecuteNonQuery();
                }

                string consultaEliminarHorarioReserva = @"delete from HorarioReservas 
                                                          where reserva = @idReserva";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idReserva", idReserva);

                comando.CommandText = consultaEliminarHorarioReserva;
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

        public static List<SelectListItem> obtenerHorariosCancha(DateTime fecha, int idCancha)
        {
            List<Horario> listaHorario = new List<Horario>();
            List<SelectListItem> listaHorariosCancha = new List<SelectListItem>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select idHorario, horario
                                    from Horario h
                                    where idHorario not in (select horario
						                                    from Reserva
						                                    where cancha = @idCancha and fecha = @fecha and estado = 1)";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@fecha", fecha);
                comando.Parameters.AddWithValue("@idCancha", idCancha);

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        Horario horarioDisponible = new Horario();
                        horarioDisponible.idHorario = int.Parse(lector["idHorario"].ToString());
                        horarioDisponible.horario1 = lector["horario"].ToString();
                        listaHorario.Add(horarioDisponible);
                    }

                    listaHorariosCancha = listaHorario.ConvertAll(d =>
                    {
                        return new SelectListItem()
                        {
                            Text = d.horario1,
                            Value = d.idHorario.ToString(),
                            Selected = false
                        };
                    });
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
            return listaHorariosCancha;
        }

        public static bool insertHorarioReservas(int idReserva, int idHorario)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"insert into HorarioReservas (horario, reserva) values (@idHorario, @idReserva)";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idHorario", idHorario);
                comando.Parameters.AddWithValue("@idReserva", idReserva);

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

        public static DatosReserva obtenerDatosReserva(int idReserva)
        {
            DatosReserva reserva = new DatosReserva();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select fecha, tipoCancha, h.horario
                                    from Reserva r join Horario h on h.idHorario = r.horario
                                         join Cancha c on c.idCancha = r.cancha
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
                        reserva.Fecha = DateTime.Parse(lector["fecha"].ToString());
                        reserva.TipoCancha = lector["tipoCancha"].ToString();
                        reserva.Horario = lector["horario"].ToString();
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
            return reserva;
        }

        public static List<Insumo> armarListaInsumos(List<int> listaInsumosActualizados, List<int> listaidInsumos)
        {
            List<Insumo> listaInsumos = new List<Insumo>();
            bool remover = false;

            foreach (var insumosNuevos in listaInsumosActualizados)
            {
                if (remover) listaidInsumos.RemoveAt(0);
                foreach (var idInsumos in listaidInsumos)
                {
                    remover = true;
                    Insumo insumo = new Insumo();
                    insumo.idInsumo = idInsumos;
                    insumo.cantidad = insumosNuevos;
                    listaInsumos.Add(insumo);
                    break;
                }
            }

            return listaInsumos;
        }

        public static bool seActualizoInsumo(List<Insumo> listaInsumosNuevos, List<Insumo> listaInsumosOriginales)
        {            
            bool seActualizoInsumo = false;
            bool remover = false;
            foreach (var insumosNuevos in listaInsumosNuevos)
            {
                if (remover) listaInsumosOriginales.RemoveAt(0);
                foreach (var insumosOriginales in listaInsumosOriginales)
                {
                    remover = true;
                    if (insumosNuevos.idInsumo == insumosOriginales.idInsumo)
                    {
                        if (insumosNuevos.cantidad != insumosOriginales.cantidad)
                        {
                            seActualizoInsumo = true;
                            break;
                        }
                    }
                }
            }
            return seActualizoInsumo;
        }

        public static void enviarMailsReserva(int idReserva, int tipoMensaje, [Optional] DatosReserva datosReservaEliminada)
        {
            SmtpClient smtpClient = new SmtpClient();
            Usuario sesion = (Usuario)HttpContext.Current.Session["User"];
            DatosReserva reservaCliente = null;
            if(datosReservaEliminada == null) reservaCliente = obtenerDatosReserva(idReserva);
            List<MailEquipoVM> listaIntegrantesEquipo = AD_Equipo.obtenerMailsEquipo(sesion.equipo);
            string mensaje = "";
            string insumos = "";
            string titulo = "";

            if(tipoMensaje == 1) //Nueva reserva
            {
                mensaje = $"Hola, su compañero de equipo hizo una reserva para el día {reservaCliente.Fecha} a las {reservaCliente.Horario} en la cancha {reservaCliente.TipoCancha}";
                titulo = "Nueva reserva!";
            }
            else if(tipoMensaje == 2) //Update reserva
            {
                mensaje = $"Hola de nuevo! su compañero de equipo hizo una modificación de la reserva y es el día {reservaCliente.Fecha} a las {reservaCliente.Horario} en la cancha {reservaCliente.TipoCancha}";
                titulo = "Reserva modificada";
            }
            else if(tipoMensaje == 3) //Update insumos de la reserva
            {
                List<Insumo> listaInsumosEnReserva = obtenerInsumosDeLaReserva(idReserva);
                foreach (var item in listaInsumosEnReserva)
                {
                    insumos = insumos + $"- {item.insumo1}({item.cantidad})\n";
                }
                mensaje = $"Hola de nuevo! su compañero de equipo hizo una modificación de la reserva y es el día {reservaCliente.Fecha} a las {reservaCliente.Horario} en la cancha {reservaCliente.TipoCancha} y con los siguientes insumos:\n{insumos}";
                titulo = "Reserva modificada";
            }
            else //Delete reserva
            {
                mensaje = $"Hola, su compañero de equipo dio de baja la reserva del día {datosReservaEliminada.Fecha} a las {datosReservaEliminada.Horario} en la cancha {datosReservaEliminada.TipoCancha}";
                titulo = "Reserva eliminada";
            }
                       
            foreach (var lista in listaIntegrantesEquipo)
            {
                smtpClient.Send("canchasgambeta@gmail.com", lista.Email, titulo, mensaje);
            }
        }

        public static List<InsumosAPedir> armarListaInsumosSeleccionados(List<int> listaIdInsumo, List<string> listaNombreInsumo, List<int> cantidadInsumo)
        {
            List<InsumosAPedir> listaInsumosAPedir = new List<InsumosAPedir>();

            for (int i = 0; i < listaIdInsumo.Count; i++)
            {
                InsumosAPedir insumo = new InsumosAPedir();
                insumo.IdInsumo = listaIdInsumo[i];
                insumo.Insumo = listaNombreInsumo[i];
                insumo.Cantidad = cantidadInsumo[i];
                listaInsumosAPedir.Add(insumo);
            }

            return listaInsumosAPedir;
        }
    }
}