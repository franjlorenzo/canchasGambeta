using CanchasGambeta.Models;
using System.Web.Mvc;

namespace CanchasGambeta.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                TempData["SesionCaducada"] = "La sesión finalizó, ingrese nuevamente";
                return RedirectToAction("LogIn", "LogIn");
            }

            return View();
        }

        public ActionResult TerminosYCondiciones()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                TempData["SesionCaducada"] = "La sesión finalizó, ingrese nuevamente";
                return RedirectToAction("LogIn", "LogIn");
            }

            return View();
        }

        public ActionResult Ayuda()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                TempData["SesionCaducada"] = "La sesión finalizó, ingrese nuevamente";
                return RedirectToAction("LogIn", "LogIn");
            }

            return View();
        }
    }
}