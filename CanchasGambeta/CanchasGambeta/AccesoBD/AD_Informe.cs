using CanchasGambeta.Models;
using CanchasGambeta.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CanchasGambeta.AccesoBD
{
    public class AD_Informe
    {
        public static string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();

        public static List<ReservasActivas> obtenerReservasActivas(DateTime fechaInicio, DateTime fechaFin)
        {
            List<ReservasActivas> listaReservas = new List<ReservasActivas>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select r.idReserva, nombreCompleto, tipoCancha, h.horario, fecha, servicioAsador, servicioInstrumentos, estado
                                    from Usuario u join Reserva r on u.idUsuario = r.cliente
                                         join Cancha c on c.idCancha = r.cancha
	                                     join Horario h on h.idHorario = r.horario
                                    where fecha between CAST(@fechaInicio AS date) and CAST(@fechaFin AS date) and estado = 1
                                    order by fecha, horario";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                comando.Parameters.AddWithValue("@fechaFin", fechaFin);

                comando.CommandType = CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        ReservasActivas auxiliar = new ReservasActivas();
                        auxiliar.IdReserva = int.Parse(lector["idReserva"].ToString());
                        auxiliar.NombreCompleto = lector["nombreCompleto"].ToString();
                        auxiliar.TipoCancha = lector["tipoCancha"].ToString();
                        auxiliar.Horario = lector["horario"].ToString();
                        auxiliar.Fecha = DateTime.Parse(lector["fecha"].ToString());
                        auxiliar.ServicioAsador = bool.Parse(lector["servicioAsador"].ToString());
                        auxiliar.ServicioInstrumento = bool.Parse(lector["servicioInstrumentos"].ToString());
                        auxiliar.ListaInsumosReserva = AD_Reserva.obtenerInsumosDeLaReserva(auxiliar.IdReserva);
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

        public static List<Insumo> obtenerTotalInsumosReservas(List<ReservasActivas> listaReservas)
        {
            List<Insumo> listaTotalInsumos = new List<Insumo>();

            foreach (var itemReservas in listaReservas) //Recorro la lista de las reservas activas
            {
                foreach (var itemListaInsumos in itemReservas.ListaInsumosReserva) //Recorro la lista de insumos reservados dentro de la lista de reservas
                {
                    if (listaTotalInsumos.Exists(insumo => insumo.idInsumo == itemListaInsumos.idInsumo)) //Si el insumo existe en la lista del total se suma la cantidad
                    {
                        foreach (var item in listaTotalInsumos)
                        {
                            if (item.idInsumo == itemListaInsumos.idInsumo)
                            {
                                item.cantidad += itemListaInsumos.cantidad;
                            }
                        }
                    }
                    else
                    {
                        listaTotalInsumos.Add(itemListaInsumos);//Si el insumo no existe en la lista del total se lo agrega
                    }
                }
            }

            return listaTotalInsumos;
        }

        public static List<ReservasActivas> obtenerReservasActivasDelCliente(int idUsuario)
        {
            List<ReservasActivas> listaReservas = new List<ReservasActivas>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select r.idReserva, nombreCompleto, tipoCancha, h.horario, fecha, servicioAsador, servicioInstrumentos, estado
                                    from Usuario u join Reserva r on u.idUsuario = r.cliente
                                         join Cancha c on c.idCancha = r.cancha
	                                     join Horario h on h.idHorario = r.horario
                                    where fecha = CAST(GETDATE() AS date) and estado = 1 and cliente = @idCliente
                                    order by fecha, horario";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idCliente", idUsuario);

                comando.CommandType = CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        ReservasActivas auxiliar = new ReservasActivas();
                        auxiliar.IdReserva = int.Parse(lector["idReserva"].ToString());
                        auxiliar.NombreCompleto = lector["nombreCompleto"].ToString();
                        auxiliar.TipoCancha = lector["tipoCancha"].ToString();
                        auxiliar.Horario = lector["horario"].ToString();
                        auxiliar.Fecha = DateTime.Parse(lector["fecha"].ToString());
                        auxiliar.ServicioAsador = bool.Parse(lector["servicioAsador"].ToString());
                        auxiliar.ServicioInstrumento = bool.Parse(lector["servicioInstrumentos"].ToString());
                        auxiliar.ListaInsumosReserva = AD_Reserva.obtenerInsumosDeLaReserva(auxiliar.IdReserva);
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

        public static List<ReservasCanceladas> obtenerReservasCanceladas(DateTime fechaInicio, DateTime fechaFin)
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
                                    where fecha between CAST(@fechaInicio AS date) and CAST(@fechaFin AS date) and estado = 0
                                    order by fecha";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                comando.Parameters.AddWithValue("@fechaFin", fechaFin);

                comando.CommandType = CommandType.Text;
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

        public static List<CanchasMasReservadasVM> obtenerCanchasMasReservadas(DateTime fechaInicio, DateTime fechaFin)
        {
            List<CanchasMasReservadasVM> listaCanchasMasReservadas = new List<CanchasMasReservadasVM>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select tipoCancha, count(*) 'Veces utilizada'
                                    from Cancha c join Reserva r on r.cancha = c.idCancha
                                    where estado = 1 and fecha between CAST(@fechaInicio AS date) and CAST(@fechaFin AS date)
                                    group by tipoCancha
                                    order by 2 desc";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                comando.Parameters.AddWithValue("@fechaFin", fechaFin);

                comando.CommandType = CommandType.Text;
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

        public static List<ClientesConMasReservas> obtenerClientesConMasReservas(DateTime fechaInicio, DateTime fechaFin)
        {
            List<ClientesConMasReservas> listaClientesConMasReservas = new List<ClientesConMasReservas>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select nombreCompleto, email, telefono, count(*) 'Cant reservas'
                                    from Usuario u join Reserva r on u.idUsuario = r.cliente
                                    where estado = 1 and fecha between CAST(@fechaInicio AS date) and CAST(@fechaFin AS date)
                                    group by nombreCompleto, email, telefono
                                    order by 4 desc";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                comando.Parameters.AddWithValue("@fechaFin", fechaFin);

                comando.CommandType = CommandType.Text;
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

        public static List<HorariosMasReservados> obtenerHorariosMasReservados(DateTime fechaInicio, DateTime fechaFin)
        {
            List<HorariosMasReservados> listaHorariosMasReservados = new List<HorariosMasReservados>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select h.horario, count(*) 'Cant reservas'
                                    from Horario h join HorarioReservas hr on h.idHorario = hr.horario
	                                     join Reserva r on r.idReserva = hr.reserva
                                    where fecha between CAST(@fechaInicio AS date) and CAST(@fechaFin AS date)
                                    group by h.horario
                                    order by 1";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                comando.Parameters.AddWithValue("@fechaFin", fechaFin);

                comando.CommandType = CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        HorariosMasReservados auxiliar = new HorariosMasReservados();
                        auxiliar.Horario = lector["horario"].ToString();
                        auxiliar.Cantidad = int.Parse(lector["Cant reservas"].ToString());
                        listaHorariosMasReservados.Add(auxiliar);
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
            return listaHorariosMasReservados;
        }

        public static List<Insumo> obtenerInsumosConsumidosEntreFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            List<Insumo> listaInsumosConsumidos = new List<Insumo>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select i.insumo, sum(cantidad) 'Cantidad vendida'
                                    from Insumo i join ReservaInsumos ri on ri.insumo = i.idInsumo
	                                     join Reserva r on r.idReserva = ri.reserva
                                    where fecha between CAST(@fechaInicio AS date) and CAST(@fechaFin AS date)
                                    group by i.insumo
                                    order by 2 desc";

                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                comando.Parameters.AddWithValue("@fechaFin", fechaFin);

                comando.CommandType = CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        Insumo auxiliar = new Insumo();
                        auxiliar.insumo1 = lector["insumo"].ToString();
                        auxiliar.cantidad = int.Parse(lector["Cantidad vendida"].ToString());
                        listaInsumosConsumidos.Add(auxiliar);
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
            return listaInsumosConsumidos;
        }

        public static List<InstrumentoRotoVM> obtenerInstrumentosRotosEntreFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            List<InstrumentoRotoVM> listaInstrumentosRotos = new List<InstrumentoRotoVM>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select i.instrumento, fechaRotura
                                    from Instrumento i join InstrumentoRoto ir on ir.instrumento = i.idInstrumento
                                    where fechaRotura between CAST(@fechaInicio AS date) and CAST(@fechaFin AS date)";

                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                comando.Parameters.AddWithValue("@fechaFin", fechaFin);

                comando.CommandType = CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        InstrumentoRotoVM auxiliar = new InstrumentoRotoVM();
                        auxiliar.Instrumento = lector["instrumento"].ToString();
                        auxiliar.FechaRotura = DateTime.Parse(lector["fechaRotura"].ToString());
                        listaInstrumentosRotos.Add(auxiliar);
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
            return listaInstrumentosRotos;
        }
    }
}