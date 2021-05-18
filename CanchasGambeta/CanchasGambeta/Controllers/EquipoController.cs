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
                if (AccesoBD.AD_Equipo.existeIntegranteEnEquipo(email))
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
                        return View();
                    }
                }
            }
            return View();
        }

        public ActionResult ModificarIntegrante(Email email)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            return View(email);
        }

       [HttpPost]
        public ActionResult ModificarIntegrante(Email email, int idEmail)
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
                    ViewBag.ErrorEliminar = "Error al eliminar al integrante, por favor intentelo nuevamente.";
                    int idEquipo = AccesoBD.AD_Equipo.obtenerEquiporPorId();
                    List<MailEquipoVM> listaEquipoMails = AccesoBD.AD_Equipo.obtenerMailsEquipo(idEquipo);
                    ViewBag.nombreEquipo = AccesoBD.AD_Equipo.obtenerNombreEquipo();
                    return View(listaEquipoMails);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult EliminarEquipo(int idEquipo)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            try
            {
                if (ModelState.IsValid)
                {
                    bool resultado = AccesoBD.AD_Equipo.eliminarEquipo(idEquipo);
                    if (resultado)
                    {
                        using (Canchas_GambetaEntities3 db = new Canchas_GambetaEntities3())
                        {
                            var oUser = (from data in db.Usuario
                                         where data.idUsuario == sesion.idUsuario
                                         select data).FirstOrDefault();

                            Session["User"] = oUser;
                        }
                        return RedirectToAction("IndexCliente", "Cliente");
                    }
                }
            }
            catch (Exception)
            {
                List<MailEquipoVM> listaEquipoMails = AccesoBD.AD_Equipo.obtenerMailsEquipo(idEquipo);
                ViewBag.nombreEquipo = AccesoBD.AD_Equipo.obtenerNombreEquipo();
                ViewBag.ErrorEliminar = "Ocurrió un error al eliminar su equipo, inténtelo nuevamente";
                return View(listaEquipoMails);
            }
            return View(idEquipo);
        }
    }
}