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

            _ = new List<CanchasMasReservadasVM>();
            List<CanchasMasReservadasVM> listaCanchasMasReservadas;
            if (TempData["canchasMasUtilizadas"] != null)
            {
                listaCanchasMasReservadas = (List<CanchasMasReservadasVM>)TempData["canchasMasUtilizadas"];
                return View(listaCanchasMasReservadas);
            }
            else
            {
                listaCanchasMasReservadas = AccesoBD.AD_Informe.obtenerCanchasMasReservadas(DateTime.Today, DateTime.Today);
                return View(listaCanchasMasReservadas);
            }
        }

        [HttpPost]
        public ActionResult CanchasMasUtilizadas(DateTime fechaInicio, DateTime fechaFin)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            if(fechaFin >= fechaInicio)
            {
                List<CanchasMasReservadasVM> listaCanchasMasReservadas = AccesoBD.AD_Informe.obtenerCanchasMasReservadas(fechaInicio, fechaFin);
                TempData["canchasMasUtilizadas"] = listaCanchasMasReservadas;
                return RedirectToAction("CanchasMasUtilizadas");
            }
            else
            {
                ViewBag.fechaMayor = "La fecha de inicio no puede ser mayor que la fecha de fin";
                List<CanchasMasReservadasVM> listaCanchasMasReservadas = AccesoBD.AD_Informe.obtenerCanchasMasReservadas(DateTime.Today, DateTime.Today);
                TempData["canchasMasUtilizadas"] = listaCanchasMasReservadas;
                return RedirectToAction("CanchasMasUtilizadas");
            }
            
        }

        public ActionResult ClientesConMasReservas()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            _ = new List<ClientesConMasReservas>();
            List<ClientesConMasReservas> listaClientesConMasReservas;
            if (TempData["clientesConMasReservas"] != null)
            {
                listaClientesConMasReservas = (List<ClientesConMasReservas>)TempData["clientesConMasReservas"];
                return View(listaClientesConMasReservas);
            }
            else
            {
                listaClientesConMasReservas = AccesoBD.AD_Informe.obtenerClientesConMasReservas(DateTime.Today, DateTime.Today);
                return View(listaClientesConMasReservas);
            }
        }

        [HttpPost]
        public ActionResult ClientesConMasReservas(DateTime fechaInicio, DateTime fechaFin)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            if (fechaFin >= fechaInicio)
            {
                List<ClientesConMasReservas> listaClientesConMasReservas = AccesoBD.AD_Informe.obtenerClientesConMasReservas(fechaInicio, fechaFin);
                TempData["clientesConMasReservas"] = listaClientesConMasReservas;
                return RedirectToAction("clientesConMasReservas");
            }
            else
            {
                ViewBag.fechaMayor = "La fecha de inicio no puede ser mayor que la fecha de fin";
                List<ClientesConMasReservas> listaClientesConMasReservas = AccesoBD.AD_Informe.obtenerClientesConMasReservas(DateTime.Today, DateTime.Today);
                TempData["clientesConMasReservas"] = listaClientesConMasReservas;
                return RedirectToAction("clientesConMasReservas");
            }
        }

        public ActionResult HorariosMasReservados()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            _ = new List<HorariosMasReservados>();
            List<HorariosMasReservados> listaHorariosMasReservados;
            if(TempData["horariosMasReservados"] != null)
            {
                listaHorariosMasReservados = (List<HorariosMasReservados>)TempData["horariosMasReservados"];
                return View(listaHorariosMasReservados);
            }
            else
            {
                listaHorariosMasReservados = AccesoBD.AD_Informe.obtenerHorariosMasReservados(DateTime.Today, DateTime.Today);
                return View(listaHorariosMasReservados);
            }           
        }

        [HttpPost]
        public ActionResult HorariosMasReservados(DateTime fechaInicio, DateTime fechaFin)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            if (fechaFin >= fechaInicio)
            {
                List<HorariosMasReservados> listaHorariosMasReservados = AccesoBD.AD_Informe.obtenerHorariosMasReservados(fechaInicio, fechaFin);
                TempData["horariosMasReservados"] = listaHorariosMasReservados;
                return RedirectToAction("HorariosMasReservados");
            }
            else
            {
                ViewBag.fechaMayor = "La fecha de inicio no puede ser mayor que la fecha de fin";
                List<HorariosMasReservados> listaHorariosMasReservados = AccesoBD.AD_Informe.obtenerHorariosMasReservados(DateTime.Today, DateTime.Today);
                TempData["horariosMasReservados"] = listaHorariosMasReservados;
                return RedirectToAction("HorariosMasReservados");
            }
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
            
            _ = new List<Insumo>();
            List<Insumo> listaInsumosConsumidos;
            if (TempData["listaInsumosConsumidos"] != null)
            {
                listaInsumosConsumidos = (List<Insumo>)TempData["listaInsumosConsumidos"];
                return View(listaInsumosConsumidos);
            }
            else
            {
                listaInsumosConsumidos = AccesoBD.AD_Informe.obtenerInsumosConsumidosEntreFechas(DateTime.Now, DateTime.Now);
                return View(listaInsumosConsumidos);
            }
        }

        [HttpPost]
        public ActionResult InsumosMasConsumidos(DateTime fechaInicio, DateTime fechaFin)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            if(fechaFin >= fechaInicio)
            {
                List<Insumo> listaInsumosConsumidos = AccesoBD.AD_Informe.obtenerInsumosConsumidosEntreFechas(fechaInicio, fechaFin);
                TempData["listaInsumosConsumidos"] = listaInsumosConsumidos;
                return RedirectToAction("InsumosMasConsumidos");
            }
            else
            {
                ViewBag.fechaMayor = "La fecha de inicio no puede ser mayor que la fecha de fin";
                List<Insumo> listaInsumosConsumidos = AccesoBD.AD_Informe.obtenerInsumosConsumidosEntreFechas(DateTime.Now, DateTime.Now);
                TempData["listaInsumosConsumidos"] = listaInsumosConsumidos;
                return RedirectToAction("InsumosMasConsumidos");
            }
        }
    }
}