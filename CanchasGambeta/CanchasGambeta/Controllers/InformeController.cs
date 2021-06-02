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
        public ActionResult ReservasActivasYCanceladas()
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

        public ActionResult HorariosMasReservados()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            List<HorariosMasReservados> listaHorariosMasReservados = AccesoBD.AD_Informe.obtenerHorariosMasReservados();
            return View(listaHorariosMasReservados);
        }

        public ActionResult InstrumentosDisponibles()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            if (TempData["ErrorInsertInstrumento"] != null) ViewBag.ErrorInsertInstrumento = TempData["ErrorInsertInstrumento"].ToString();
            if (TempData["ErrorInsertInstrumentoRoto"] != null) ViewBag.ErrorInsertInstrumentoRoto = TempData["ErrorInsertInstrumentoRoto"].ToString();
            if (TempData["ErrorEliminarInstrumentoRoto"] != null) ViewBag.ErrorEliminarInstrumentoRoto = TempData["ErrorEliminarInstrumentoRoto"].ToString();

            return View(new VistaInstrumentos { TablaInstrumentosDisponibles = AccesoBD.AD_Informe.obtenerInstrumentosDisponibles(), TablaInstrumentosRotos = AccesoBD.AD_Informe.obtenerInstrumentosRotos(), InstrumentoNuevo = new InstrumentoDisponible() });
        }

        [HttpPost]
        public ActionResult NuevoInstrumento(InstrumentoDisponible nuevoInstrumento)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            if (ModelState.IsValid)
            {
                bool resultado = AccesoBD.AD_Informe.nuevoInstrumento(nuevoInstrumento);
                if (resultado) return RedirectToAction("InstrumentosDisponibles", "Informe");
                else
                {
                    TempData["ErrorInsertInstrumento"] = "Ocurrió un error al cargar el nuevo instrumento, inténtelo nuevamente.";
                    return RedirectToAction("InstrumentosDisponibles", "Informe");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult NuevoInstrumentoRoto(int idInstrumento)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            if (ModelState.IsValid)
            {
                bool resultado = AccesoBD.AD_Informe.nuevoElementoRoto(idInstrumento);
                if (resultado) return RedirectToAction("InstrumentosDisponibles", "Informe");
                else
                {
                    TempData["ErrorInsertInstrumentoRoto"] = "Ocurrió un error al registrar el instrumento roto, inténtelo nuevamente.";
                    return RedirectToAction("InstrumentosDisponibles", "Informe");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult EliminarInstrumentoRoto(int idInstrumento)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            if (ModelState.IsValid)
            {
                bool resultado = AccesoBD.AD_Informe.eliminarInstrumentoRoto(idInstrumento);
                if (resultado) return RedirectToAction("InstrumentosDisponibles", "Informe");
                else
                {
                    TempData["ErrorEliminarInstrumentoRoto"] = "Ocurrió un error al eliminar el instrumento roto, inténtelo nuevamente.";
                    return RedirectToAction("InstrumentosDisponibles", "Informe");
                }
            }
            return View();
        }

        public ActionResult InsumosMasConsumidos()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            return View();
        }

        [HttpPost]
        public ActionResult InsumosMasConsumidos(DateTime fechaInicio, DateTime fechaFin)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            List<Insumo> listaInsumosConsumidos = AccesoBD.AD_Informe.obtenerInsumosConsumidosEntreFechas(fechaInicio, fechaFin);
            return View(listaInsumosConsumidos);
        }
    }
}