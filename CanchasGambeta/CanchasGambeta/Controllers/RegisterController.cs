using CanchasGambeta.Models;
using System.Linq;
using System.Web.Mvc;

namespace CanchasGambeta.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Usuario usuario, string Email, string Password)
        {
            if (ModelState.IsValid)
            {
                bool resultado = AccesoBD.AD_Usuario.nuevoUsuario(usuario);
                if (resultado)
                {
                    using (Canchas_GambetaEntities db = new Canchas_GambetaEntities())
                    {
                        var oUser = (from data in db.Usuario
                                     where data.email == Email.Trim() && data.password == Password.Trim()
                                     select data).FirstOrDefault();

                        Session["User"] = oUser;
                        Session.Timeout = 10;

                        if (oUser != null) return RedirectToAction("IndexCliente", "Cliente");
                        else
                        {
                            ViewBag.Error = "Error al crear su cuenta. Por favor intentelo de nuevo";
                            return View();
                        }
                    }
                }
                else
                {
                    ViewBag.Error = "Error al crear su cuenta. Por favor intentelo de nuevo";
                    return View(usuario);
                }
            }
            return View(usuario);
        }
    }
}