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
        public ActionResult ModificarPerfilAdministrador(Usuario usuario)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            try
            {
                if (ModelState.IsValid)
                {
                    bool resultado = AccesoBD.AD_Usuario.actualizarUsuario(usuario);
                    if (resultado)
                    {
                        var oUser = AccesoBD.AD_Usuario.obtenerUsuario(sesion.idUsuario);
                        Session["User"] = oUser;

                        return RedirectToAction("PerfilAdministrador", "Administrador");
                    }
                    else return View(usuario);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
            return View(usuario);
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
            return View( new VistaMisPedidos { TablaPedido = AccesoBD.AD_Administrador.obtenerTodosLosPedidos(), NuevoPedido = new NuevoPedido()});
        }

        [HttpPost]
        public ActionResult NuevoPedido(NuevoPedido nuevoPedido, string idProveedor)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            nuevoPedido.IdProveedor = int.Parse(idProveedor);

            try
            {
                if (ModelState.IsValid)
                {
                    bool resultado = AccesoBD.AD_Administrador.nuevoPedido(nuevoPedido);
                    if (resultado) return RedirectToAction("MisPedidos", "Administrador");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorInsertPedido = ex.Message;
                return RedirectToAction("MisPedidos", "Administrador");
            }

            return View();
        }

        [HttpPost]
        public ActionResult ConcretarPedido()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            int idPedido = int.Parse(Request["listado.IdPedido"]);

            try
            {
                if (ModelState.IsValid)
                {
                    bool resultado = AccesoBD.AD_Administrador.concretarUnPedido(idPedido);
                    if (resultado) return RedirectToAction("MisPedidos", "Administrador");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorConcretarPedido = ex.Message;
                return RedirectToAction("MisPedidos", "Administrador");
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
        public ActionResult ModificarPedido(Pedido pedido)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            try
            {
                if (ModelState.IsValid)
                {
                    bool resultado = AccesoBD.AD_Administrador.modificarPedido(pedido);
                    if (resultado) return RedirectToAction("MisPedidos", "Administrador");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorModificarPedido = ex.Message;
                return View(pedido);
            }

            return View(pedido);
        }

        [HttpPost]
        public ActionResult EliminarPedido()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            int idPedido = int.Parse(Request["listado.IdPedido"]);

            try
            {
                bool resultado = AccesoBD.AD_Administrador.eliminarPedido(idPedido);
                if (resultado) return RedirectToAction("MisPedidos", "Administrador");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorEliminarPedido = ex.Message;
                return RedirectToAction("MisPedidos", "Administrador");
            }

            return View();
        }
    }
}