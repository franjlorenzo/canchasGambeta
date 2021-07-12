using CanchasGambeta.Models;
using CanchasGambeta.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CanchasGambeta.AccesoBD
{
    public class AD_Insumo
    {
        public static string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();

        public static List<Insumo> ObtenerTop10InsumosMenosStock()
        {
            List<Insumo> lista = new List<Insumo>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select top 10 idInsumo, insumo, precio, stock
                                    from Insumo
                                    where estado = 1
                                    order by 4 asc";
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
                        auxiliar.precio = Math.Round(decimal.Parse(lector["precio"].ToString()), 2);
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

        public static bool NuevoInsumo(Insumo nuevo)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consultaInsertEquipo = "insert into Insumo (insumo, precio, stock, estado) values (@insumo, @precio, @stock, @estado)";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@insumo", nuevo.insumo1);
                comando.Parameters.AddWithValue("@precio", nuevo.precio);
                comando.Parameters.AddWithValue("@stock", nuevo.stock);
                comando.Parameters.AddWithValue("@estado", 1);

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

        public static Insumo ObtenerInsumoPorId(int idInsumo)
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

        public static bool ModificarInsumo(Insumo insumo)
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

        public static bool EliminarInsumo(int idInsumo)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consultaEliminarInsumo = @"update Insumo set estado = 0
                                                  where idInsumo = @idInsumo";
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

        public static List<BuscarInsumos> ObtenerInsumosPorNombre(string nombreInsumo)
        {
            List<BuscarInsumos> listaInsumosEncontrados = new List<BuscarInsumos>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                comando.CommandType = System.Data.CommandType.Text;

                if (nombreInsumo == "MostrarTodosLosInsumos")
                {
                    string consultaObtenerTodosLosInsumos = @"select * from Insumo where estado = 1 order by insumo";
                    comando.CommandText = consultaObtenerTodosLosInsumos;
                }
                else
                {
                    string consultaObtenerInsumosPorNombre = @"select idInsumo, insumo, stock, precio
                                                               from Insumo
                                                               where insumo like @nombreInsumo and estado = 1
                                                               order by 2";
                    comando.Parameters.Clear();
                    comando.Parameters.AddWithValue("@nombreInsumo", "%" + nombreInsumo + "%");
                    comando.CommandText = consultaObtenerInsumosPorNombre;
                }

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        BuscarInsumos insumo = new BuscarInsumos();
                        insumo.IdInsumo = int.Parse(lector["idInsumo"].ToString());
                        insumo.Insumo = lector["insumo"].ToString();
                        insumo.Stock = int.Parse(lector["stock"].ToString());
                        insumo.Precio = Math.Round(decimal.Parse(lector["precio"].ToString()), 2);
                        listaInsumosEncontrados.Add(insumo);
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
            return listaInsumosEncontrados;
        }
    }
}