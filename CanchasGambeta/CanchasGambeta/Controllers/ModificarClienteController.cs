
using CanchasGambeta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CanchasGambeta.Controllers
{
    public class ModificarClienteController : Controller
    {
        // GET: ModificarCliente
        public ActionResult ModificarCliente()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if(sesion == null)
            {
                return RedirectToAction("LogIn", "LogIn");
            }

            Usuario usuario = AccesoBD.AD_Usuario.obtenerUsuario(sesion.idUsuario);
            return View(usuario);
        }

        [HttpPost]
        public ActionResult ModificarCliente(Usuario usuario, int idUsuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool resultado = AccesoBD.AD_Usuario.actualizarUsuario(usuario);
                    if (resultado)
                    {
                        var oUser = AccesoBD.AD_Usuario.obtenerUsuario(idUsuario);
                        Session["User"] = oUser;

                        return RedirectToAction("PerfilCliente", "PerfilCliente");
                    }
                    else
                    {
                        return View(usuario);
                    }

                }
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }

            return View(usuario);
        }
    }
}