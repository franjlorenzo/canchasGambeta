using System.Web.Mvc;

namespace CanchasGambeta.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TerminosYCondiciones()
        {
            return View();
        }

        public ActionResult Ayuda()
        {
            return View();
        }
    }
}