using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CanchasGambeta.Controllers
{
    public class CerrarSesionController : Controller
    {
        // GET: CerrarSesion
        public ActionResult IndexCliente()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult PerfilCliente()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ModificarCliente()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult MiEquipo()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ModificarIntegrante()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult EliminarEquipo()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult NuevoEquipo()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult EliminarIntegrante()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult IndexAdministrador()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ModificarPerfilAdministrador()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult PerfilAdministrador()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult MisInsumos()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ModificarInsumo()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult EliminarInsumo()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}