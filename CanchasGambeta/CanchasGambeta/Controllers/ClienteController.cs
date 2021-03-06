using CanchasGambeta.Models;
using CanchasGambeta.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CanchasGambeta.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult IndexCliente()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                TempData["SesionCaducada"] = "La sesión finalizó, ingrese nuevamente";
                return RedirectToAction("LogIn", "LogIn");
            }

            List<ReservasActivas> reservasCliente = AccesoBD.AD_Informe.ObtenerReservasActivasDelCliente(sesion.idUsuario);
            return View(reservasCliente);
        }

        public ActionResult PerfilCliente()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                TempData["SesionCaducada"] = "La sesión finalizó, ingrese nuevamente";
                return RedirectToAction("LogIn", "LogIn");
            }

            return View();
        }

        public ActionResult ModificarCliente()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                TempData["SesionCaducada"] = "La sesión finalizó, ingrese nuevamente";
                return RedirectToAction("LogIn", "LogIn");
            }

            Usuario usuario = AccesoBD.AD_Usuario.ObtenerUsuario(sesion.idUsuario);
            return View(usuario);
        }

        [HttpPost]
        public ActionResult ModificarCliente(Usuario usuarioModificado)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                TempData["SesionCaducada"] = "La sesión finalizó, ingrese nuevamente";
                return RedirectToAction("LogIn", "LogIn");
            }

            if (ModelState.IsValid)
            {
                if (usuarioModificado.email != sesion.email)
                {
                    if (!AccesoBD.AD_Usuario.ExisteEmailUsuario(usuarioModificado.email))
                    {
                        bool resultado = AccesoBD.AD_Usuario.ActualizarUsuario(usuarioModificado);
                        if (resultado)
                        {
                            var oUser = AccesoBD.AD_Usuario.ObtenerUsuario(sesion.idUsuario);
                            Session["User"] = oUser;

                            return RedirectToAction("PerfilCliente", "Cliente");
                        }
                        else
                        {
                            ViewBag.ErrorActualizarUsuario = "Ocurrió un error al actualizar su perfil, inténtelo nuevamente.";
                            Usuario usuario = AccesoBD.AD_Usuario.ObtenerUsuario(sesion.idUsuario);
                            return View(usuario);
                        }
                    }
                    else
                    {
                        ViewBag.EmailYaRegistrado = "El correo electrónico al que desea cambiarse ya está registrado.";
                        Usuario usuario = AccesoBD.AD_Usuario.ObtenerUsuario(sesion.idUsuario);
                        return View(usuario);
                    }
                }
                else
                {
                    bool resultado = AccesoBD.AD_Usuario.ActualizarUsuario(usuarioModificado);
                    if (resultado)
                    {
                        var oUser = AccesoBD.AD_Usuario.ObtenerUsuario(sesion.idUsuario);
                        Session["User"] = oUser;

                        return RedirectToAction("PerfilCliente", "Cliente");
                    }
                    else
                    {
                        ViewBag.ErrorActualizarUsuario = "Ocurrió un error al actualizar su perfil, inténtelo nuevamente.";
                        Usuario usuario = AccesoBD.AD_Usuario.ObtenerUsuario(sesion.idUsuario);
                        return View(usuario);
                    }
                }
            }

            return View(usuarioModificado);
        }
    }
}