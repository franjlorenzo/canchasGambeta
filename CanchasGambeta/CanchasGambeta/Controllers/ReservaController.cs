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

            Canchas_GambetaEntities3 db = new Canchas_GambetaEntities3();
            NuevaReservaConDropDownList modelo = new NuevaReservaConDropDownList();
            foreach (var cancha in db.Cancha)
            {
                modelo.Canchas.Add(new SelectListItem { Text = cancha.tipoCancha, Value = cancha.idCancha.ToString() });
            }
            DateTime fechaNull = new DateTime(0001, 01, 01);
            ViewBag.fechaReserva = fechaNull;
            return View(new VistaReserva { NuevaReservaConDropDownList = modelo, TablaReservaVM = AccesoBD.AD_Reserva.obtenerReservasDelCliente() });
        }

        [HttpPost]       
        public ActionResult MisReservas(NuevaReservaConDropDownList nuevaReserva, List<int> cantidad)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");
            DateTime fechaNull = new DateTime(0001, 01, 01);
            DateTime fechaReserva = DateTime.Parse(Request["fechaReservaElegida"]);
            bool fechaSeleccionada = false;

            try
            {
                if (nuevaReserva.fecha.Equals(fechaNull) && fechaReserva.Equals(fechaNull))
                {

                }
                if (fechaSeleccionada) nuevaReserva.fecha = DateTime.Parse(Request["fecha"]);               
                if (!fechaReserva.Equals(fechaNull) && fechaReserva == nuevaReserva.fecha)
                {
                    nuevaReserva.fecha = fechaReserva;
                    nuevaReserva.servicioAsador = bool.Parse(Request["NuevaReservaVM.ServicioAsador"].Contains("true").ToString());
                    nuevaReserva.servicioInstrumento = bool.Parse(Request.Form["NuevaReservaVM.ServicioInstrumento"].Contains("true").ToString());

                    if (ModelState.IsValid)
                    {
                        bool insertExitoso = AccesoBD.AD_Reserva.nuevaReserva(nuevaReserva);
                        if (insertExitoso)
                        {
                            bool insertReservaInsumo = AccesoBD.AD_Reserva.insertReservaInsumo(nuevaReserva, AccesoBD.AD_Reserva.obtenerReservaPorAtributos(nuevaReserva.idCancha, nuevaReserva.idHorario, nuevaReserva.fecha), cantidad);
                            if (insertReservaInsumo) return RedirectToAction("MisReservas", "Reserva");
                        }
                        else
                        {
                            ViewBag.ErrorInsertReserva = "Ocurrió un error al registrar la reserva. Intenteló nuevamente.";
                            return RedirectToAction("MisReservas", "Reserva");
                        }
                    }
                }
                else
                {
                    fechaSeleccionada = true;
                    fechaReserva = nuevaReserva.fecha;
                    ViewBag.fechaReserva = fechaReserva;
                    Canchas_GambetaEntities3 db = new Canchas_GambetaEntities3();
                    foreach (var cancha in db.Cancha)
                    {
                        nuevaReserva.Canchas.Add(new SelectListItem { Text = cancha.tipoCancha, Value = cancha.idCancha.ToString() });
                    }
                    foreach (var item in nuevaReserva.Canchas)
                    {
                        if (item.Value.Equals(nuevaReserva.idCancha.ToString()))
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                    nuevaReserva.Horarios = AccesoBD.AD_Reserva.obtenerHorariosCancha(nuevaReserva.fecha, nuevaReserva.idCancha);
                    return View(new VistaReserva { NuevaReservaConDropDownList = nuevaReserva, TablaReservaVM = AccesoBD.AD_Reserva.obtenerReservasDelCliente() });
                }
            }
            catch (Exception)
            {
                ViewBag.fechaReserva = fechaNull;
                ViewBag.SeleccionarFecha = "Debe seleccionar una fecha primero.";
                Canchas_GambetaEntities3 db = new Canchas_GambetaEntities3();
                NuevaReservaConDropDownList modelo = new NuevaReservaConDropDownList();
                foreach (var cancha in db.Cancha)
                {
                    modelo.Canchas.Add(new SelectListItem { Text = cancha.tipoCancha, Value = cancha.idCancha.ToString() });
                }
                return View(new VistaReserva { NuevaReservaConDropDownList = modelo, TablaReservaVM = AccesoBD.AD_Reserva.obtenerReservasDelCliente() });
            }                   
            return View();
        }
        
        
        
        /*public ActionResult NuevaReserva(NuevaReservaVM nuevaReserva, List<int> cantidad)
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
                    if (insertReservaInsumo) return RedirectToAction("MisReservas", "Reserva");
                }
                else
                {
                    ViewBag.ErrorInsertReserva = "Ocurrió un error al registrar la reserva. Intenteló nuevamente.";
                    return RedirectToAction("MisReservas", "Reserva");
                }
            }
            return View(nuevaReserva);
        }*/

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

            ViewBag.canchas = listaCanchas;
            ViewBag.horarios = listaHorarios;
            return View(actualizarReserva);
        }

        [HttpPost]
        public ActionResult ModificarReserva(ActualizarReservaVM actualizarReservaVM, List<int> cantidad, List<int> idInsumo)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            actualizarReservaVM.ListaInsumosEnLaReserva = AccesoBD.AD_Reserva.obtenerInsumosDeLaReserva(actualizarReservaVM.IdReserva);
            DateTime fechaNull = new DateTime(0001, 01, 01);
            DateTime fechaReserva = DateTime.Parse(Request["fechaReserva"]);
            if (actualizarReservaVM.Fecha.Equals(fechaNull)) actualizarReservaVM.Fecha = fechaReserva;
           
            try
            {
                if (ModelState.IsValid)
                {
                    bool resultado = AccesoBD.AD_Reserva.modificarReserva(actualizarReservaVM, cantidad, idInsumo);
                    if (resultado) return RedirectToAction("MisReservas", "Reserva");
                    else
                    {
                        ViewBag.ErrorModificarReserva = "Ocurrió un error al actualizar la reserva, por favor intenteló nuevamente más tarde.";
                        return View(actualizarReservaVM);
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
            return View(actualizarReservaVM);
        }

        [HttpPost]
        public ActionResult EliminarReserva()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            int idReserva = int.Parse(Request["listado.IdReserva"]);

            if (ModelState.IsValid)
            {
                bool resultado = AccesoBD.AD_Reserva.eliminarReserva(idReserva);
                if (resultado) return RedirectToAction("MisReservas", "Reserva");
                else
                {
                    ViewBag.ErrorEliminarReserva = "Ocurrió un error al eliminar la reserva. Intenteló nuevamente.";
                    return RedirectToAction("MisReservas", "Reserva");
                }
            }
            return View();
        }
    }
}