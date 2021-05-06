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
            ReservaVM reserva = new ReservaVM();
            List<SelectListItem> canchas = reserva.ListaCanchas.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.tipoCancha,
                    Value = d.idCancha.ToString(),
                    Selected = false
                };
            });

            List<SelectListItem> horarios = reserva.ListaHorarios.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.horario1,
                    Value = d.idHorario.ToString(),
                    Selected = false
                };
            });

            List<SelectListItem> insumos = reserva.ListaInsumos.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.insumo1 + " - ($" + d.precio + ")",
                    Value = d.idInsumo.ToString(),
                    Selected = false
                };
            });

            ViewBag.canchas = canchas;
            ViewBag.horarios = horarios;
            ViewBag.insumos = insumos;
            return View(reserva);
        }
    }
}