using CanchasGambeta.Models;
using CanchasGambeta.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;

namespace CanchasGambeta.Controllers
{
    public class ReservaController : Controller
    {
        // GET: Reserva
        public ActionResult MisReservas()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            Canchas_GambetaEntities db = new Canchas_GambetaEntities();
            NuevaReservaVM modelo = new NuevaReservaVM();
            foreach (var cancha in db.Cancha)
            {
                modelo.Canchas.Add(new SelectListItem { Text = cancha.tipoCancha, Value = cancha.idCancha.ToString() });
            }
            modelo.fecha = DateTime.Today;
            if (TempData["ErrorModificarReserva"] != null) ViewBag.ErrorModificarReserva = TempData["ErrorModificarReserva"].ToString();
            return View(new VistaReserva { NuevaReservaVM = modelo, TablaReservaVM = AccesoBD.AD_Reserva.obtenerReservasDelCliente() });
        }

        [HttpPost]       
        public ActionResult MisReservas(NuevaReservaVM nuevaReserva, List<int> cantidad)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");
            nuevaReserva.fecha = DateTime.Parse(Request["NuevaReservaVM.fecha"]);
            int idCanchaElegida = int.Parse(Request["idCanchaElegida"]);
            DateTime fechaElegida = DateTime.Parse(Request["fechaElegida"]);

