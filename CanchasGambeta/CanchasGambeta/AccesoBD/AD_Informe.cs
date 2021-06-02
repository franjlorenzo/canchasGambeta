using CanchasGambeta.Models;
using CanchasGambeta.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
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
                string consulta = @"select r.idReserva, nombreCompleto, tipoCancha, h.horario, fecha, servicioAsador, servicioInstrumentos, estado
                                    from Usuario u join Reserva r on u.idUsuario = r.cliente
                                         join Cancha c on c.idCancha = r.cancha
	                                     join Horario h on h.idHorario = r.horario
                                    where fecha >= GETDATE()-1 and estado = 1
                                    order by fecha";

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

        public static List<CanchasMasReservadasVM> obtenerCanchasMasReservadas()
        {
            List<CanchasMasReservadasVM> listaCanchasMasReservadas = new List<CanchasMasReservadasVM>();
            /*DataTable datos = new DataTable();
            datos.Columns.Add(new DataColumn("Canchas", typeof(string)));
            datos.Columns.Add(new DataColumn("Cantidad de reservas", typeof(string)));
            string strDatos = "";*/
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select tipoCancha, count(*) 'Veces utilizada'
                                    from Cancha c join Reserva r on r.cancha = c.idCancha
                                    where estado = 1
                                    group by tipoCancha
                                    order by 2 desc";

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
                        //datos.Rows.Add(lector["tipoCancha"].ToString(), int.Parse(lector["Veces utilizada"].ToString()));
                    }

                    /*strDatos = "[['Canchas', 'Cantidad de reservas'],";

                    int cantidadRows = 1;
                    foreach (DataRow dr in datos.Rows)
                    {
                        if(datos.Rows.Count != cantidadRows)
                        {
                            cantidadRows++;
                            strDatos = strDatos + "[";
                            strDatos = strDatos + "'" + dr[0] + "'," + dr[1];
                            strDatos = strDatos + "],";
                        }
                        else
                        {
                            strDatos = strDatos + "[";
                            strDatos = strDatos + "'" + dr[0] + "'," + dr[1];
                            strDatos = strDatos + "]]";
                        }*                       
                    }*/                    
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
                                    group by nombreCompleto, email, telefono
                                    order by 4 desc";

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

        public static List<HorariosMasReservados> obtenerHorariosMasReservados()
        {
            List<HorariosMasReservados> listaHorariosMasReservados = new List<HorariosMasReservados>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select h.horario, count(*) 'Cant reservas'
                                    from Horario h join HorarioReservas hr on h.idHorario = hr.horario
                                    group by h.horario
                                    order by 2 desc";

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

        public static List<InstrumentoDisponible> obtenerInstrumentosDisponibles()
        {
            List<InstrumentoDisponible> listaInstrumentosDisponibles = new List<InstrumentoDisponible>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select idInstrumento, instrumento, fechaCompra 
                                    from Instrumento
                                    where estado = 1";

                comando.CommandType = CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        InstrumentoDisponible auxiliar = new InstrumentoDisponible();
                        auxiliar.IdInstrumento = int.Parse(lector["idInstrumento"].ToString());
                        auxiliar.Instrumento = lector["instrumento"].ToString();
                        auxiliar.FechaCompra = DateTime.Parse(lector["fechaCompra"].ToString());
                        listaInstrumentosDisponibles.Add(auxiliar);
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
            return listaInstrumentosDisponibles;
        }

        public static bool nuevoInstrumento(InstrumentoDisponible nuevoInstrumento)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consultaInsertInstrumento = "insert into Instrumento (instrumento, fechaCompra, estado) values (@instrumento, @fechaCompra, @estado)";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@instrumento", nuevoInstrumento.Instrumento);
                comando.Parameters.AddWithValue("@fechaCompra", nuevoInstrumento.FechaCompra);
                comando.Parameters.AddWithValue("@estado", 1);

                comando.CommandType = CommandType.Text;
                comando.CommandText = consultaInsertInstrumento;

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

        public static bool nuevoElementoRoto(int idInstrumento)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consultaUpdateInstrumento = @"update Instrumento set estado = 0
                                                     where idInstrumento = @idInstrumento";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idInstrumento", idInstrumento);

                comando.CommandType = CommandType.Text;
                comando.CommandText = consultaUpdateInstrumento;

                conexion.Open();
                comando.Connection = conexion;
                comando.ExecuteNonQuery();

                string consultaInsertInstrumentoRoto = @"insert into InstrumentoRoto (instrumento, fechaRotura) values (@idInstrumento, getdate())";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idInstrumento", idInstrumento);

                comando.CommandText = consultaInsertInstrumentoRoto;
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

        public static List<InstrumentoRotoVM> obtenerInstrumentosRotos()
        {
            List<InstrumentoRotoVM> listaInstrumentosRotos = new List<InstrumentoRotoVM>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select ir.instrumento 'idInstrumento', i.instrumento, fechaRotura 
                                    from Instrumento i join InstrumentoRoto ir on i.idInstrumento = ir.instrumento";

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
                        auxiliar.IdInstrumento = int.Parse(lector["idInstrumento"].ToString());
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

        public static bool eliminarInstrumentoRoto(int idInstrumento)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consultaEliminarInstrumentoRoto = @"delete from InstrumentoRoto where instrumento = @idInstrumento";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idInstrumento", idInstrumento);

                comando.CommandType = CommandType.Text;
                comando.CommandText = consultaEliminarInstrumentoRoto;

                conexion.Open();
                comando.Connection = conexion;
                comando.ExecuteNonQuery();

                string consultaEliminarInstrumento = @"delete from Instrumento where idInstrumento = @idInstrumento";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idInstrumento", idInstrumento);

                comando.CommandText = consultaEliminarInstrumento;
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
                                    where fecha between @fechaInicio and @fechaFin
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
    }
}