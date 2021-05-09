using CanchasGambeta.Models;
using CanchasGambeta.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CanchasGambeta.Controllers
{
    public class ReservaController : Controller
    {
        // GET: Reserva
        public ActionResult MisReservas()
        { 
            List<Cancha> canchas = AccesoBD.AD_Reserva.obtenerCanchas();
            List<SelectListItem> listaCanchas = canchas.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.tipoCancha,
                    Value = d.idCancha.ToString(),
                    Selected = false
                };
            });

            List<Horario> horarios = AccesoBD.AD_Reserva.obtenerHorarios();
            List<SelectListItem> listaHorarios = horarios.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.horario1,
                    Value = d.idHorario.ToString(),
                    Selected = false
                };
            });

            List<Insumo> insumos = AccesoBD.AD_Insumo.obtenerInsumos();
            List<SelectListItem> listaInsumos = insumos.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.insumo1 + " - ($" + d.precio + ")",
                    Value = d.idInsumo.ToString(),
                    Selected = false
                };
            });

            ViewBag.canchas = listaCanchas;
            ViewBag.horarios = listaHorarios;
            ViewBag.insumos = listaInsumos;
            return View( new VistaReserva {NuevaReservaVM = new NuevaReservaVM(), TablaReservaVM = AccesoBD.AD_Reserva.obtenerReservasDelCliente(),});
        }

        [HttpPost]
        public ActionResult NuevaReserva(NuevaReservaVM nuevaReserva, List<int> cantidad)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                return RedirectToAction("LogIn", "LogIn");
            }

            nuevaReserva.Fecha = DateTime.Parse(Request["NuevaReservaVM.Fecha"]);
            nuevaReserva.ServicioAsador = bool.Parse(Request["NuevaReservaVM.ServicioAsador"].Contains("true").ToString());
            nuevaReserva.ServicioInstrumento  = bool.Parse(Request.Form["NuevaReservaVM.ServicioInstrumento"].Contains("true").ToString());

            if (ModelState.IsValid)
            {
                bool insertExitoso = AccesoBD.AD_Reserva.nuevaReserva(nuevaReserva);
                bool insertReservaInsumo = AccesoBD.AD_Reserva.insertReservaInsumo(nuevaReserva, AccesoBD.AD_Reserva.obtenerReservaPorAtributos(nuevaReserva.IdCancha, nuevaReserva.IdHorario, nuevaReserva.Fecha), cantidad);
                if (insertExitoso)
                {
                    return RedirectToAction("MisReservas", "Reserva");
                }
                else
                {
                    ViewBag.ErrorInsertReserva = "Ocurrió un error al registrar la reserva. Intenteló nuevamente.";
                    return View(nuevaReserva);
                }
            }
            return View(nuevaReserva);
        }
    }
}