            try
            {
                /*Solo se cambia la fecha, cancha no seleccionada. Se devuelve la vista con la fecha cambiada y con viewbag actualizado*/
                if(nuevaReserva.fecha != fechaElegida && nuevaReserva.idCancha == idCanchaElegida && nuevaReserva.idCancha == 0)
                {
                    Canchas_GambetaEntities db = new Canchas_GambetaEntities();
                    NuevaReservaVM modelo = new NuevaReservaVM();
                    foreach (var cancha in db.Cancha)
                    {
                        modelo.Canchas.Add(new SelectListItem { Text = cancha.tipoCancha, Value = cancha.idCancha.ToString() });
                    }
                    modelo.fecha = nuevaReserva.fecha;
                    ViewBag.fechaElegida = nuevaReserva.fecha;
                    return View(new VistaReserva { NuevaReservaVM = modelo, TablaReservaVM = AccesoBD.AD_Reserva.obtenerReservasDelCliente() });
                }
                /*Solo se cambia la cancha, pero antes no habia cancha seleccionada. Se devuelven los horarios disponibles de esa cancha en ese dia*/
                if(nuevaReserva.fecha == fechaElegida && nuevaReserva.idCancha != idCanchaElegida && idCanchaElegida == 0)
                {
                    Canchas_GambetaEntities db = new Canchas_GambetaEntities();
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
                    ViewBag.idCanchaElegida = nuevaReserva.idCancha;
                    ViewBag.fechaElegida = nuevaReserva.fecha;
                    return View(new VistaReserva { NuevaReservaVM = nuevaReserva, TablaReservaVM = AccesoBD.AD_Reserva.obtenerReservasDelCliente() });
                }
                /*Se cambia la fecha y hay una cancha seleccionada anteriormente. Se devuelven los horarios actualizados para esa fecha*/
                if(nuevaReserva.fecha != fechaElegida && nuevaReserva.idCancha == idCanchaElegida && nuevaReserva.idCancha != 0)
                {
                    Canchas_GambetaEntities db = new Canchas_GambetaEntities();
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
                    ViewBag.fechaElegida = nuevaReserva.fecha;
                    ViewBag.idCanchaElegida = nuevaReserva.idCancha;
                    return View(new VistaReserva { NuevaReservaVM = nuevaReserva, TablaReservaVM = AccesoBD.AD_Reserva.obtenerReservasDelCliente() });
                }
                /*Se cambia la cancha y ya se estaban mostrando los horarios. Se devuelven los horarios actualizados para esa cancha*/
                if(nuevaReserva.fecha == fechaElegida && nuevaReserva.idCancha != idCanchaElegida && idCanchaElegida != 0)
                {
                    Canchas_GambetaEntities db = new Canchas_GambetaEntities();
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
                    ViewBag.idCanchaElegida = nuevaReserva.idCancha;
                    return View(new VistaReserva { NuevaReservaVM = nuevaReserva, TablaReservaVM = AccesoBD.AD_Reserva.obtenerReservasDelCliente() });
                }
                /*La fecha y cancha coinciden con sus viewbags y el horario es seleccionado. Se hace el insert en la BD*/
                if (nuevaReserva.fecha == fechaElegida && nuevaReserva.idCancha == idCanchaElegida && nuevaReserva.idHorario != 0)
                {
                    nuevaReserva.servicioAsador = bool.Parse(Request["NuevaReservaVM.servicioAsador"].Contains("true").ToString());
                    nuevaReserva.servicioInstrumento = bool.Parse(Request.Form["NuevaReservaVM.servicioInstrumento"].Contains("true").ToString());

                    bool insertReserva = AccesoBD.AD_Reserva.nuevaReserva(nuevaReserva);
                    if (insertReserva)
                    {
                        nuevaReserva.idReserva = AccesoBD.AD_Reserva.obtenerReservaPorAtributos(nuevaReserva.idCancha, nuevaReserva.idHorario, nuevaReserva.fecha);
                        bool insertReservaInsumo = AccesoBD.AD_Reserva.insertReservaInsumo(nuevaReserva, cantidad);
                        bool insertHorarioReservas = AccesoBD.AD_Reserva.insertHorarioReservas(nuevaReserva.idReserva, nuevaReserva.idHorario);

                        if (insertReservaInsumo && insertHorarioReservas && sesion.equipo != null)
                        {
                            bool enviarMails = bool.Parse(Request["NuevaReservaVM.enviarMails"].Contains("true").ToString());
                            if(enviarMails) AccesoBD.AD_Reserva.enviarMailsReserva(nuevaReserva.idReserva, 1);
                        }
                        return RedirectToAction("MisReservas", "Reserva");
                    }
                    else
                    {
                        ViewBag.ErrorInsertReserva = "Ocurrió un error al registrar la reserva, inténtelo nuevamente.";
                        Canchas_GambetaEntities db = new Canchas_GambetaEntities();
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
                        foreach (var horario in db.Horario)
                        {
                            nuevaReserva.Horarios.Add(new SelectListItem { Text = horario.horario1, Value = horario.idHorario.ToString() });
                        }
                        foreach (var item in nuevaReserva.Horarios)
                        {
                            if (item.Value.Equals(nuevaReserva.idHorario.ToString()))
                            {
                                item.Selected = true;
                                break;
                            }
                        }
                        return View(new VistaReserva { NuevaReservaVM = nuevaReserva, TablaReservaVM = AccesoBD.AD_Reserva.obtenerReservasDelCliente() });
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex;
                Canchas_GambetaEntities db = new Canchas_GambetaEntities();
                NuevaReservaVM modelo = new NuevaReservaVM();
                foreach (var cancha in db.Cancha)
                {
                    modelo.Canchas.Add(new SelectListItem { Text = cancha.tipoCancha, Value = cancha.idCancha.ToString() });
                }
                return View(new VistaReserva { NuevaReservaVM = modelo, TablaReservaVM = AccesoBD.AD_Reserva.obtenerReservasDelCliente() });
            }

            return View();
        }

        public ActionResult ModificarReserva(int idReserva)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            ActualizarReservaVM actualizarReserva = AccesoBD.AD_Reserva.obtenerReservaPorId(idReserva);
            actualizarReserva.ListaInsumosEnLaReserva = AccesoBD.AD_Reserva.obtenerInsumosDeLaReserva(idReserva);

            Canchas_GambetaEntities db = new Canchas_GambetaEntities();

            foreach (var canchas in db.Cancha)
            {
                actualizarReserva.Canchas.Add(new SelectListItem { Text = canchas.tipoCancha, Value = canchas.idCancha.ToString() });
            }
            foreach (var item in actualizarReserva.Canchas)
            {
                if (item.Value.Equals(actualizarReserva.IdCancha.ToString()))
                {
                    item.Selected = true;
                    break;
                }
            }

            actualizarReserva.Horarios = AccesoBD.AD_Reserva.obtenerHorariosCancha(actualizarReserva.Fecha, actualizarReserva.IdCancha);

            ViewBag.idCanchaReserva = actualizarReserva.IdCancha;
            ViewBag.fechaReserva = actualizarReserva.Fecha;
            ViewBag.idHorarioReserva = actualizarReserva.IdHorario;
            if (TempData["sinModificacion"] != null) ViewBag.sinModificacion = TempData["sinModificacion"].ToString();
            return View(actualizarReserva);
        }

        [HttpPost]
        public ActionResult ModificarReserva(ActualizarReservaVM actualizarReserva, List<int> listaInsumosActualizados, List<int> listaidInsumos)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            actualizarReserva.ListaInsumosEnLaReserva = AccesoBD.AD_Reserva.obtenerInsumosDeLaReserva(actualizarReserva.IdReserva);
            DateTime fechaReserva = DateTime.Parse(Request["fechaReserva"]);
            int idCanchaReserva = int.Parse(Request["idCanchaReserva"]);
            int idHorarioReserva = 0;
            if (!Request["idHorarioReserva"].Equals(null)) idHorarioReserva = int.Parse(Request["idHorarioReserva"]);
                  
