using CanchasGambeta.Models;
using CanchasGambeta.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Web;

namespace CanchasGambeta.AccesoBD
{
    public class AD_Pedido
    {
        public static string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();

        public static List<TablaPedido> obtenerTodosLosPedidos()
        {
            List<TablaPedido> lista = new List<TablaPedido>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select idPedido, fecha, descripcion, idProveedor, nombreCompleto, telefono, empresa, estado
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
                        auxiliar.Fecha = DateTime.Parse(lector["fecha"].ToString());
                        auxiliar.DescripcionPedido = lector["descripcion"].ToString();
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
                string consultaInsertPedido = @"insert into Pedido (proveedor, fecha, descripcion, estado) values (@proveedor, @fecha, @descripcion, @estado)";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@proveedor", nuevoPedido.IdProveedor);
                comando.Parameters.AddWithValue("@fecha", nuevoPedido.Fecha);
                comando.Parameters.AddWithValue("@descripcion", nuevoPedido.Descripcion);
                comando.Parameters.AddWithValue("@estado", true);

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consultaInsertPedido;

                conexion.Open();
                comando.Connection = conexion;
                comando.ExecuteNonQuery();

                string consultaInsertDetallePedido = @"insert into DetallePedido (pedido, insumo, cantidadPedida) values (@pedido, @insumo, @cantidadPedida)";
                comando.CommandText = consultaInsertDetallePedido;

                foreach (var lista in nuevoPedido.InsumosPedido)
                {
                    comando.Parameters.Clear();
                    comando.Parameters.AddWithValue("@pedido", obtenerIdPedidoPorAtributos(nuevoPedido.IdProveedor, nuevoPedido.Fecha, nuevoPedido.Descripcion));
                    comando.Parameters.AddWithValue("@insumo", lista.IdInsumo);
                    comando.Parameters.AddWithValue("@cantidadPedida", lista.Cantidad);                   
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

        public static bool concretarUnPedido(ConcretarPedido concretarPedido)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consultaUpdateEstado = @"update Pedido set estado = 0 where idPedido = @idPedido";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idPedido", concretarPedido.IdPedido);

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consultaUpdateEstado;

                conexion.Open();
                comando.Connection = conexion;
                comando.ExecuteNonQuery();

                string consultaCompletarDetalle = @"update DetallePedido set fechaRecibido = @fechaRecibido,
                                                                             cantidadRecibida = @cantidadRecibida
                                                    where pedido = @idPedido and insumo = @idInsumo";
                comando.CommandText = consultaCompletarDetalle;

                foreach (var item in concretarPedido.ListaCantidadesRecibidas)
                {
                    comando.Parameters.Clear();
                    comando.Parameters.AddWithValue("@fechaRecibido", concretarPedido.FechaRecibido);
                    comando.Parameters.AddWithValue("@cantidadRecibida", item.Cantidad);
                    comando.Parameters.AddWithValue("@idPedido", concretarPedido.IdPedido);
                    comando.Parameters.AddWithValue("@idInsumo", item.IdInsumo);
                    comando.ExecuteNonQuery();
                }

                string consultaUpdateStock = @"update Insumo set stock = stock + @cantidadRecibida
                                               where idInsumo = @idInsumo";
                comando.CommandText = consultaUpdateStock;

                foreach (var item in concretarPedido.ListaCantidadesRecibidas)
                {
                    comando.Parameters.Clear();
                    comando.Parameters.AddWithValue("@cantidadRecibida", item.Cantidad);
                    comando.Parameters.AddWithValue("@idInsumo", item.IdInsumo);
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

        public static List<InsumosAPedir> obtenerInsumosDelPedido(int idPedido)
        {
            List<InsumosAPedir> insumosEnElPedido = new List<InsumosAPedir>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select dp.insumo 'idInsumo', i.insumo, cantidadPedida
                                    from DetallePedido dp join Insumo i on i.idInsumo = dp.insumo
                                    where pedido = @idPedido";
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
                        InsumosAPedir insumo = new InsumosAPedir();
                        insumo.IdInsumo = int.Parse(lector["idInsumo"].ToString());
                        insumo.Insumo = lector["insumo"].ToString();
                        insumo.Cantidad = int.Parse(lector["cantidadPedida"].ToString());
                        insumosEnElPedido.Add(insumo);
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
            return insumosEnElPedido;
        }

        public static bool modificarPedido(int idPedido, List<InsumosAPedir> insumosAPedir)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consultaDeleteDetalle = @"delete from DetallePedido where pedido = @idPedido";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idPedido", idPedido);

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consultaDeleteDetalle;

                conexion.Open();
                comando.Connection = conexion;
                comando.ExecuteNonQuery();

                string consultaInsertNuevosInsumos = @"insert into DetallePedido (pedido, insumo, cantidadPedida) values (@idPedido, @insumo, @cantidadPedida)";
                comando.CommandText = consultaInsertNuevosInsumos;

                foreach (var item in insumosAPedir)
                {
                    comando.Parameters.Clear();
                    comando.Parameters.AddWithValue("@idPedido", idPedido);
                    comando.Parameters.AddWithValue("@insumo", item.IdInsumo);
                    comando.Parameters.AddWithValue("@cantidadPedida", item.Cantidad);
                    comando.ExecuteNonQuery();
                }

                string consultaUpdateDescripcion = @"update Pedido set descripcion = @descripcion where idPedido = @idPedido";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@descripcion", armarDescripcionPedido(insumosAPedir));
                comando.Parameters.AddWithValue("@idPedido", idPedido);

                comando.CommandText = consultaUpdateDescripcion;
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
                string consultaDeletePedido = @"delete from Pedido where idPedido = @idPedido";
                string consultaDeleteDetallePedido = @"delete from DetallePedido where pedido = @idPedido";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idPedido", idPedido);

                comando.CommandType = System.Data.CommandType.Text;
                conexion.Open();
                comando.Connection = conexion;

                comando.CommandText = consultaDeleteDetallePedido;
                comando.ExecuteNonQuery();
                comando.CommandText = consultaDeletePedido;
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

        public static NuevoPedido obtenerPedidoPorId(int idPedido)
        {
            NuevoPedido pedido = new NuevoPedido();
            pedido.InsumosPedido = new List<InsumosAPedir>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select proveedor, descripcion, fecha from Pedido where idPedido = @idPedido";
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
                        pedido.IdPedido = idPedido;
                        pedido.Descripcion = lector["descripcion"].ToString();
                        pedido.IdProveedor = int.Parse(lector["proveedor"].ToString());
                        pedido.Fecha = DateTime.Parse(lector["fecha"].ToString());
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

        public static void enviarMailAProveedor(NuevoPedido nuevoPedido, int tipoMensaje, List<InsumosAPedir> listaInsumosActualizados, NuevoPedido pedidoSinModificar, NuevoPedido pedidoEliminado)
        {
            SmtpClient smtpClient = new SmtpClient();

            Proveedor proveedor = null;
            if (nuevoPedido != null) 
            {
                nuevoPedido.IdPedido = obtenerIdPedidoPorAtributos(nuevoPedido.IdProveedor, nuevoPedido.Fecha, nuevoPedido.Descripcion);
                proveedor = obtenerProveedorPorId(nuevoPedido.IdProveedor);
            }
            if(pedidoSinModificar != null) proveedor = obtenerProveedorPorId(pedidoSinModificar.IdProveedor);
            if(pedidoEliminado != null) proveedor = obtenerProveedorPorId(pedidoEliminado.IdProveedor);

            string mensaje = "";
            string titulo = "";

            if (tipoMensaje == 1) //Nuevo pedido
            {
                mensaje = $"Hola {proveedor.nombreCompleto}. Canchas Gambeta ha realizado un pedido nuevo:\n" +
                          $"Identificador N°{nuevoPedido.IdPedido}\n" +
                          $"Insumos pedidos:\n" +
                          $"{nuevoPedido.Descripcion}";
                titulo = "Nuevo pedido";
            }
            else if (tipoMensaje == 2) //Update Pedido
            {
                string descripcion = armarDescripcionPedido(listaInsumosActualizados);
                mensaje = $"Hola {proveedor.nombreCompleto}. Canchas Gambeta le informa que se han realizado modificaciones en el pedido N°{pedidoSinModificar.IdPedido}\n" +
                          $"El pedido antes de modificarse era:\n" +
                          $"{pedidoSinModificar.Descripcion}\n" +
                          $"El pedido actualmente es:\n" +
                          $"{descripcion}";
                titulo = "Pedido modificado";
            }
            else //Delete pedido
            {
                mensaje = $"Hola {proveedor.nombreCompleto}. Canchas Gambeta le informa que el pedido N°{pedidoEliminado.IdPedido} ha sido cancelado.\n" +
                          $"El pedido contenia los siguientes items:\n" +
                          $"{pedidoEliminado.Descripcion}" +
                          $"Gracias por su atención y disculpe las molestias.";
                titulo = "Pedido eliminado";
            }

            smtpClient.Send("canchasgambeta@gmail.com", proveedor.email, titulo, mensaje);
        }

        public static int obtenerIdPedidoPorAtributos(int idProveedor, DateTime fecha, string descripcion)
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

        public static List<InsumosAPedir> armarListaInsumosSeleccionados(List<int> listaIdInsumo, List<string> listaNombreInsumo, List<int> cantidadInsumo, [Optional] List<int> listaStockInsumo)
        {
            List<InsumosAPedir> listaInsumosAPedir = new List<InsumosAPedir>();

            for (int i = 0; i < listaIdInsumo.Count; i++)
            {
                InsumosAPedir insumo = new InsumosAPedir();
                insumo.IdInsumo = listaIdInsumo[i];
                insumo.Insumo = listaNombreInsumo[i];
                insumo.Cantidad = cantidadInsumo[i];
                if (listaStockInsumo != null) insumo.Stock = listaStockInsumo[i];
                listaInsumosAPedir.Add(insumo);
            }

            return listaInsumosAPedir;
        }

        public static string armarDescripcionPedido(List<InsumosAPedir> listaInsumosAPedir)
        {
            string descripcion = "";

            foreach (var listado in listaInsumosAPedir)
            {
                descripcion = descripcion + $"{listado.Insumo} - Cantidad: {listado.Cantidad}\n";
            }

            return descripcion;
        }

        public static ConcretarPedido obtenerDatosPedidoAConcretar(int idPedido)
        {
            ConcretarPedido pedidoAConcretar = new ConcretarPedido();
            pedidoAConcretar.FechaRecibido = DateTime.Now;
            pedidoAConcretar.ListaCantidadesRecibidas = new List<InsumosAPedir>();
            pedidoAConcretar.ListaInsumosPedidos = new List<InsumosAPedir>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consultaObtenerDatosPedido = @"select idPedido, proveedor, nombreCompleto, fecha
                                                      from Pedido pe join Proveedor pr on pr.idProveedor = pe.proveedor
                                                      where idPedido = @idPedido";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idPedido", idPedido);

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consultaObtenerDatosPedido;

                conexion.Open();
                comando.Connection = conexion;

                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        pedidoAConcretar.IdPedido = idPedido;
                        pedidoAConcretar.IdProveedor = int.Parse(lector["proveedor"].ToString());
                        pedidoAConcretar.NombreProveedor = lector["nombreCompleto"].ToString();
                        pedidoAConcretar.FechaRealizado = DateTime.Parse(lector["fecha"].ToString());
                    }
                }
                lector.Close();

                string consultaObtenerInsumosDelPedido = @"select dp.insumo 'idInsumo', i.insumo, cantidadPedida from DetallePedido dp join Insumo i on i.idInsumo = dp.insumo
                                                           where pedido = @idPedido";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idPedido", idPedido);

                comando.CommandText = consultaObtenerInsumosDelPedido;
                SqlDataReader lector2 = comando.ExecuteReader();
                if (lector2 != null)
                {
                    while (lector2.Read())
                    {
                        InsumosAPedir insumoPedido = new InsumosAPedir();
                        insumoPedido.IdInsumo = int.Parse(lector2["idInsumo"].ToString());
                        insumoPedido.Insumo = lector2["insumo"].ToString();
                        insumoPedido.Cantidad = int.Parse(lector2["cantidadPedida"].ToString());
                        pedidoAConcretar.ListaInsumosPedidos.Add(insumoPedido);
                    }
                }
                lector2.Close();

                string consultaObtenerInsumosAConcretar = @"select dp.insumo 'idInsumo', i.insumo from DetallePedido dp join Insumo i on i.idInsumo = dp.insumo
                                                            where pedido = @idPedido";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idPedido", idPedido);
                
