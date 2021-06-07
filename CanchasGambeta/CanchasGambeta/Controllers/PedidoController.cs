using CanchasGambeta.Models;
using CanchasGambeta.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace CanchasGambeta.Controllers
{
    public class PedidoController : Controller
    {
        // GET: Pedido
        public ActionResult MisPedidos()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            List<Proveedor> proveedores = AccesoBD.AD_Pedido.obtenerTodosLosProveedores();
            List<SelectListItem> listaProveedores = proveedores.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.nombreCompleto + " - (" + d.empresa + ")",
                    Value = d.idProveedor.ToString(),
                    Selected = false
                };
            });

            NuevoPedido nuevoPedido = new NuevoPedido();
            List<InsumosAPedir> listaInsumosAPedir = new List<InsumosAPedir>();
            ViewBag.proveedores = listaProveedores;
            if (TempData["listaInsumosAPedir"] != null)
            {
                listaInsumosAPedir = (List<InsumosAPedir>)TempData["listaInsumosAPedir"];
                nuevoPedido.Descripcion = AccesoBD.AD_Pedido.armarDescripcionPedido(listaInsumosAPedir);
            }
            if (TempData["ErrorInsertPedido"] != null) ViewBag.ErrorInsertPedido = TempData["ErrorInsertPedido"].ToString();
            if (TempData["ErrorConcretarPedido"] != null) ViewBag.ErrorConcretarPedido = TempData["ErrorConcretarPedido"].ToString();
            if (TempData["ErrorEliminarPedido"] != null) ViewBag.ErrorEliminarPedido = TempData["ErrorEliminarPedido"].ToString();
            if (TempData["ErrorInsertProveedor"] != null) ViewBag.ErrorInsertProveedor = TempData["ErrorInsertProveedor"].ToString();
            if (TempData["ErrorEliminarProveedor"] != null) ViewBag.ErrorEliminarProveedor = TempData["ErrorEliminarProveedor"].ToString();
            if (TempData["ErrorPedidosSinConcretar"] != null) ViewBag.ErrorPedidosSinConcretar = TempData["ErrorPedidosSinConcretar"].ToString();
            return View(new VistaMisPedidos { TablaPedido = AccesoBD.AD_Pedido.obtenerTodosLosPedidos(), NuevoPedido = nuevoPedido, NuevoProveedor = new NuevoProveedor(), TablaProveedores = AccesoBD.AD_Pedido.obtenerTodosLosProveedoresTabla(), InsumosAPedir = listaInsumosAPedir });
        }

        [HttpPost]
        public ActionResult MisPedidos(NuevoPedido nuevoPedido, string idProveedor, List<int> listaIdInsumoAlPedido, List<string> listaNombreInsumoAlPedido, List<int> listaCantidadInsumoAlPedido)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            nuevoPedido.IdProveedor = int.Parse(idProveedor);
            nuevoPedido.InsumosPedido = AccesoBD.AD_Pedido.armarListaInsumosSeleccionados(listaIdInsumoAlPedido, listaNombreInsumoAlPedido, listaCantidadInsumoAlPedido);

            if (ModelState.IsValid)
            {
                bool resultado = AccesoBD.AD_Pedido.nuevoPedido(nuevoPedido);
                if (resultado)
                {
                    //AccesoBD.AD_Administrador.enviarMailAProveedor(nuevoPedido, 1, null, null);
                    return RedirectToAction("MisPedidos", "Pedido");
                }
                else
                {
                    TempData["ErrorInsertPedido"] = "Ocurrió un error al registrar el pedido, inténtelo nuevamente.";
                    return RedirectToAction("MisPedidos", "Pedido");
                }
            }

            return View();
        }

        public ActionResult AgregarInsumosAlPedido()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            VistaPedirInsumos vistaPedirInsumos = new VistaPedirInsumos();
            vistaPedirInsumos.BuscarInsumos = new List<BuscarInsumos>();
            vistaPedirInsumos.InsumosAPedir = new List<InsumosAPedir>();
            if (TempData["buscarInsumos"] != null) vistaPedirInsumos.BuscarInsumos = (List<BuscarInsumos>)TempData["buscarInsumos"];
            if (TempData["listaInsumosAPedir"] != null) vistaPedirInsumos.InsumosAPedir = (List<InsumosAPedir>)TempData["listaInsumosAPedir"];
            if (TempData["ErrorBuscarInsumo"] != null) ViewBag.ErrorBuscarInsumo = TempData["ErrorBuscarInsumo"].ToString();
            if (TempData["nombreInsumo"] != null) ViewBag.nombreInsumo = TempData["nombreInsumo"].ToString();
            if (TempData["nombreInsumoAnterior"] != null) ViewBag.nombreInsumo = TempData["nombreInsumoAnterior"].ToString();
            if (TempData["cantidadIgualCero"] != null) ViewBag.cantidadIgualCero = TempData["cantidadIgualCero"].ToString();

            return View(vistaPedirInsumos);
        }

        [HttpPost]
        public ActionResult BuscarInsumo(string buscarInsumo, List<int> listaIdInsumoAlPedido, List<string> listaNombreInsumoAlPedido, List<int> listaCantidadInsumoAlPedido)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            if (buscarInsumo == "" || buscarInsumo == " ") buscarInsumo = null;
            List<InsumosAPedir> listaInsumosAPedir = new List<InsumosAPedir>();
            if (listaCantidadInsumoAlPedido != null && listaIdInsumoAlPedido != null && listaNombreInsumoAlPedido != null) //Verifico si ya existe una lista en la vista y si es así, la armo con los datos existentes
            {
                listaInsumosAPedir = AccesoBD.AD_Pedido.armarListaInsumosSeleccionados(listaIdInsumoAlPedido, listaNombreInsumoAlPedido, listaCantidadInsumoAlPedido);
            }

            if (buscarInsumo != null)
            {
                TempData["nombreInsumo"] = buscarInsumo;
                TempData["buscarInsumos"] = AccesoBD.AD_Insumo.obtenerInsumosPorNombre(buscarInsumo);
                TempData["listaInsumosAPedir"] = listaInsumosAPedir;
                return RedirectToAction("AgregarInsumosAlPedido");
            }
            if (Request["nombreInsumoAnterior"] == "")
            {
                TempData["ErrorBuscarInsumo"] = "Debe proporcionar una letra o palabra para buscar un insumo";
                return RedirectToAction("AgregarInsumosAlPedido");
            }
            else
            {
                TempData["nombreInsumoAnterior"] = Request["nombreInsumoAnterior"];
                TempData["ErrorBuscarInsumo"] = "Debe proporcionar una letra o palabra para buscar un insumo";
                TempData["buscarInsumos"] = AccesoBD.AD_Insumo.obtenerInsumosPorNombre(Request["nombreInsumoAnterior"]);
                return RedirectToAction("AgregarInsumosAlPedido");
            }
        }

        [HttpPost]
        public ActionResult AgregarInsumos(List<int> IdInsumoAlPedido, List<string> NombreInsumoAlPedido, List<int> CantidadInsumoAlPedido)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            List<InsumosAPedir> listaInsumosAPedir = AccesoBD.AD_Pedido.armarListaInsumosSeleccionados(IdInsumoAlPedido, NombreInsumoAlPedido, CantidadInsumoAlPedido);

            foreach (var item in listaInsumosAPedir)
            {
                if(item.Cantidad == 0)
                {
                    TempData["cantidadIgualCero"] = "No se puede pedir un insumo con cantidad cero(0)";
                    TempData["listaInsumosAPedir"] = listaInsumosAPedir;
                    return RedirectToAction("AgregarInsumosAlPedido");
                }
            }

            TempData["listaInsumosAPedir"] = listaInsumosAPedir;
            return RedirectToAction("MisPedidos");
        }

        [HttpPost]
        public ActionResult QuitarInsumo(int IdInsumoAlPedido, string NombreInsumoAlPedido, List<int> listaIdInsumoAlPedido, List<string> listaNombreInsumoAlPedido, List<int> listaCantidadInsumoAlPedido)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            InsumosAPedir insumoEnLista = new InsumosAPedir(IdInsumoAlPedido, NombreInsumoAlPedido, 0);
            List<InsumosAPedir> listaInsumosAPedir = AccesoBD.AD_Pedido.armarListaInsumosSeleccionados(listaIdInsumoAlPedido, listaNombreInsumoAlPedido, listaCantidadInsumoAlPedido);

            if (listaInsumosAPedir.Exists(insumo => insumo.IdInsumo == insumoEnLista.IdInsumo))
            {
                var insumoAEliminar = listaInsumosAPedir.Single(insumo => insumo.IdInsumo == insumoEnLista.IdInsumo);
                listaInsumosAPedir.Remove(insumoAEliminar);
            }

            TempData["listaInsumosAPedir"] = listaInsumosAPedir;
            return RedirectToAction("AgregarInsumosAlPedido");

        }
        
        [HttpPost]
        public ActionResult AgregarInsumosAlPedido(int idInsumo, string nombreInsumo, List<int> listaIdInsumoAlPedido, List<string> listaNombreInsumoAlPedido, List<int> listaCantidadInsumoAlPedido)
        {
            
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            InsumosAPedir nuevoInsumo = new InsumosAPedir(idInsumo, nombreInsumo, 0); //creo un nuevo insumo a agregar a la lista de InsumosAPedir
            List<InsumosAPedir> listaInsumosAPedir = new List<InsumosAPedir>(); //creo una lista de los insumos a pedir
            if (listaCantidadInsumoAlPedido != null && listaIdInsumoAlPedido != null && listaNombreInsumoAlPedido != null) //Verifico si ya existe una lista en la vista y si es así, la armo con los datos existentes
            {
                listaInsumosAPedir = AccesoBD.AD_Pedido.armarListaInsumosSeleccionados(listaIdInsumoAlPedido, listaNombreInsumoAlPedido, listaCantidadInsumoAlPedido);
            }

            bool existeInsumoEnLista = false;
            if (listaInsumosAPedir.Count != 0)
            {
                //Recorro la lista de insumos y verifico si el nuevo insumo ya existe en la misma
                foreach (var lista in listaInsumosAPedir) if (lista.IdInsumo == nuevoInsumo.IdInsumo) existeInsumoEnLista = true;

                if (existeInsumoEnLista) //Si el insumo existe en la lista, no lo agrego a la misma y muestro un mensaje
                {
                    ViewBag.insumoYaExiste = "Este insumo ya está en la lista para pedir";
                    ViewBag.nombreInsumo = Request["buscarInsumoAnterior"];
                    return View(new VistaPedirInsumos { BuscarInsumos = AccesoBD.AD_Insumo.obtenerInsumosPorNombre(Request["buscarInsumoAnterior"]), InsumosAPedir = listaInsumosAPedir });
                }
            }

            //Si el insumo no existe en la lista o la lista está vacia se agrega el insumo a la lista
            listaInsumosAPedir.Add(nuevoInsumo);
            ViewBag.nombreInsumo = Request["buscarInsumoAnterior"];
            return View(new VistaPedirInsumos { BuscarInsumos = AccesoBD.AD_Insumo.obtenerInsumosPorNombre(Request["buscarInsumoAnterior"]), InsumosAPedir = listaInsumosAPedir });
        }

        public ActionResult ConcretarPedido(int idPedido)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            ConcretarPedido concretarPedido = AccesoBD.AD_Pedido.obtenerDatosPedidoAConcretar(idPedido);
            return View(concretarPedido);
        }

        [HttpPost]
        public ActionResult ConcretarPedido(ConcretarPedido concretarPedido, List<int> idInsumo, List<string> nombreInsumo, List<int> cantidadRecibida)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            concretarPedido.ListaCantidadesRecibidas = AccesoBD.AD_Pedido.armarListaInsumosSeleccionados(idInsumo, nombreInsumo, cantidadRecibida);

            if (ModelState.IsValid)
            {
                bool resultado = AccesoBD.AD_Pedido.concretarUnPedido(concretarPedido);
                if (resultado) return RedirectToAction("MisPedidos", "Pedido");
                else
                {
                    TempData["ErrorConcretarPedido"] = "Ocurrió un error al concretrar el pedido, inténtelo nuevamente.";
                    return RedirectToAction("MisPedidos", "Pedido");
                }
            }

            return View();
        }

        public ActionResult ModificarPedido(int idPedido)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            List<Proveedor> proveedores = AccesoBD.AD_Pedido.obtenerTodosLosProveedores();
            List<SelectListItem> listaProveedores = proveedores.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.nombreCompleto + " - (" + d.empresa + ")",
                    Value = d.idProveedor.ToString(),
                    Selected = false
                };
            });

            ViewBag.proveedores = listaProveedores;
            Pedido pedido = AccesoBD.AD_Pedido.obtenerPedidoPorId(idPedido);

            foreach (var item in listaProveedores)
            {
                if (item.Value.Equals(pedido.proveedor.ToString()))
                {
                    item.Selected = true;
                    break;
                }
            }
            return View(pedido);
        }

        [HttpPost]
        public ActionResult ModificarPedido(Pedido pedidoModificado)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            if (ModelState.IsValid)
            {
                bool resultado = AccesoBD.AD_Pedido.modificarPedido(pedidoModificado);
                if (resultado)
                {
                    //AccesoBD.AD_Administrador.enviarMailAProveedor(null, 2, pedidoModificado, null);
                    return RedirectToAction("MisPedidos", "Pedido");
                }
                else
                {
                    ViewBag.ErrorModificarPedido = "Error al modificar el pedido, inténtelo nuevamente.";
                    List<Proveedor> proveedores = AccesoBD.AD_Pedido.obtenerTodosLosProveedores();
                    List<SelectListItem> listaProveedores = proveedores.ConvertAll(d =>
                    {
                        return new SelectListItem()
                        {
                            Text = d.nombreCompleto + " - (" + d.empresa + ")",
                            Value = d.idProveedor.ToString(),
                            Selected = false
                        };
                    });

                    ViewBag.proveedores = listaProveedores;
                    Pedido pedido = AccesoBD.AD_Pedido.obtenerPedidoPorId(pedidoModificado.idPedido);

                    foreach (var item in listaProveedores)
                    {
                        if (item.Value.Equals(pedido.proveedor.ToString()))
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                    return View(pedido);
                }
            }

            return View(pedidoModificado);
        }

        [HttpPost]
        public ActionResult EliminarPedido()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            int idPedido = int.Parse(Request["listado.IdPedido"]);
            Pedido datosPedido = AccesoBD.AD_Pedido.obtenerPedidoPorId(idPedido);

            bool resultado = AccesoBD.AD_Pedido.eliminarPedido(idPedido);
            if (resultado)
            {
                //AccesoBD.AD_Administrador.enviarMailAProveedor(null, 3, null, datosPedido);
                return RedirectToAction("MisPedidos", "Pedido");
            }
            else
            {
                TempData["ErrorEliminarPedido"] = "Ocurrió un error al eliminar el pedido, inténtelo nuevamente.";
                return RedirectToAction("MisPedidos", "Pedido");
            }
        }

        [HttpPost]
        public ActionResult NuevoProveedor(NuevoProveedor nuevoProveedor)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            if (ModelState.IsValid)
            {
                bool resultado = AccesoBD.AD_Pedido.nuevoProveedor(nuevoProveedor);
                if (resultado) return RedirectToAction("MisPedidos", "Pedido");
                else
                {
                    TempData["ErrorInsertProveedor"] = "Ocurrió un erro al registrar el proveedor, inténtelo nuevamente";
                    return RedirectToAction("MisPedidos", "Pedido");
                }
            }

            return View();
        }

        public ActionResult ModificarProveedor(int idProveedor)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            Proveedor proveedor = AccesoBD.AD_Pedido.obtenerProveedorPorId(idProveedor);
            return View(proveedor);
        }

        [HttpPost]
        public ActionResult ModificarProveedor(Proveedor proveedor)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            if (ModelState.IsValid)
            {
                bool resultado = AccesoBD.AD_Pedido.modificarProveedor(proveedor);
                if (resultado) return RedirectToAction("MisPedidos", "Pedido");
                else
                {
                    ViewBag.ErrorModificarProveedor = "Ocurrió un error al modificar al proveedor, inténtelo nuevamente.";
                    return View(proveedor);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult EliminarProveedor()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            int idProveedor = int.Parse(Request["idProveedor"]);

            if (AccesoBD.AD_Pedido.pedidosSinConcretar(idProveedor))
            {
                bool resultado = AccesoBD.AD_Pedido.eliminarProveedor(idProveedor);
                if (resultado) return RedirectToAction("MisPedidos", "Pedido");
                else
                {
                    TempData["ErrorEliminarProveedor"] = "Ocurrió un error al eliminar el proveedor, inténtelo nuevamente.";
                    return RedirectToAction("MisPedidos", "Pedido");
                }
            }
            else
            {
                TempData["ErrorPedidosSinConcretar"] = "Todavía hay pedidos sin concretar al proveedor que quiere eliminar, concretelos para poder eliminar";
                return RedirectToAction("MisPedidos", "Pedido");
            }
        }

        public ActionResult VerDetallePedido(int idPedido)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            DetallePedidoConcretado detallePedido = AccesoBD.AD_Pedido.obtenerDetallePedidoPorId(idPedido);
            return View(detallePedido);
        }
    }
}