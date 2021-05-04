using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CanchasGambeta.Models;

namespace CanchasGambeta.Controllers
{
    public class AdministradorController : Controller
    {
        // GET: Administrador
        public ActionResult IndexAdministrador()
        {
            return View();
        }

        public ActionResult PerfilAdministrador()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                return RedirectToAction("LogIn", "LogIn");
            }
            return View();
        }
    }
}