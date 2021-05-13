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
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

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

            /*List<Insumo> insumos = AccesoBD.AD_Insumo.obtenerInsumos();
            List<SelectListItem> listaInsumos = insumos.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.insumo1 + " - ($" + d.precio + ")",
                    Value = d.idInsumo.ToString(),
                    Selected = false
                };
            });*/

            ViewBag.canchas = listaCanchas;
            ViewBag.horarios = listaHorarios;
            //ViewBag.insumos = listaInsumos;
            return View( new VistaReserva {NuevaReservaVM = new NuevaReservaVM(), TablaReservaVM = AccesoBD.AD_Reserva.obtenerReservasDelCliente()});
        }

        [HttpPost]
        public ActionResult NuevaReserva(NuevaReservaVM nuevaReserva, List<int> cantidad)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            nuevaReserva.Fecha = DateTime.Parse(Request["NuevaReservaVM.Fecha"]);
            nuevaReserva.ServicioAsador = bool.Parse(Request["NuevaReservaVM.ServicioAsador"].Contains("true").ToString());
            nuevaReserva.ServicioInstrumento  = bool.Parse(Request.Form["NuevaReservaVM.ServicioInstrumento"].Contains("true").ToString());

            if (ModelState.IsValid)
            {
                bool insertExitoso = AccesoBD.AD_Reserva.nuevaReserva(nuevaReserva);
                if (insertExitoso)
                {
                    bool insertReservaInsumo = AccesoBD.AD_Reserva.insertReservaInsumo(nuevaReserva, AccesoBD.AD_Reserva.obtenerReservaPorAtributos(nuevaReserva.IdCancha, nuevaReserva.IdHorario, nuevaReserva.Fecha), cantidad);
                    if (insertReservaInsumo)
                    {
                        return RedirectToAction("MisReservas", "Reserva");
                    }

                    /*bool hayInsumo = false;
                    for (int i = 0; i < cantidad.Count; i++)
                    {
                        if (cantidad[i] != 0)
                        {
                            hayInsumo = true;
                            break;
                        }
                    }

                    if (hayInsumo)
                    {
                        
                    }
                    else
                    {
                        return RedirectToAction("MisReservas", "Reserva");
                    }*/
                }
                else
                {
                    ViewBag.ErrorInsertReserva = "Ocurrió un error al registrar la reserva. Intenteló nuevamente.";
                    return RedirectToAction("MisReservas", "Reserva");
                }
            }
            return View(nuevaReserva);
        }

        public ActionResult ModificarReserva(int idReserva)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            ActualizarReservaVM actualizarReserva = AccesoBD.AD_Reserva.obtenerReservaPorId(idReserva);
            actualizarReserva.ListaInsumosEnLaReserva = AccesoBD.AD_Reserva.obtenerInsumosDeLaReserva(idReserva);

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
           
            Cancha cancha = AccesoBD.AD_Reserva.obtenerCanchaPorId(idReserva);

            foreach (var item in listaCanchas)
            {
                if (item.Value.Equals(cancha.idCancha.ToString()))
                {
                    item.Selected = true;
                    break;
                }
            }

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

            Horario horario = AccesoBD.AD_Reserva.obtenerHorarioPorId(idReserva);

            foreach (var item in listaHorarios)
            {
                if (item.Value.Equals(horario.idHorario.ToString()))
                {
                    item.Selected = true;
                    break;
                }
            }

            /*List<Insumo> insumos = AccesoBD.AD_Insumo.obtenerInsumos();
            List<SelectListItem> listaInsumos = insumos.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.insumo1 + " - ($" + d.precio + ")",
                    Value = d.idInsumo.ToString(),
                    Selected = false
                };
            });
            ViewBag.insumos = listaInsumos;*/

            ViewBag.canchas = listaCanchas;
            ViewBag.horarios = listaHorarios;
            return View(actualizarReserva);
        }
    }
}