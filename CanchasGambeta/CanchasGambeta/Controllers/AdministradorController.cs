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
            if(TempData["ErrorEliminarPedido"] != null) ViewBag.ErrorEliminarPedido = TempData["ErrorEliminarPedido"].ToString();
            return View( new VistaMisPedidos { TablaPedido = AccesoBD.AD_Administrador.obtenerTodosLosPedidos(), NuevoPedido = new NuevoPedido()});
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
                if (resultado) return RedirectToAction("MisPedidos", "Administrador");
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
                if (resultado) return RedirectToAction("MisPedidos", "Administrador");
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

            bool resultado = AccesoBD.AD_Administrador.eliminarPedido(idPedido);
            if (resultado) return RedirectToAction("MisPedidos", "Administrador");
            else
            {
                TempData["ErrorEliminarPedido"] = "Ocurrió un error al eliminar el pedido, inténtelo nuevamente.";
                return RedirectToAction("MisPedidos", "Administrador");
            }
        }
    }
}