using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CanchasGambeta.Models;
using CanchasGambeta.ViewModels;

namespace CanchasGambeta.Controllers
{
    public class AdministradorController : Controller
    {
        // GET: Administrador
        public ActionResult IndexAdministrador()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            return View();
        }

        public ActionResult PerfilAdministrador()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            return View();
        }

        public ActionResult ModificarPerfilAdministrador()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            Usuario usuario = AccesoBD.AD_Usuario.obtenerUsuario(sesion.idUsuario);
            return View(usuario);
        }

        [HttpPost]
        public ActionResult ModificarPerfilAdministrador(Usuario usuarioModificado)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            if (ModelState.IsValid)
            {
                if (usuarioModificado.email != sesion.email)
                {
                    if (!AccesoBD.AD_Usuario.existeEmailUsuario(usuarioModificado.email))
                    {
                        bool resultado = AccesoBD.AD_Usuario.actualizarUsuario(usuarioModificado);
                        if (resultado)
                        {
                            var oUser = AccesoBD.AD_Usuario.obtenerUsuario(sesion.idUsuario);
                            Session["User"] = oUser;

                            return RedirectToAction("PerfilAdministrador", "Administrador");
                        }
                        else
                        {
                            ViewBag.ErrorActualizarUsuario = "Ocurrió un error al actualizar su perfil, inténtelo nuevamente.";
                            Usuario usuario = AccesoBD.AD_Usuario.obtenerUsuario(sesion.idUsuario);
                            return View(usuario);
                        }
                    }
                    else
                    {
                        ViewBag.EmailYaRegistrado = "El correo electrónico al que desea cambiarse ya está registrado.";
                        Usuario usuario = AccesoBD.AD_Usuario.obtenerUsuario(sesion.idUsuario);
                        return View(usuario);
                    }
                }
                else
                {
                    bool resultado = AccesoBD.AD_Usuario.actualizarUsuario(usuarioModificado);
                    if (resultado)
                    {
                        var oUser = AccesoBD.AD_Usuario.obtenerUsuario(sesion.idUsuario);
                        Session["User"] = oUser;

                        return RedirectToAction("PerfilAdministrador", "Administrador");
                    }
                    else
                    {
                        ViewBag.ErrorActualizarUsuario = "Ocurrió un error al actualizar su perfil, inténtelo nuevamente.";
                        Usuario usuario = AccesoBD.AD_Usuario.obtenerUsuario(sesion.idUsuario);
                        return View(usuario);
                    }
                }
                
            }
            return View(usuarioModificado);
        }

        public ActionResult MisPedidos()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            List<Proveedor> proveedores = AccesoBD.AD_Administrador.obtenerTodosLosProveedores();
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
            if (TempData["ErrorInsertPedido"] != null) ViewBag.ErrorInsertPedido = TempData["ErrorInsertPedido"].ToString();
            if (TempData["ErrorConcretarPedido"] != null) ViewBag.ErrorConcretarPedido = TempData["ErrorConcretarPedido"].ToString();
            if (TempData["ErrorEliminarPedido"] != null) ViewBag.ErrorEliminarPedido = TempData["ErrorEliminarPedido"].ToString();
            if (TempData["ErrorInsertProveedor"] != null) ViewBag.ErrorInsertProveedor = TempData["ErrorInsertProveedor"].ToString();
            if (TempData["ErrorEliminarProveedor"] != null) ViewBag.ErrorEliminarProveedor = TempData["ErrorEliminarProveedor"].ToString();
            if (TempData["ErrorPedidosSinConcretar"] != null) ViewBag.ErrorPedidosSinConcretar = TempData["ErrorPedidosSinConcretar"].ToString();
            return View( new VistaMisPedidos { TablaPedido = AccesoBD.AD_Administrador.obtenerTodosLosPedidos(), NuevoPedido = new NuevoPedido(), NuevoProveedor = new NuevoProveedor(), TablaProveedores = AccesoBD.AD_Administrador.obtenerTodosLosProveedoresTabla() });
        }

        [HttpPost]
        public ActionResult MisPedidos(NuevoPedido nuevoPedido, string nombreInsumo, string idProveedor)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            return View();
        }

        public ActionResult AgregarInsumosAlPedido()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            List<BuscarInsumos> buscarInsumos = new List<BuscarInsumos>();
            return View(new VistaPedirInsumos { BuscarInsumos = new List<BuscarInsumos>(), InsumosAPedir = new List<InsumosAPedir>() });
        }

        [HttpPost]
        public ActionResult AgregarInsumosAlPedido(string nombreInsumo, List<string> listaNombreInsumo, List<int> cantidadInsumo)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            //El usuario no proporciona una palabra para buscar, se devuelve error
            if(nombreInsumo == "" && listaNombreInsumo == null && cantidadInsumo == null)
            {
                ViewBag.ErrorBuscarInsumo = "Debe proporcionar una letra o palabra para buscar un insumo";
                List<BuscarInsumos> buscarInsumos = new List<BuscarInsumos>();
                return View(buscarInsumos);
            }
            //El usuario proporciona una palabra para buscar y la tabla está vacia o el usuario proporciona una palabra para buscar y la tabla contiene elementos
            if ((nombreInsumo != "" && listaNombreInsumo == null && cantidadInsumo == null) || (nombreInsumo != "" && listaNombreInsumo != null && cantidadInsumo != null))
            {
                List<BuscarInsumos> listaInsumoEncontrado = AccesoBD.AD_Insumo.obtenerInsumosPorNombre(nombreInsumo);
                return View(listaInsumoEncontrado);
            }
            //
            if(nombreInsumo == "" && listaNombreInsumo != null && cantidadInsumo != null)
            {

            }
            return View();
        }

        [HttpPost]
        public ActionResult NuevoPedido(NuevoPedido nuevoPedido, string idProveedor)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            nuevoPedido.IdProveedor = int.Parse(idProveedor);

            if (ModelState.IsValid)
            {
                bool resultado = AccesoBD.AD_Administrador.nuevoPedido(nuevoPedido);
                if (resultado)
                {
                    AccesoBD.AD_Administrador.enviarMailAProveedor(nuevoPedido, 1, null, null);
                    return RedirectToAction("MisPedidos", "Administrador");
                } 
                else
                {
                    TempData["ErrorInsertPedido"] = "Ocurrió un error al registrar el pedido, inténtelo nuevamente.";
                    return RedirectToAction("MisPedidos", "Administrador");
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult ConcretarPedido()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            int idPedido = int.Parse(Request["listado.IdPedido"]);

            if (ModelState.IsValid)
            {
                bool resultado = AccesoBD.AD_Administrador.concretarUnPedido(idPedido);
                if (resultado) return RedirectToAction("MisPedidos", "Administrador");
                else
                {
                    TempData["ErrorConcretarPedido"] = "Ocurrió un error al concretrar el pedido, inténtelo nuevamente.";
                    return RedirectToAction("MisPedidos", "Administrador");
                }
            }

            return View();
        }

        public ActionResult ModificarPedido(int idPedido)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            List<Proveedor> proveedores = AccesoBD.AD_Administrador.obtenerTodosLosProveedores();
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
            Pedido pedido = AccesoBD.AD_Administrador.obtenerPedidoPorId(idPedido);

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
                bool resultado = AccesoBD.AD_Administrador.modificarPedido(pedidoModificado);
                if (resultado)
                {
                    AccesoBD.AD_Administrador.enviarMailAProveedor(null, 2, pedidoModificado, null);
                    return RedirectToAction("MisPedidos", "Administrador");
                }
                else
                {
                    ViewBag.ErrorModificarPedido = "Error al modificar el pedido, inténtelo nuevamente.";
                    List<Proveedor> proveedores = AccesoBD.AD_Administrador.obtenerTodosLosProveedores();
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
                    Pedido pedido = AccesoBD.AD_Administrador.obtenerPedidoPorId(pedidoModificado.idPedido);

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
            Pedido datosPedido = AccesoBD.AD_Administrador.obtenerPedidoPorId(idPedido);

            bool resultado = AccesoBD.AD_Administrador.eliminarPedido(idPedido);
            if (resultado)
            {
                AccesoBD.AD_Administrador.enviarMailAProveedor(null, 3, null, datosPedido);
                return RedirectToAction("MisPedidos", "Administrador");
            }
            else
            {
                TempData["ErrorEliminarPedido"] = "Ocurrió un error al eliminar el pedido, inténtelo nuevamente.";
                return RedirectToAction("MisPedidos", "Administrador");
            }
        }

        [HttpPost]
        public ActionResult NuevoProveedor(NuevoProveedor nuevoProveedor)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            if (ModelState.IsValid)
            {
                bool resultado = AccesoBD.AD_Administrador.nuevoProveedor(nuevoProveedor);
                if (resultado) return RedirectToAction("MisPedidos", "Administrador");
                else
                {
                    TempData["ErrorInsertProveedor"] = "Ocurrió un erro al registrar el proveedor, inténtelo nuevamente";
                    return RedirectToAction("MisPedidos", "Administrador");
                }
            }

            return View();
        }

        public ActionResult ModificarProveedor(int idProveedor)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            Proveedor proveedor = AccesoBD.AD_Administrador.obtenerProveedorPorId(idProveedor);
            return View(proveedor);
        }

        [HttpPost]
        public ActionResult ModificarProveedor(Proveedor proveedor)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            if (ModelState.IsValid)
            {
                bool resultado = AccesoBD.AD_Administrador.modificarProveedor(proveedor);
                if (resultado) return RedirectToAction("MisPedidos", "Administrador");
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

            if (AccesoBD.AD_Administrador.pedidosSinConcretar(idProveedor))
            {
                bool resultado = AccesoBD.AD_Administrador.eliminarProveedor(idProveedor);
                if (resultado) return RedirectToAction("MisPedidos", "Administrador");
                else
                {
                    TempData["ErrorEliminarProveedor"] = "Ocurrió un error al eliminar el proveedor, inténtelo nuevamente.";
                    return RedirectToAction("MisPedidos", "Administrador");
                }
            }
            else
            {
                TempData["ErrorPedidosSinConcretar"] = "Todavía hay pedidos sin concretar al proveedor que quiere eliminar, concretelos para poder eliminar";
                return RedirectToAction("MisPedidos", "Administrador");
            }          
        }
    }
}