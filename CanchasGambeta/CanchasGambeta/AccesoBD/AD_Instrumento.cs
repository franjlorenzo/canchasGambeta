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
    }
}