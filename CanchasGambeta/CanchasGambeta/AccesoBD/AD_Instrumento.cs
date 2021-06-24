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
    public class AD_Instrumento
    {
        public static string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
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

                string consultaInsertInstrumentoRoto = @"insert into InstrumentoRoto (instrumento, fechaRotura, estado) values (@idInstrumento, getdate(), 1)";
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
                string consulta = @"select ir.idInstrumentoRoto 'idInstrumento', i.instrumento, fechaRotura 
                                    from Instrumento i join InstrumentoRoto ir on i.idInstrumento = ir.instrumento
                                    order by 3 desc";

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
                        auxiliar.IdInstrumentoRoto = int.Parse(lector["idInstrumento"].ToString());
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

        public static InstrumentoRotoVM obtenerInstrumentoRotoPorId(int idInstrumentoRoto)
        {
            InstrumentoRotoVM instrumento = new InstrumentoRotoVM();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select i.idInstrumento 'idInstrumento', i.instrumento, fechaRotura
                                    from Instrumento i join InstrumentoRoto ir on i.idInstrumento = ir.instrumento
                                    where idInstrumentoRoto = @idInstrumentoRoto";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idInstrumentoRoto", idInstrumentoRoto);

                comando.CommandType = CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        instrumento.IdInstrumentoRoto = idInstrumentoRoto;
                        instrumento.Instrumento = lector["instrumento"].ToString();
                        instrumento.FechaRotura = DateTime.Parse(lector["fechaRotura"].ToString());
                        instrumento.IdInstrumentoDisponible = int.Parse(lector["idInstrumento"].ToString());
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
            return instrumento;
        }

        public static bool instrumentoRepuesto(InstrumentoRotoVM instrumento)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consultaInsertInstrumentoRepuesto = "insert into InstrumentoRepuesto (nombreInstrumento, idInstrumentoRoto, fechaRepuesto) values (@nombreInstrumento, @idInstrumentoRoto, @fechaRepuesto)";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@nombreInstrumento", instrumento.Instrumento);
                comando.Parameters.AddWithValue("@idInstrumentoRoto", instrumento.IdInstrumentoRoto);
                comando.Parameters.AddWithValue("@fechaRepuesto", instrumento.FechaRotura);

                comando.CommandType = CommandType.Text;
                comando.CommandText = consultaInsertInstrumentoRepuesto;

                conexion.Open();
                comando.Connection = conexion;
                comando.ExecuteNonQuery();

                string consultaUpdateInstrumentoDisponible = @"update Instrumento set instrumento = @instrumento,
                                                                                      fechaCompra = @fechaCompra,
                                                                                      estado = 1
                                                               where idInstrumento = @idInstrumentoDisponible";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@instrumento", instrumento.Instrumento);
                comando.Parameters.AddWithValue("@fechaCompra", instrumento.FechaRotura);
                comando.Parameters.AddWithValue("@idInstrumentoDisponible", instrumento.IdInstrumentoDisponible);

                comando.CommandText = consultaUpdateInstrumentoDisponible;
                comando.ExecuteNonQuery();

                string consultaUpdateInstrumentoRoto = @"update InstrumentoRoto set estado = 0
                                                         where idInstrumentoRoto = @idInstrumentoRoto";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idInstrumentoRoto", instrumento.IdInstrumentoRoto);

                comando.CommandText = consultaUpdateInstrumentoRoto;
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