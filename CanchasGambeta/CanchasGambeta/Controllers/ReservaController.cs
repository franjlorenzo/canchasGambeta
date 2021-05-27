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

            Canchas_GambetaEntities3 db = new Canchas_GambetaEntities3();
            NuevaReservaConDropDownList modelo = new NuevaReservaConDropDownList();
            foreach (var cancha in db.Cancha)
            {
                modelo.Canchas.Add(new SelectListItem { Text = cancha.tipoCancha, Value = cancha.idCancha.ToString() });
            }
            modelo.fecha = DateTime.Today;
            return View(new VistaReserva { NuevaReservaConDropDownList = modelo, TablaReservaVM = AccesoBD.AD_Reserva.obtenerReservasDelCliente() });
        }

        [HttpPost]       
        public ActionResult MisReservas(NuevaReservaConDropDownList nuevaReserva, List<int> cantidad)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");
            nuevaReserva.fecha = DateTime.Parse(Request["NuevaReservaConDropDownList.fecha"]);
            int idCanchaElegida = int.Parse(Request["idCanchaElegida"]);
            DateTime fechaElegida = DateTime.Parse(Request["fechaElegida"]);

            try
            {
                /*Solo se cambia la fecha, cancha no seleccionada. Se devuelve la vista con la fecha cambiada y con viewbag actualizado*/
                if(nuevaReserva.fecha != fechaElegida && nuevaReserva.idCancha == idCanchaElegida && nuevaReserva.idCancha == 0)
                {
                    Canchas_GambetaEntities3 db = new Canchas_GambetaEntities3();
                    NuevaReservaConDropDownList modelo = new NuevaReservaConDropDownList();
                    foreach (var cancha in db.Cancha)
                    {
                        modelo.Canchas.Add(new SelectListItem { Text = cancha.tipoCancha, Value = cancha.idCancha.ToString() });
                    }
                    modelo.fecha = nuevaReserva.fecha;
                    ViewBag.fechaElegida = nuevaReserva.fecha;
                    return View(new VistaReserva { NuevaReservaConDropDownList = modelo, TablaReservaVM = AccesoBD.AD_Reserva.obtenerReservasDelCliente() });
                }
                /*Solo se cambia la cancha, pero antes no habia cancha seleccionada. Se devuelven los horarios disponibles de esa cancha en ese dia*/
                if(nuevaReserva.fecha == fechaElegida && nuevaReserva.idCancha != idCanchaElegida && idCanchaElegida == 0)
                {
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
                    ViewBag.idCanchaElegida = nuevaReserva.idCancha;
                    ViewBag.fechaElegida = nuevaReserva.fecha;
                    return View(new VistaReserva { NuevaReservaConDropDownList = nuevaReserva, TablaReservaVM = AccesoBD.AD_Reserva.obtenerReservasDelCliente() });
                }
                /*Se cambia la fecha y hay una cancha seleccionada anteriormente. Se devuelven los horarios actualizados para esa fecha*/
                if(nuevaReserva.fecha != fechaElegida && nuevaReserva.idCancha == idCanchaElegida && nuevaReserva.idCancha != 0)
                {
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
                    ViewBag.fechaElegida = nuevaReserva.fecha;
                    ViewBag.idCanchaElegida = nuevaReserva.idCancha;
                    return View(new VistaReserva { NuevaReservaConDropDownList = nuevaReserva, TablaReservaVM = AccesoBD.AD_Reserva.obtenerReservasDelCliente() });
                }
                /*Se cambia la cancha y ya se estaban mostrando los horarios. Se devuelven los horarios actualizados para esa cancha*/
                if(nuevaReserva.fecha == fechaElegida && nuevaReserva.idCancha != idCanchaElegida && idCanchaElegida != 0)
                {
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
                    ViewBag.idCanchaElegida = nuevaReserva.idCancha;
                    return View(new VistaReserva { NuevaReservaConDropDownList = nuevaReserva, TablaReservaVM = AccesoBD.AD_Reserva.obtenerReservasDelCliente() });
                }
                /*La fecha y cancha coinciden con sus viewbags y el horario es seleccionado. Se hace el insert en la BD*/
                if (nuevaReserva.fecha == fechaElegida && nuevaReserva.idCancha == idCanchaElegida && nuevaReserva.idHorario != 0)
                {
                    nuevaReserva.servicioAsador = bool.Parse(Request["NuevaReservaConDropDownList.servicioAsador"].Contains("true").ToString());
                    nuevaReserva.servicioInstrumento = bool.Parse(Request.Form["NuevaReservaConDropDownList.servicioInstrumento"].Contains("true").ToString());

                    bool insertReserva = AccesoBD.AD_Reserva.nuevaReserva(nuevaReserva);
                    if (insertReserva)
                    {
                        nuevaReserva.idReserva = AccesoBD.AD_Reserva.obtenerReservaPorAtributos(nuevaReserva.idCancha, nuevaReserva.idHorario, nuevaReserva.fecha);
                        bool insertReservaInsumo = AccesoBD.AD_Reserva.insertReservaInsumo(nuevaReserva, cantidad);
                        bool insertHorarioReservas = AccesoBD.AD_Reserva.insertHorarioReservas(nuevaReserva.idReserva, nuevaReserva.idHorario);
                        if (insertReservaInsumo && insertHorarioReservas)
                        {
                            bool enviarMails = bool.Parse(Request["NuevaReservaConDropDownList.enviarMails"].Contains("true").ToString());
                            if (enviarMails)
                            {
                                var smtpClient = new SmtpClient("smtp.gmail.com")
                                {
                                    Port = 587,
                                    Credentials = new NetworkCredential("canchasgambeta@gmail.com", "Canchasgambetafl1997"),
                                    EnableSsl = true,
                                };

                                DatosReserva reservaCliente = AccesoBD.AD_Reserva.obtenerDatosReserva(nuevaReserva.idReserva);
                                List<MailEquipoVM> listaIntegrantesEquipo = AccesoBD.AD_Equipo.obtenerMailsEquipo(sesion.equipo);
                                string mensaje = "Hola! su compañero de equipo hizo una reserva para el día " + reservaCliente.Fecha + " a las " + reservaCliente.Horario + " en la cancha " + reservaCliente.TipoCancha;
                                
                                foreach (var lista in listaIntegrantesEquipo)
                                {
                                    smtpClient.Send("canchasgambeta@gmail.com", lista.Email, "Nueva reserva!", mensaje);
                                }
                            }
                            return RedirectToAction("MisReservas", "Reserva");
                        }
                    }
                    else
                    {
                        ViewBag.ErrorInsertReserva = "Ocurrió un error al registrar la reserva, inténtelo nuevamente.";
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
                        return View(new VistaReserva { NuevaReservaConDropDownList = nuevaReserva, TablaReservaVM = AccesoBD.AD_Reserva.obtenerReservasDelCliente() });
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex;
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