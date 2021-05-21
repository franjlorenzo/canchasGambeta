using CanchasGambeta.Models;
using CanchasGambeta.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CanchasGambeta.Controllers
{
    public class InformeController : Controller
    {
        // GET: Informe
        public ActionResult ReservasActivasYCandeladas()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            return View(new VistaReservasActivasYCanceladas { listaReservasActivas = AccesoBD.AD_Informe.obtenerReservasActivas(), listaReservasCanceladas = AccesoBD.AD_Informe.obtenerReservasCanceladas() });
        }

        public ActionResult CanchasMasUtilizadas()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            List<CanchasMasReservadasVM> listaCanchasMasReservadas = AccesoBD.AD_Informe.obtenerCanchasMasReservadas();
            return View(listaCanchasMasReservadas);
        }

        public ActionResult ClientesConMasReservas()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            List<ClientesConMasReservas> listaClientesConMasReservas = AccesoBD.AD_Informe.obtenerClientesConMasReservas();
            return View(listaClientesConMasReservas);
        }
    }
}