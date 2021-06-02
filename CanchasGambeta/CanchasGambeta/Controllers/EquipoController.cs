using CanchasGambeta.Models;
using CanchasGambeta.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CanchasGambeta.Controllers
{
    public class EquipoController : Controller
    {
        // GET: Equipo
        public ActionResult NuevoEquipo()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            return View();
        }

        [HttpPost]
        public ActionResult NuevoEquipo(string nombreEquipo)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            if (ModelState.IsValid)
            {
                bool existeEquipo = AccesoBD.AD_Equipo.existeEquipo(nombreEquipo);
                if (existeEquipo)
                {
                    ViewBag.ErrorCreacion = "El nombre del equipo ya se encuentra registrado, intente con otro.";
                    return View();
                }

                bool resultado = AccesoBD.AD_Equipo.nuevoEquipo(nombreEquipo);
                if (resultado)
                {
                    bool insertExitoso = AccesoBD.AD_Equipo.insertarEquipoEnUsuario(nombreEquipo);
                    if (insertExitoso)
                    {
                        var oUser = AccesoBD.AD_Usuario.obtenerUsuario(sesion.idUsuario);
                        Session["User"] = oUser;
                        return RedirectToAction("MiEquipo", "Equipo");
                    }
                }     
            }
            return View();
        }

        public ActionResult MiEquipo()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            int idEquipo = AccesoBD.AD_Equipo.obtenerEquiporPorId();
            List<MailEquipoVM> listaEquipoMails = AccesoBD.AD_Equipo.obtenerMailsEquipo(idEquipo);
            ViewBag.nombreEquipo = AccesoBD.AD_Equipo.obtenerNombreEquipo();

            if(TempData["ErrorEliminarIntegrante"] != null) ViewBag.ErrorEliminarIntegrante = TempData["ErrorEliminarIntegrante"].ToString();
            if (TempData["ErrorEliminarEquipo"] != null) ViewBag.ErrorEliminarEquipo = TempData["ErrorEliminarEquipo"].ToString();
            return View(listaEquipoMails);
        }

        [HttpPost]
        public ActionResult MiEquipo(string email)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            if (ModelState.IsValid)
            {
                if(email == sesion.email)
                {
                    ViewBag.ErrorInsertIntegrante = "Usted ya forma parte del equipo.";
                    int idEquipo = AccesoBD.AD_Equipo.obtenerEquiporPorId();
                    List<MailEquipoVM> listaEquipoMails = AccesoBD.AD_Equipo.obtenerMailsEquipo(idEquipo);
                    return View(listaEquipoMails);
                }
                else if (AccesoBD.AD_Equipo.existeIntegranteEnEquipo(email))
                {
                    ViewBag.ErrorInsertIntegrante = "El integrante que desea agregar ya existe en su equipo.";
                    int idEquipo = AccesoBD.AD_Equipo.obtenerEquiporPorId();
                    List<MailEquipoVM> listaEquipoMails = AccesoBD.AD_Equipo.obtenerMailsEquipo(idEquipo);
                    return View(listaEquipoMails);
                }
                else
                {
                    bool resultado = AccesoBD.AD_Equipo.agregarNuevoIntegrante(email);
                    if (resultado) return RedirectToAction("MiEquipo", "Equipo");
                    else
                    {
                        ViewBag.ErrorInsertIntegrante = "Ocurrió un error al cargar el nuevo integrante. Intenteló nuevamente.";
                        int idEquipo = AccesoBD.AD_Equipo.obtenerEquiporPorId();
                        List<MailEquipoVM> listaEquipoMails = AccesoBD.AD_Equipo.obtenerMailsEquipo(idEquipo);
                        return View(listaEquipoMails);
                    }
                }
            }
            return View();
        }

        public ActionResult ModificarIntegrante()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            int idEmail = int.Parse(Request["idEmail"]);
            string email = Request["email1"];
            Email integrante = new Email(idEmail, email);

            return View(integrante);
        }

       [HttpPost]
        public ActionResult ModificarIntegrante(Email email)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            try
            {
                if (ModelState.IsValid)
                {
                    if (AccesoBD.AD_Equipo.existeIntegranteEnEquipo(email.email1))
                    {
                        ViewBag.ErrorModificar = "El integrante que desea modificar ya existe en su equipo.";
                        return View(email);
                    }
                    else
                    {
                        bool resultado = AccesoBD.AD_Equipo.modificarIntegrante(email);
                        if (resultado) return RedirectToAction("MiEquipo", "Equipo");
                        else
                        {
                            ViewBag.ErrorModificar = "Ocurrió un error al modificar el integrante, por favor inténtelo de nuevo.";
                            return View(email);
                        }                    
                    }
                }
            }
            catch(Exception ex)
            {
                ViewBag.ErrorModificar = ex.Message;
                return View(email);
            }

            return View(email);
        }

        [HttpPost]
        public ActionResult EliminarIntegrante(int idEmail, string email)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            if (ModelState.IsValid)
            {
                Email integrante = new Email(idEmail, email);
                bool resultado = AccesoBD.AD_Equipo.eliminarIntegrante(integrante);
                if (resultado) return RedirectToAction("MiEquipo", "Equipo");
                else
                {
                    TempData["ErrorEliminarIntegrante"] = "Error al eliminar al integrante, por favor inténtelo nuevamente.";
                    return RedirectToAction("MiEquipo", "Equipo");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult EliminarEquipo(int idEquipo)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            if (ModelState.IsValid)
            {
                bool resultado = AccesoBD.AD_Equipo.eliminarEquipo(idEquipo);
                if (resultado)
                {
                    using (Canchas_GambetaEntities db = new Canchas_GambetaEntities())
                    {
                        var oUser = (from data in db.Usuario
                                     where data.idUsuario == sesion.idUsuario
                                     select data).FirstOrDefault();
                        Session["User"] = oUser;
                    }
                    return RedirectToAction("IndexCliente", "Cliente");
                }
                else
                {
                    TempData["ErrorEliminarEquipo"] = "Ocurrió un error al eliminar su equipo, inténtelo nuevamente";
                    return RedirectToAction("MiEquipo", "Equipo");
                }
            }
            return View(idEquipo);
        }

        public ActionResult CambiarNombreEquipo()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            Equipo equipo = AccesoBD.AD_Equipo.obtenerEquipoUsuario(sesion.equipo);
            return View(equipo);
        }

        [HttpPost]
        public ActionResult CambiarNombreEquipo(string nombreEquipo)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            if (ModelState.IsValid)
            {
                bool existeEquipo = AccesoBD.AD_Equipo.existeEquipo(nombreEquipo);
                if (existeEquipo)
                {
                    ViewBag.ErrorCreacion = "El nombre del equipo ya se encuentra registrado, intente con otro.";
                    Equipo equipo = AccesoBD.AD_Equipo.obtenerEquipoUsuario(sesion.equipo);
                    return View(equipo);
                }

                bool resultado = AccesoBD.AD_Equipo.cambiarNombreEquipo(nombreEquipo);
                if (resultado) return RedirectToAction("MiEquipo", "Equipo");
                else
                {
                    ViewBag.ErrorCambiarNombre = "Ocurrió un error al cambiar el nombre de su equipo, inténtelo nuevamente";
                    Equipo equipo = AccesoBD.AD_Equipo.obtenerEquipoUsuario(sesion.equipo);
                    return View(equipo);
                }
            }
            return View();
        }
    }
}