                comando.CommandText = consultaObtenerInsumosAConcretar;
                SqlDataReader lector3 = comando.ExecuteReader();
                if (lector3 != null)
                {
                    while (lector3.Read())
                    {
                        InsumosAPedir insumoAConcretar = new InsumosAPedir();
                        insumoAConcretar.IdInsumo = int.Parse(lector3["idInsumo"].ToString());
                        insumoAConcretar.Insumo = lector3["insumo"].ToString();
                        insumoAConcretar.Cantidad = 0;
                        pedidoAConcretar.ListaCantidadesRecibidas.Add(insumoAConcretar);
                    }
                }
                lector3.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
            return pedidoAConcretar;
        }

        public static DetallePedidoConcretado obtenerDetallePedidoPorId(int idPedido)
        {
            DetallePedidoConcretado detallePedido = new DetallePedidoConcretado();
            detallePedido.ListaInsumosPedidosrecibidos = new List<InsumosPedidosRecibidos>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlCommand comando = new SqlCommand();

            try
            {
                string consulta = @"select distinct nombreCompleto, fecha, fechaRecibido
                                    from Proveedor pr join Pedido pe on pr.idProveedor = pe.proveedor
                                         join DetallePedido dp on pe.idPedido = dp.pedido
                                    where idPedido = @idPedido";
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
                        detallePedido.IdPedido = idPedido;
                        detallePedido.NombreProveedor = lector["nombreCompleto"].ToString();
                        detallePedido.FechaRealizado = DateTime.Parse(lector["fecha"].ToString());
                        detallePedido.FechaRecibido = DateTime.Parse(lector["fechaRecibido"].ToString());
                    }
                }
                lector.Close();

                string consultaObtenerInsumosDetalle = @"select i.insumo, cantidadPedida, cantidadRecibida
                                                         from DetallePedido dp join Insumo i on dp.insumo = i.idInsumo
                                                         where pedido = @idPedido";
                comando.CommandText = consultaObtenerInsumosDetalle;

                SqlDataReader lector2 = comando.ExecuteReader();
                if (lector2 != null)
                {
                    while (lector2.Read())
                    {
                        InsumosPedidosRecibidos insumo = new InsumosPedidosRecibidos();
                        insumo.Insumo = lector2["insumo"].ToString();
                        insumo.CantidadPedida = int.Parse(lector2["cantidadPedida"].ToString());
                        insumo.CantidadRecibida = int.Parse(lector2["cantidadRecibida"].ToString());
                        detallePedido.ListaInsumosPedidosrecibidos.Add(insumo);
                    }
                }
                lector2.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
            return detallePedido;
        }
    }
}