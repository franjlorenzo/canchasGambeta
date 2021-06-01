using CanchasGambeta.Models;
using CanchasGambeta.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace CanchasGambeta.AccesoBD
{
    public class AD_Administrador
    {
        public static string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();

        public static List<TablaPedido> obtenerTodosLosPedidos()
        {
            List<TablaPedido> lista = new List<TablaPedido>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select idPedido, descripcion, fecha, idProveedor, nombreCompleto, telefono, empresa, estado
                                    from Pedido ped join Proveedor prov on ped.proveedor = prov.idProveedor";

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        TablaPedido auxiliar = new TablaPedido();
                        auxiliar.IdPedido = int.Parse(lector["idPedido"].ToString());
                        auxiliar.DescripcionPedido = lector["descripcion"].ToString();
                        auxiliar.Fecha = DateTime.Parse(lector["fecha"].ToString());
                        auxiliar.IdProveedor = int.Parse(lector["idProveedor"].ToString());
                        auxiliar.NombreProveedor = lector["nombreCompleto"].ToString();
                        auxiliar.Telefono = lector["telefono"].ToString();
                        auxiliar.Empresa = lector["empresa"].ToString();
                        auxiliar.Estado = bool.Parse(lector["estado"].ToString());
                        auxiliar.PedidosPendientes = obtenerPedidosPendientes();
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

        public static List<Proveedor> obtenerTodosLosProveedores()
        {
            List<Proveedor> lista = new List<Proveedor>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select idProveedor, nombreCompleto, empresa, telefono, email from Proveedor";

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        Proveedor auxiliar = new Proveedor();
                        auxiliar.idProveedor = int.Parse(lector["idProveedor"].ToString());
                        auxiliar.nombreCompleto = lector["nombreCompleto"].ToString();
                        auxiliar.empresa = lector["empresa"].ToString();
                        auxiliar.telefono = lector["telefono"].ToString();
                        auxiliar.email = lector["email"].ToString();
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

        public static List<TablaProveedores> obtenerTodosLosProveedoresTabla()
        {
            List<TablaProveedores> lista = new List<TablaProveedores>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select idProveedor, nombreCompleto, empresa, telefono, email from Proveedor";

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        TablaProveedores auxiliar = new TablaProveedores();
                        auxiliar.IdProveedor = int.Parse(lector["idProveedor"].ToString());
                        auxiliar.NombreCompleto = lector["nombreCompleto"].ToString();
                        auxiliar.Empresa = lector["empresa"].ToString();
                        auxiliar.Telefono = lector["telefono"].ToString();
                        auxiliar.Email = lector["email"].ToString();
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

        public static bool nuevoPedido(NuevoPedido nuevoPedido)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"insert into Pedido (proveedor, descripcion, fecha, estado) values (@proveedor, @descripcion, @fecha, @estado)";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@proveedor", nuevoPedido.IdProveedor);
                comando.Parameters.AddWithValue("@descripcion", nuevoPedido.Descripcion);
                comando.Parameters.AddWithValue("@fecha", nuevoPedido.Fecha);
                comando.Parameters.AddWithValue("@estado", 1);

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

        public static bool concretarUnPedido(int idPedido)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"update Pedido set estado = 0 where idPedido = @idPedido";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idPedido", idPedido);

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

        public static Pedido obtenerPedidoPorId(int idPedido)
        {
            Pedido pedido = new Pedido();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select idPedido, proveedor, descripcion, fecha from Pedido where idPedido = @idPedido";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idPedido", idPedido);

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        pedido.idPedido = int.Parse(lector["idPedido"].ToString());
                        pedido.proveedor = int.Parse(lector["proveedor"].ToString());
                        pedido.descripcion = lector["descripcion"].ToString();
                        pedido.fecha = DateTime.Parse(lector["fecha"].ToString());
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
            return pedido;
        }

        public static bool modificarPedido(Pedido pedido)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"update Pedido set descripcion = @descripcion,
                                                      fecha = getdate()
                                    where idPedido = @idPedido";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@descripcion", pedido.descripcion);
                comando.Parameters.AddWithValue("@idPedido", pedido.idPedido);

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

        public static bool eliminarPedido(int idPedido)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"delete from Pedido where idPedido = @idPedido";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idPedido", idPedido);

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

        public static List<TablaPedido> obtenerPedidosPendientes()
        {
            List<TablaPedido> lista = new List<TablaPedido>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select idPedido from Pedido where estado = 1";

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        TablaPedido auxiliar = new TablaPedido();
                        auxiliar.IdPedido = int.Parse(lector["idPedido"].ToString());
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

        public static Proveedor obtenerProveedorPorId(int idProveedor)
        {
            Proveedor proveedor = new Proveedor();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select * from Proveedor where idProveedor = @idProveedor";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idProveedor", idProveedor);

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        proveedor.idProveedor = idProveedor;
                        proveedor.nombreCompleto = lector["nombreCompleto"].ToString();
                        proveedor.telefono = lector["telefono"].ToString();
                        proveedor.empresa = lector["empresa"].ToString();
                        proveedor.email = lector["email"].ToString();
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
            return proveedor;
        }

        public static bool nuevoProveedor(NuevoProveedor nuevoProveedor)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"insert into Proveedor (nombreCompleto, empresa, telefono, email) values (@nombreCompleto, @empresa, @telefono, @email)";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@nombreCompleto", nuevoProveedor.NombreCompleto);
                comando.Parameters.AddWithValue("@empresa", nuevoProveedor.Empresa);
                comando.Parameters.AddWithValue("@telefono", nuevoProveedor.Telefono);
                comando.Parameters.AddWithValue("@email", nuevoProveedor.Email);

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

        public static bool pedidosSinConcretar(int idProveedor)
        {
            bool resultado = true;
            List<Pedido> pedidosAProveedor = new List<Pedido>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select * from Pedido
                                    where proveedor = @idProveedor";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idProveedor", idProveedor);

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        Pedido auxiliar = new Pedido();
                        auxiliar.estado = bool.Parse(lector["estado"].ToString());
                        pedidosAProveedor.Add(auxiliar);
                    }
                }

                foreach (var lista in pedidosAProveedor)
                {
                    if(lista.estado == true)
                    {
                        resultado = false;
                        break;
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
            return resultado;
        }

        public static bool modificarProveedor(Proveedor proveedor)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"update Proveedor set nombreCompleto = @nombreCompleto,
                                                         telefono = @telefono,
                                                         email = @email,
                                                         empresa = @empresa
                                    where idProveedor = @idProveedor";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@nombreCompleto", proveedor.nombreCompleto);
                comando.Parameters.AddWithValue("@telefono", proveedor.telefono);
                comando.Parameters.AddWithValue("@email", proveedor.email);
                comando.Parameters.AddWithValue("@empresa", proveedor.empresa);
                comando.Parameters.AddWithValue("@idProveedor", proveedor.idProveedor);

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

        public static bool eliminarProveedor(int idProveedor)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consultaEliminarPedidos = @"delete from Pedido where proveedor = @idProveedor";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idProveedor", idProveedor);

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consultaEliminarPedidos;
                conexion.Open();
                comando.Connection = conexion;
                comando.ExecuteNonQuery();

                string consultaEliminarProveedor = @"delete from Proveedor where idProveedor = @idProveedor";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idProveedor", idProveedor);
                comando.CommandText = consultaEliminarProveedor;
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

        public static void enviarMailAProveedor(NuevoPedido nuevoPedido, int tipoMensaje, Pedido actualizarPedido, Pedido pedidoEliminado)
        {
            SmtpClient smtpClient = new SmtpClient();

            Proveedor proveedor = null;
            if (nuevoPedido != null) 
            {
                nuevoPedido.IdPedido = obtenerIdPedidoPorAtributos(nuevoPedido.Descripcion, nuevoPedido.IdProveedor, nuevoPedido.Fecha);
                proveedor = obtenerProveedorPorId(nuevoPedido.IdProveedor);
            }
            if(actualizarPedido != null) proveedor = obtenerProveedorPorId(actualizarPedido.proveedor);
            if(pedidoEliminado != null) proveedor = obtenerProveedorPorId(pedidoEliminado.proveedor);

            string mensaje = "";
            string titulo = "";

            if (tipoMensaje == 1) //Nuevo pedido
            {
                mensaje = $"Hola {proveedor.nombreCompleto}. Canchas Gambeta ha realizado un pedido nuevo:\n N° {nuevoPedido.IdPedido}\n Descripción: {nuevoPedido.Descripcion}";
                titulo = "Nuevo pedido";
            }
            else if (tipoMensaje == 2) //Update Pedido
            {
                mensaje = $"Hola {proveedor.nombreCompleto}. Canchas Gambeta le informa que se han realizado modificaciones en el pedido N°{actualizarPedido.idPedido}\n Descripción: {actualizarPedido.descripcion}";
                titulo = "Pedido modificado";
            }
            else //Delete pedido
            {
                mensaje = $"Hola {proveedor.nombreCompleto}. Canchas Gambeta le informa que el pedido N° {pedidoEliminado.idPedido} ha sido cancelado.\nDescripción: {pedidoEliminado.descripcion}.\nGracias por su atención y disculpe las molestias.";
                titulo = "Pedido eliminado";
            }

            smtpClient.Send("canchasgambeta@gmail.com", proveedor.email, titulo, mensaje);
        }

        public static int obtenerIdPedidoPorAtributos(string descripcion, int idProveedor, DateTime fecha)
        {
            int idPedido = 0;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select idPedido from Pedido 
                                    where proveedor = @idProveedor and
                                    descripcion = @descripcion and
                                    fecha = @fecha";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idProveedor", idProveedor);
                comando.Parameters.AddWithValue("@descripcion", descripcion);
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
                       idPedido = int.Parse(lector["idPedido"].ToString());

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
            return idPedido;
        }
    }
}