using CanchasGambeta.Models;
using CanchasGambeta.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CanchasGambeta.Controllers
{
    public class InformeController : Controller
    {
        // GET: Informe
        //--------------------------------------RESERVAS ACTIVAS, CANCELADAS Y CONCRETADAS-----------------------------------------------
        public ActionResult ReservasActivas()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                TempData["SesionCaducada"] = "La sesión finalizó, ingrese nuevamente";
                return RedirectToAction("LogIn", "LogIn");
            }

            VistaReservasActivas vista = new VistaReservasActivas
            {
                ReservasActivas = new List<ReservasActivas>(),
                TotalCadaInsumo = new List<Insumo>()
            };

            if (TempData["ReservaConcretada"] != null) ViewBag.ReservaConcretada = TempData["ReservaConcretada"].ToString();
            if (TempData["fechaMayor"] != null) ViewBag.fechaMayor = TempData["fechaMayor"].ToString();
            if (TempData["listaReservasActivas"] != null && TempData["listaTotalInsumos"] != null)
            {
                vista.ReservasActivas = (List<ReservasActivas>)TempData["listaReservasActivas"];
                vista.TotalCadaInsumo = (List<Insumo>)TempData["listaTotalInsumos"];
                return View(vista);
            }
            else
            {
                vista.TotalCadaInsumo = AccesoBD.AD_Informe.ObtenerTotalInsumosReservas(AccesoBD.AD_Informe.ObtenerReservasActivas(DateTime.Now, DateTime.Now));
                vista.ReservasActivas = AccesoBD.AD_Informe.ObtenerReservasActivas(DateTime.Now, DateTime.Now);
                return View(vista);
            }
        }

        [HttpPost]
        public ActionResult ReservasActivas(DateTime fechaInicio, DateTime fechaFin)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                TempData["SesionCaducada"] = "La sesión finalizó, ingrese nuevamente";
                return RedirectToAction("LogIn", "LogIn");
            }

            if (fechaFin >= fechaInicio)
            {
                List<ReservasActivas> listaReservasActivas = AccesoBD.AD_Informe.ObtenerReservasActivas(fechaInicio, fechaFin);
                List<Insumo> listaTotalInsumos = AccesoBD.AD_Informe.ObtenerTotalInsumosReservas(AccesoBD.AD_Informe.ObtenerReservasActivas(fechaInicio, fechaFin));
                TempData["listaReservasActivas"] = listaReservasActivas;
                TempData["listaTotalInsumos"] = listaTotalInsumos;
                return RedirectToAction("ReservasActivas");
            }
            else
            {
                TempData["fechaMayor"] = "La fecha de inicio no puede ser mayor que la fecha de fin";
                List<ReservasActivas> listaReservasActivas = AccesoBD.AD_Informe.ObtenerReservasActivas(DateTime.Today, DateTime.Today);
                List<Insumo> listaTotalInsumos = AccesoBD.AD_Informe.ObtenerTotalInsumosReservas(AccesoBD.AD_Informe.ObtenerReservasActivas(DateTime.Today, DateTime.Today));
                TempData["listaReservasActivas"] = listaReservasActivas;
                TempData["listaTotalInsumos"] = listaTotalInsumos;
                return RedirectToAction("ReservasActivas");
            }
        }

        public ActionResult ReservasCanceladas()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                TempData["SesionCaducada"] = "La sesión finalizó, ingrese nuevamente";
                return RedirectToAction("LogIn", "LogIn");
            }

            _ = new List<ReservasCanceladas>();
            List<ReservasCanceladas> listaReservasCanceladas;
            if (TempData["fechaMayor"] != null) ViewBag.fechaMayor = TempData["fechaMayor"].ToString();
            if (TempData["listaReservasCanceladas"] != null)
            {
                listaReservasCanceladas = (List<ReservasCanceladas>)TempData["listaReservasCanceladas"];
                return View(listaReservasCanceladas);
            }
            else
            {
                listaReservasCanceladas = AccesoBD.AD_Informe.ObtenerReservasCanceladas(DateTime.Now, DateTime.Now);
                return View(listaReservasCanceladas);
            }
        }

        [HttpPost]
        public ActionResult ReservasCanceladas(DateTime fechaInicio, DateTime fechaFin)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                TempData["SesionCaducada"] = "La sesión finalizó, ingrese nuevamente";
                return RedirectToAction("LogIn", "LogIn");
            }

            if (fechaFin >= fechaInicio)
            {
                List<ReservasCanceladas> listaReservasCanceladas = AccesoBD.AD_Informe.ObtenerReservasCanceladas(fechaInicio, fechaFin);
                TempData["listaReservasCanceladas"] = listaReservasCanceladas;
                return RedirectToAction("ReservasCanceladas");
            }
            else
            {
                TempData["fechaMayor"] = "La fecha de inicio no puede ser mayor que la fecha de fin";
                List<ReservasCanceladas> listaReservasCanceladas = AccesoBD.AD_Informe.ObtenerReservasCanceladas(DateTime.Today, DateTime.Today);
                TempData["listaReservasCanceladas"] = listaReservasCanceladas;
                return RedirectToAction("ReservasCanceladas");
            }
        }

        public ActionResult ReservasConcretadas()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                TempData["SesionCaducada"] = "La sesión finalizó, ingrese nuevamente";
                return RedirectToAction("LogIn", "LogIn");
            }

            _ = new List<TablaReservaVM>();
            List<TablaReservaVM> listaReservasConcretadas;
            if (TempData["fechaMayor"] != null) ViewBag.fechaMayor = TempData["fechaMayor"].ToString();
            if (TempData["listaReservasConcretadas"] != null)
            {
                listaReservasConcretadas = (List<TablaReservaVM>)TempData["listaReservasConcretadas"];
                return View(listaReservasConcretadas);
            }
            else
            {
                listaReservasConcretadas = AccesoBD.AD_Informe.ObtenerReservasConcretadas(DateTime.Now, DateTime.Now);
                return View(listaReservasConcretadas);
            }
        }

        [HttpPost]
        public ActionResult ReservasConcretadas(DateTime fechaInicio, DateTime fechaFin)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                TempData["SesionCaducada"] = "La sesión finalizó, ingrese nuevamente";
                return RedirectToAction("LogIn", "LogIn");
            }

            if (fechaFin >= fechaInicio)
            {
                List<TablaReservaVM> listaReservasConcretadas = AccesoBD.AD_Informe.ObtenerReservasConcretadas(fechaInicio, fechaFin);
                TempData["listaReservasConcretadas"] = listaReservasConcretadas;
                return RedirectToAction("ReservasConcretadas");
            }
            else
            {
                TempData["fechaMayor"] = "La fecha de inicio no puede ser mayor que la fecha de fin";
                List<TablaReservaVM> listaReservasConcretadas = AccesoBD.AD_Informe.ObtenerReservasConcretadas(DateTime.Today, DateTime.Today);
                TempData["listaReservasConcretadas"] = listaReservasConcretadas;
                return RedirectToAction("ReservasConcretadas");
            }
        }

        //--------------------------------------CANCHAS MÁS UTILIZADAS-----------------------------------------------
        public ActionResult CanchasMasUtilizadas()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                TempData["SesionCaducada"] = "La sesión finalizó, ingrese nuevamente";
                return RedirectToAction("LogIn", "LogIn");
            }

            _ = new List<CanchasMasReservadasVM>();
            List<CanchasMasReservadasVM> listaCanchasMasReservadas;
            if (TempData["fechaMayor"] != null) ViewBag.fechaMayor = TempData["fechaMayor"].ToString();
            if (TempData["canchasMasUtilizadas"] != null)
            {
                listaCanchasMasReservadas = (List<CanchasMasReservadasVM>)TempData["canchasMasUtilizadas"];
                return View(listaCanchasMasReservadas);
            }
            else
            {
                listaCanchasMasReservadas = AccesoBD.AD_Informe.ObtenerCanchasMasReservadas(DateTime.Today, DateTime.Today);
                return View(listaCanchasMasReservadas);
            }
        }

        [HttpPost]
        public ActionResult CanchasMasUtilizadas(DateTime fechaInicio, DateTime fechaFin)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                TempData["SesionCaducada"] = "La sesión finalizó, ingrese nuevamente";
                return RedirectToAction("LogIn", "LogIn");
            }

            if (fechaFin >= fechaInicio)
            {
                List<CanchasMasReservadasVM> listaCanchasMasReservadas = AccesoBD.AD_Informe.ObtenerCanchasMasReservadas(fechaInicio, fechaFin);
                TempData["canchasMasUtilizadas"] = listaCanchasMasReservadas;
                return RedirectToAction("CanchasMasUtilizadas");
            }
            else
            {
                TempData["fechaMayor"] = "La fecha de inicio no puede ser mayor que la fecha de fin"; ;
                List<CanchasMasReservadasVM> listaCanchasMasReservadas = AccesoBD.AD_Informe.ObtenerCanchasMasReservadas(DateTime.Today, DateTime.Today);
                TempData["canchasMasUtilizadas"] = listaCanchasMasReservadas;
                return RedirectToAction("CanchasMasUtilizadas");
            }
        }

        //--------------------------------------CLIENTES CON MÁS RESERVAS-----------------------------------------------
        public ActionResult ClientesConMasReservas()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                TempData["SesionCaducada"] = "La sesión finalizó, ingrese nuevamente";
                return RedirectToAction("LogIn", "LogIn");
            }

            _ = new List<ClientesConMasReservas>();
            List<ClientesConMasReservas> listaClientesConMasReservas;
            if (TempData["fechaMayor"] != null) ViewBag.fechaMayor = TempData["fechaMayor"].ToString();
            if (TempData["clientesConMasReservas"] != null)
            {
                listaClientesConMasReservas = (List<ClientesConMasReservas>)TempData["clientesConMasReservas"];
                return View(listaClientesConMasReservas);
            }
            else
            {
                listaClientesConMasReservas = AccesoBD.AD_Informe.ObtenerClientesConMasReservas(DateTime.Today, DateTime.Today);
                return View(listaClientesConMasReservas);
            }
        }

        [HttpPost]
        public ActionResult ClientesConMasReservas(DateTime fechaInicio, DateTime fechaFin)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                TempData["SesionCaducada"] = "La sesión finalizó, ingrese nuevamente";
                return RedirectToAction("LogIn", "LogIn");
            }

            if (fechaFin >= fechaInicio)
            {
                List<ClientesConMasReservas> listaClientesConMasReservas = AccesoBD.AD_Informe.ObtenerClientesConMasReservas(fechaInicio, fechaFin);
                TempData["clientesConMasReservas"] = listaClientesConMasReservas;
                return RedirectToAction("clientesConMasReservas");
            }
            else
            {
                TempData["fechaMayor"] = "La fecha de inicio no puede ser mayor que la fecha de fin";
                List<ClientesConMasReservas> listaClientesConMasReservas = AccesoBD.AD_Informe.ObtenerClientesConMasReservas(DateTime.Today, DateTime.Today);
                TempData["clientesConMasReservas"] = listaClientesConMasReservas;
                return RedirectToAction("clientesConMasReservas");
            }
        }

        //--------------------------------------HORARIOS MÁS RESERVADOS-----------------------------------------------
        public ActionResult HorariosMasReservados()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                TempData["SesionCaducada"] = "La sesión finalizó, ingrese nuevamente";
                return RedirectToAction("LogIn", "LogIn");
            }

            _ = new List<HorariosMasReservados>();
            List<HorariosMasReservados> listaHorariosMasReservados;
            if (TempData["fechaMayor"] != null) ViewBag.fechaMayor = TempData["fechaMayor"].ToString();
            if (TempData["horariosMasReservados"] != null)
            {
                listaHorariosMasReservados = (List<HorariosMasReservados>)TempData["horariosMasReservados"];
                return View(listaHorariosMasReservados);
            }
            else
            {
                listaHorariosMasReservados = AccesoBD.AD_Informe.ObtenerHorariosMasReservados(DateTime.Today, DateTime.Today);
                return View(listaHorariosMasReservados);
            }
        }

        [HttpPost]
        public ActionResult HorariosMasReservados(DateTime fechaInicio, DateTime fechaFin)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                TempData["SesionCaducada"] = "La sesión finalizó, ingrese nuevamente";
                return RedirectToAction("LogIn", "LogIn");
            }

            if (fechaFin >= fechaInicio)
            {
                List<HorariosMasReservados> listaHorariosMasReservados = AccesoBD.AD_Informe.ObtenerHorariosMasReservados(fechaInicio, fechaFin);
                TempData["horariosMasReservados"] = listaHorariosMasReservados;
                return RedirectToAction("HorariosMasReservados");
            }
            else
            {
                TempData["fechaMayor"] = "La fecha de inicio no puede ser mayor que la fecha de fin";
                List<HorariosMasReservados> listaHorariosMasReservados = AccesoBD.AD_Informe.ObtenerHorariosMasReservados(DateTime.Today, DateTime.Today);
                TempData["horariosMasReservados"] = listaHorariosMasReservados;
                return RedirectToAction("HorariosMasReservados");
            }
        }

        //--------------------------------------INSUMOS MÁS CONSUMIDOS-----------------------------------------------
        public ActionResult InsumosMasConsumidos()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                TempData["SesionCaducada"] = "La sesión finalizó, ingrese nuevamente";
                return RedirectToAction("LogIn", "LogIn");
            }

            _ = new List<Insumo>();
            List<Insumo> listaInsumosConsumidos;
            if (TempData["fechaMayor"] != null) ViewBag.fechaMayor = TempData["fechaMayor"].ToString();
            if (TempData["listaInsumosConsumidos"] != null)
            {
                listaInsumosConsumidos = (List<Insumo>)TempData["listaInsumosConsumidos"];
                return View(listaInsumosConsumidos);
            }
            else
            {
                listaInsumosConsumidos = AccesoBD.AD_Informe.ObtenerInsumosConsumidosEntreFechas(DateTime.Now, DateTime.Now);
                return View(listaInsumosConsumidos);
            }
        }

        [HttpPost]
        public ActionResult InsumosMasConsumidos(DateTime fechaInicio, DateTime fechaFin)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                TempData["SesionCaducada"] = "La sesión finalizó, ingrese nuevamente";
                return RedirectToAction("LogIn", "LogIn");
            }

            if (fechaFin >= fechaInicio)
            {
                List<Insumo> listaInsumosConsumidos = AccesoBD.AD_Informe.ObtenerInsumosConsumidosEntreFechas(fechaInicio, fechaFin);
                TempData["listaInsumosConsumidos"] = listaInsumosConsumidos;
                return RedirectToAction("InsumosMasConsumidos");
            }
            else
            {
                TempData["fechaMayor"] = "La fecha de inicio no puede ser mayor que la fecha de fin";
                List<Insumo> listaInsumosConsumidos = AccesoBD.AD_Informe.ObtenerInsumosConsumidosEntreFechas(DateTime.Now, DateTime.Now);
                TempData["listaInsumosConsumidos"] = listaInsumosConsumidos;
                return RedirectToAction("InsumosMasConsumidos");
            }
        }

        //--------------------------------------INSTRUMENTOS ROTOS-----------------------------------------------
        public ActionResult InstrumentosRotos()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                TempData["SesionCaducada"] = "La sesión finalizó, ingrese nuevamente";
                return RedirectToAction("LogIn", "LogIn");
            }

            _ = new List<InstrumentoRotoVM>();
            List<InstrumentoRotoVM> listaInstrumentosRotos;
            if (TempData["fechaMayor"] != null) ViewBag.fechaMayor = TempData["fechaMayor"].ToString();
            if (TempData["listaInstrumentosRotos"] != null)
            {
                listaInstrumentosRotos = (List<InstrumentoRotoVM>)TempData["listaInstrumentosRotos"];
                return View(listaInstrumentosRotos);
            }
            else
            {
                listaInstrumentosRotos = AccesoBD.AD_Informe.ObtenerInstrumentosRotosEntreFechas(DateTime.Now, DateTime.Now);
                return View(listaInstrumentosRotos);
            }
        }

        [HttpPost]
        public ActionResult InstrumentosRotos(DateTime fechaInicio, DateTime fechaFin)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                TempData["SesionCaducada"] = "La sesión finalizó, ingrese nuevamente";
                return RedirectToAction("LogIn", "LogIn");
            }

            if (fechaFin >= fechaInicio)
            {
                List<InstrumentoRotoVM> listaInstrumentosRotos = AccesoBD.AD_Informe.ObtenerInstrumentosRotosEntreFechas(fechaInicio, fechaFin);
                TempData["listaInstrumentosRotos"] = listaInstrumentosRotos;
                return RedirectToAction("InstrumentosRotos");
            }
            else
            {
                TempData["fechaMayor"] = "La fecha de inicio no puede ser mayor que la fecha de fin";
                List<InstrumentoRotoVM> listaInstrumentosRotos = AccesoBD.AD_Informe.ObtenerInstrumentosRotosEntreFechas(DateTime.Now, DateTime.Now);
                TempData["listaInstrumentosRotos"] = listaInstrumentosRotos;
                return RedirectToAction("InstrumentosRotos");
            }
        }
    }
}