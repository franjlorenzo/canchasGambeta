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
    }
}