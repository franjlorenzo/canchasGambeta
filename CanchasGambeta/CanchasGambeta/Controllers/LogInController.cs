using System;
using System.Linq;
using System.Web.Mvc;

namespace CanchasGambeta.Controllers
{
    public class LogInController : Controller
    {
        // GET: LogIn
        public ActionResult LogIn()
        {
            if (TempData["SesionCaducada"] != null) ViewBag.SesionCaducada = TempData["SesionCaducada"].ToString();
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(string Email, string Password)
        {
            try
            {
                using (Models.Canchas_GambetaEntities db = new Models.Canchas_GambetaEntities())
                {
                    var oUser = (from data in db.Usuario
                                 where data.email == Email.Trim() && data.password == Password.Trim()
                                 select data).FirstOrDefault(); //FirstOrDefault devuelve el elemento o devuelve null

                    if (oUser == null)
                    {
                        ViewBag.Error = "Usuario o contraseña invalida";
                        return View();
                    }

                    Session["User"] = oUser;
                    Session.Timeout = 15;

                    if (oUser.rol == 2) return RedirectToAction("IndexCliente", "Cliente");
                    else return RedirectToAction("IndexAdministrador", "Administrador");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}