using CanchasGambeta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CanchasGambeta.Controllers
{
    public class PerfilClienteController : Controller
    {
        // GET: PerfilCliente
        public ActionResult PerfilCliente()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                return RedirectToAction("LogIn", "LogIn");
            }
            return View();
        }

        [HttpPost]
        public ActionResult PerfilCliente(Usuario usuario)
        {
            return View();
        }
    }
}