using CanchasGambeta.Models;
using CanchasGambeta.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization; //Esto para parse de la fecha por parametro al registrar una reserva
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
            DateTime fechaNull = new DateTime(0001, 01, 01);
            ViewBag.fechaReserva = fechaNull;
            return View(new VistaReserva { NuevaReservaConDropDownList = modelo, TablaReservaVM = AccesoBD.AD_Reserva.obtenerReservasDelCliente() });
        }

        [HttpPost]       
        public ActionResult MisReservas(NuevaReservaConDropDownList nuevaReserva, List<int> cantidad, string fecha)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");
            DateTime fechaNull = new DateTime(0001, 01, 01);
            DateTime fechaReserva = DateTime.Parse(Request["fechaReservaElegida"]);
            int canchaSeleccionada = 0;

            DateTime fechaElegida = DateTime.ParseExact(fecha, "d/M/yyyy", CultureInfo.CurrentCulture);

            try
            {
                //Se elije la fecha de la reserva, no se elige la cancha entonces se devuelve la vista sin modificaciones
                if (!nuevaReserva.fecha.Equals(fechaNull) && nuevaReserva.idCancha != canchaSeleccionada)
                {

                }
                if (!nuevaReserva.fecha.Equals(fechaNull) && nuevaReserva.idCancha == canchaSeleccionada)
                {
                    fechaReserva = nuevaReserva.fecha;
                    ViewBag.fechaReserva = fechaReserva;
                    Canchas_GambetaEntities3 db = new Canchas_GambetaEntities3();
                    NuevaReservaConDropDownList modelo = new NuevaReservaConDropDownList();
                    foreach (var cancha in db.Cancha)
                    {
                        modelo.Canchas.Add(new SelectListItem { Text = cancha.tipoCancha, Value = cancha.idCancha.ToString() });
                    }
                    return View(new VistaReserva { NuevaReservaConDropDownList = modelo, TablaReservaVM = AccesoBD.AD_Reserva.obtenerReservasDelCliente() });
                }

                //Se elije la fecha de la reserva, la cancha y se devuelven los horarios disponibles
                if (!fechaElegida.Equals(fechaNull) && fechaReserva.Equals(fechaNull)) //cuando elijo horario entra aca y no deberia
                {
                    ViewBag.fechaReserva = fechaElegida; //CAMBIADO, va fechaReserva
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
                    nuevaReserva.Horarios = AccesoBD.AD_Reserva.obtenerHorariosCancha(fechaReserva, nuevaReserva.idCancha);
                    return View(new VistaReserva { NuevaReservaConDropDownList = nuevaReserva, TablaReservaVM = AccesoBD.AD_Reserva.obtenerReservasDelCliente() });
                }

                //Se elije el horario, no se cambia la fecha elegida o se elige la misma y se hace el insert de la reserva y los insumos
                if ((nuevaReserva.fecha.Equals(fechaNull) && !fechaReserva.Equals(fechaNull)) || (nuevaReserva.fecha.Equals(fechaReserva) && !fechaReserva.Equals(fechaNull)))
                {
                    nuevaReserva.fecha = fechaReserva;
                    nuevaReserva.idHorario = int.Parse(Request["NuevaReservaConDropDownList.Horarios"]);
                    nuevaReserva.servicioAsador = bool.Parse(Request["NuevaReservaConDropDownList.servicioAsador"].Contains("true").ToString());
                    nuevaReserva.servicioInstrumento = bool.Parse(Request.Form["NuevaReservaConDropDownList.servicioInstrumento"].Contains("true").ToString());

                    bool insertExitoso = AccesoBD.AD_Reserva.nuevaReserva(nuevaReserva);
                    if (insertExitoso)
                    {
                        bool insertReservaInsumo = AccesoBD.AD_Reserva.insertReservaInsumo(nuevaReserva, AccesoBD.AD_Reserva.obtenerReservaPorAtributos(nuevaReserva.idCancha, nuevaReserva.idHorario, nuevaReserva.fecha), cantidad);
                        if (insertReservaInsumo) return RedirectToAction("MisReservas", "Reserva");
                    }
                    else
                    {
                        ViewBag.ErrorInsertReserva = "Ocurrió un error al registrar la reserva. Intenteló nuevamente.";
                        Canchas_GambetaEntities3 db = new Canchas_GambetaEntities3();
                        NuevaReservaConDropDownList modelo = new NuevaReservaConDropDownList();
                        foreach (var cancha in db.Cancha)
                        {
                            modelo.Canchas.Add(new SelectListItem { Text = cancha.tipoCancha, Value = cancha.idCancha.ToString() });
                        }
                        ViewBag.fechaReserva = fechaNull;
                        return View(new VistaReserva { NuevaReservaConDropDownList = modelo, TablaReservaVM = AccesoBD.AD_Reserva.obtenerReservasDelCliente() });
                    }
                }

                //No se elige una fecha, se elige la cancha entonces se muestra mensaje para que se seleccione una fecha
                if (nuevaReserva.fecha.Equals(fechaNull) && fechaReserva.Equals(fechaNull))
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

        //PRUEBA MAILS
        [HttpPost]
        public ActionResult EnviarMail()
        {

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("canchasgambeta@gmail.com", "Canchasgambetafl1997"),
                EnableSsl = true,
            };

            smtpClient.Send("canchasgambeta@gmail.com", "lorenzofran1@gmail.com", "Prueba", "Esto te lo mando con C#");
            return RedirectToAction("IndexCliente", "Cliente");
        }
    }
}