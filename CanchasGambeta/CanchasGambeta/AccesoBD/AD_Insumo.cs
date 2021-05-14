using CanchasGambeta.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CanchasGambeta.AccesoBD
{
    public class AD_Insumo
    {
        public static string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
        public static List<Insumo> obtenerInsumos()
        {
            List<Insumo> lista = new List<Insumo>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select idInsumo, insumo, precio, stock from Insumo";
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
                        Insumo auxiliar = new Insumo();
                        auxiliar.idInsumo = int.Parse(lector["idInsumo"].ToString());
                        auxiliar.insumo1 = lector["insumo"].ToString();
                        auxiliar.precio = Math.Round(decimal.Parse(lector["precio"].ToString()),2);
                        auxiliar.stock = int.Parse(lector["stock"].ToString());
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

        public static bool nuevoInsumo(string insumo, decimal precio, int stock)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consultaInsertEquipo = "insert into Insumo (insumo, precio, stock) values (@insumo, @precio, @stock)";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@insumo", insumo);
                comando.Parameters.AddWithValue("@precio", precio);
                comando.Parameters.AddWithValue("@stock", stock);


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

        public static Insumo obtenerInsumoPorId(int idInsumo)
        {
            Insumo insumo = new Insumo();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = "select insumo, precio, stock from Insumo where idInsumo = @idInsumo";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idInsumo", idInsumo);

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        insumo.idInsumo = idInsumo;
                        insumo.insumo1 = lector["insumo"].ToString();
                        insumo.precio = Math.Round(decimal.Parse(lector["precio"].ToString()));
                        insumo.stock = int.Parse(lector["stock"].ToString());
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
            return insumo;
        }

        public static bool modificarInsumo(Insumo insumo)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"update Insumo set insumo = @insumo,
                                                      precio = @precio,
                                                      stock = @stock
                                    where idInsumo = @idInsumo";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@insumo", insumo.insumo1);
                comando.Parameters.AddWithValue("@precio", insumo.precio);
                comando.Parameters.AddWithValue("@stock", insumo.stock);
                comando.Parameters.AddWithValue("@idInsumo", insumo.idInsumo);

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

        public static bool eliminarInsumo(int idInsumo)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consultaEliminarInsumo = "delete from Insumo where idInsumo = @idInsumo";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idInsumo", idInsumo);

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consultaEliminarInsumo;

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
    }
}