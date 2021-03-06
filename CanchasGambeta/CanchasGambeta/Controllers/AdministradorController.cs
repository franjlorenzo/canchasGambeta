using CanchasGambeta.Models;
using System.Web.Mvc;

namespace CanchasGambeta.Controllers
{
    public class AdministradorController : Controller
    {
        // GET: Administrador
        public ActionResult IndexAdministrador()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                TempData["SesionCaducada"] = "La sesión finalizó, ingrese nuevamente";
                return RedirectToAction("LogIn", "LogIn");
            }

            return View();
        }

        public ActionResult PerfilAdministrador()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                TempData["SesionCaducada"] = "La sesión finalizó, ingrese nuevamente";
                return RedirectToAction("LogIn", "LogIn");
            }

            return View();
        }

        public ActionResult ModificarPerfilAdministrador()
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
        public ActionResult ModificarPerfilAdministrador(Usuario usuarioModificado)
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

                            return RedirectToAction("PerfilAdministrador", "Administrador");
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

                        return RedirectToAction("PerfilAdministrador", "Administrador");
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