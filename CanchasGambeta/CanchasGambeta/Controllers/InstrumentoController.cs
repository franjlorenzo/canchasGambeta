using CanchasGambeta.Models;
using CanchasGambeta.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CanchasGambeta.Controllers
{
    public class InstrumentoController : Controller
    {
        // GET: Instrumento
        //--------------------------------------INSTRUMENTO-----------------------------------------------
        public ActionResult InstrumentosDisponibles()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            if (TempData["ErrorInsertInstrumento"] != null) ViewBag.ErrorInsertInstrumento = TempData["ErrorInsertInstrumento"].ToString();
            if (TempData["ErrorInsertInstrumentoRoto"] != null) ViewBag.ErrorInsertInstrumentoRoto = TempData["ErrorInsertInstrumentoRoto"].ToString();
            if (TempData["ErrorEliminarInstrumentoRoto"] != null) ViewBag.ErrorEliminarInstrumentoRoto = TempData["ErrorEliminarInstrumentoRoto"].ToString();

            return View(new VistaInstrumentos { TablaInstrumentosDisponibles = AccesoBD.AD_Instrumento.obtenerInstrumentosDisponibles(), TablaInstrumentosRotos = AccesoBD.AD_Instrumento.obtenerInstrumentosRotos(), InstrumentoNuevo = new InstrumentoDisponible() });
        }

        [HttpPost]
        public ActionResult NuevoInstrumento(InstrumentoDisponible nuevoInstrumento)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            if (ModelState.IsValid)
            {
                bool resultado = AccesoBD.AD_Instrumento.nuevoInstrumento(nuevoInstrumento);
                if (resultado) return RedirectToAction("InstrumentosDisponibles", "Instrumento");
                else
                {
                    TempData["ErrorInsertInstrumento"] = "Ocurrió un error al cargar el nuevo instrumento, inténtelo nuevamente.";
                    return RedirectToAction("InstrumentosDisponibles", "Instrumento");
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
                bool resultado = AccesoBD.AD_Instrumento.nuevoElementoRoto(idInstrumento);
                if (resultado) return RedirectToAction("InstrumentosDisponibles", "Instrumento");
                else
                {
                    TempData["ErrorInsertInstrumentoRoto"] = "Ocurrió un error al registrar el instrumento roto, inténtelo nuevamente.";
                    return RedirectToAction("InstrumentosDisponibles", "Instrumento");
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
                bool resultado = AccesoBD.AD_Instrumento.eliminarInstrumentoRoto(idInstrumento);
                if (resultado) return RedirectToAction("InstrumentosDisponibles", "Instrumento");
                else
                {
                    TempData["ErrorEliminarInstrumentoRoto"] = "Ocurrió un error al eliminar el instrumento roto, inténtelo nuevamente.";
                    return RedirectToAction("InstrumentosDisponibles", "Instrumento");
                }
            }
            return View();
        }
    }
}