            try
            {
                /*El usuario cambió la fecha, se devuelven los horarios y los viewbag actualizados*/
                if ((actualizarReserva.Fecha != fechaReserva && idHorarioReserva == actualizarReserva.IdHorario) || (actualizarReserva.Fecha != fechaReserva && idHorarioReserva != actualizarReserva.IdHorario))
                {
                    Canchas_GambetaEntities db = new Canchas_GambetaEntities();
                    foreach (var cancha in db.Cancha)
                    {
                        actualizarReserva.Canchas.Add(new SelectListItem { Text = cancha.tipoCancha, Value = cancha.idCancha.ToString() });
                    }
                    foreach (var item in actualizarReserva.Canchas)
                    {
                        if (item.Value.Equals(actualizarReserva.IdCancha.ToString()))
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                    actualizarReserva.Horarios = AccesoBD.AD_Reserva.obtenerHorariosCancha(actualizarReserva.Fecha, actualizarReserva.IdCancha);
                    ViewBag.idCanchaReserva = actualizarReserva.IdCancha;
                    ViewBag.fechaReserva = actualizarReserva.Fecha;
                    return View(actualizarReserva);
                }
                /*El usuario cambió la cancha, se devuelven los horarios y los viewbag actualizados*/
                if((actualizarReserva.IdCancha != idCanchaReserva && actualizarReserva.IdHorario == idHorarioReserva) || (actualizarReserva.IdCancha != idCanchaReserva && actualizarReserva.IdHorario != idHorarioReserva))
                {
                    Canchas_GambetaEntities db = new Canchas_GambetaEntities();
                    foreach (var cancha in db.Cancha)
                    {
                        actualizarReserva.Canchas.Add(new SelectListItem { Text = cancha.tipoCancha, Value = cancha.idCancha.ToString() });
                    }
                    foreach (var item in actualizarReserva.Canchas)
                    {
                        if (item.Value.Equals(actualizarReserva.IdCancha.ToString()))
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                    actualizarReserva.Horarios = AccesoBD.AD_Reserva.obtenerHorariosCancha(actualizarReserva.Fecha, actualizarReserva.IdCancha);
                    ViewBag.idCanchaReserva = actualizarReserva.IdCancha;
                    ViewBag.fechaReserva = actualizarReserva.Fecha;
                    return View(actualizarReserva);
                }
                /*El usuario cambió algo de la reserva y luego no seleccionó un horario*/
                if(actualizarReserva.Fecha == fechaReserva && actualizarReserva.IdCancha == idCanchaReserva && actualizarReserva.IdHorario == idHorarioReserva && actualizarReserva.IdHorario == 0)
                {
                    Canchas_GambetaEntities db = new Canchas_GambetaEntities();
                    foreach (var cancha in db.Cancha)
                    {
                        actualizarReserva.Canchas.Add(new SelectListItem { Text = cancha.tipoCancha, Value = cancha.idCancha.ToString() });
                    }
                    foreach (var item in actualizarReserva.Canchas)
                    {
                        if (item.Value.Equals(actualizarReserva.IdCancha.ToString()))
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                    actualizarReserva.Horarios = AccesoBD.AD_Reserva.obtenerHorariosCancha(actualizarReserva.Fecha, actualizarReserva.IdCancha);
                    ViewBag.idCanchaReserva = actualizarReserva.IdCancha;
                    ViewBag.fechaReserva = actualizarReserva.Fecha;
                    ViewBag.sinHorario = "No seleccionó un horario para la reserva.";
                    return View(actualizarReserva);
                }
                /*El usuario no cambió fecha, cancha u horario de la reserva y presiona Modificar reserva. Se verifica que no haya modificado los servicios o insumos*/
                if (actualizarReserva.Fecha == fechaReserva && actualizarReserva.IdCancha == idCanchaReserva && actualizarReserva.IdHorario == idHorarioReserva && actualizarReserva.IdHorario != 0)
                {
                    //Obtengo los datos de la reserva antes de que sea modificada
                    ActualizarReservaVM reservaSinModificacion = AccesoBD.AD_Reserva.obtenerReservaPorId(actualizarReserva.IdReserva);
                    reservaSinModificacion.ListaInsumosEnLaReserva = AccesoBD.AD_Reserva.obtenerInsumosDeLaReserva(actualizarReserva.IdReserva);

                    //Cargo la nueva lista de insumos en la reserva modificada
                    actualizarReserva.ListaInsumosEnLaReserva = AccesoBD.AD_Reserva.armarListaInsumos(listaInsumosActualizados, listaidInsumos);

                    //Comparo las 2 listas para ver si algun insumo se modificó
                    bool seActualizoInsumo = AccesoBD.AD_Reserva.seActualizoInsumo(actualizarReserva.ListaInsumosEnLaReserva, reservaSinModificacion.ListaInsumosEnLaReserva);

                    if(seActualizoInsumo || reservaSinModificacion.ServicioAsador != actualizarReserva.ServicioAsador || reservaSinModificacion.ServicioInstrumento != actualizarReserva.ServicioInstrumento)
                    {
                        if (ModelState.IsValid)
                        {
                            bool resultado = AccesoBD.AD_Reserva.modificarReserva(actualizarReserva, listaInsumosActualizados);
                            if (resultado && sesion.equipo != null)
                            {
                                bool enviarMails = bool.Parse(Request["EnviarMails"].Contains("true").ToString());
                                if (enviarMails) AccesoBD.AD_Reserva.enviarMailsReserva(actualizarReserva.IdReserva, 3);
                                return RedirectToAction("MisReservas", "Reserva");
                            }
                            else
                            {
                                TempData["ErrorModificarReserva"] = "Ocurrió un error al actualizar la reserva, por favor inténtelo nuevamente.";
                                return RedirectToAction("MisReservas", "Reserva");
                            }
                        }
                    }
                    else //No se actualizó ningun atributo de la reserva, se redirecciona a la reserva con mensaje
                    {
                        TempData["sinModificacion"] = "No modificó su reserva. Presione cancelar para volver a sus reservas";
                        return RedirectToAction("ModificarReserva", new { idReserva = actualizarReserva.IdReserva });
                    }
                }
                /*El usuario selecciona un horario y presiona Modificar reserva, se hace update de la misma con los insumos*/
                if (actualizarReserva.Fecha == fechaReserva && actualizarReserva.IdCancha == idCanchaReserva && actualizarReserva.IdHorario != idHorarioReserva)
                {
                    if (ModelState.IsValid)
                    {
                        bool resultado = AccesoBD.AD_Reserva.modificarReserva(actualizarReserva, listaInsumosActualizados);
                        if (resultado && sesion.equipo != null)
                        {
                            bool enviarMails = bool.Parse(Request["EnviarMails"].Contains("true").ToString());
                            if (enviarMails) AccesoBD.AD_Reserva.enviarMailsReserva(actualizarReserva.IdReserva, 2);
                            return RedirectToAction("MisReservas", "Reserva");
                        }
                        else
                        {
                            TempData["ErrorModificarReserva"] = "Ocurrió un error al actualizar la reserva, por favor inténtelo nuevamente.";
                            return RedirectToAction("MisReservas", "Reserva");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Canchas_GambetaEntities db = new Canchas_GambetaEntities();
                foreach (var canchas in db.Cancha)
                {
                    actualizarReserva.Canchas.Add(new SelectListItem { Text = canchas.tipoCancha, Value = canchas.idCancha.ToString() });
                }
                foreach (var item in actualizarReserva.Canchas)
                {
                    if (item.Value.Equals(actualizarReserva.IdCancha.ToString()))
                    {
                        item.Selected = true;
                        break;
                    }
                }

                ViewBag.Error = ex;
                ViewBag.idCanchaReserva = actualizarReserva.IdCancha;
                ViewBag.fechaReserva = actualizarReserva.Fecha;
                ViewBag.idHorarioReserva = actualizarReserva.IdHorario;
                return View(actualizarReserva);
            }
            
            return View(actualizarReserva);
        }

        [HttpPost]
        public ActionResult EliminarReserva()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            int idReserva = int.Parse(Request["listado.IdReserva"]);
            DatosReserva reservaCliente = AccesoBD.AD_Reserva.obtenerDatosReserva(idReserva);//En caso de que el usuario tenga un equipo, se recupera la info de la reserva antes de que se borre

            if (ModelState.IsValid)
            {
                bool resultado = AccesoBD.AD_Reserva.eliminarReserva(idReserva);
                if (resultado)
                {
                    if(sesion.equipo != null) AccesoBD.AD_Reserva.enviarMailsReserva(idReserva, 0, reservaCliente);
                    return RedirectToAction("MisReservas", "Reserva");
                }
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