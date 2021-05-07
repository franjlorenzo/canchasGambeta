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
            return View();
        }

        [HttpPost]
        public ActionResult NuevaReserva(NuevaReservaVM nuevaReserva)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                return RedirectToAction("LogIn", "LogIn");
            }

            if (ModelState.IsValid)
            {
                bool resultado = AccesoBD.AD_Reserva.nuevaReserva(nuevaReserva);
                if (resultado)
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