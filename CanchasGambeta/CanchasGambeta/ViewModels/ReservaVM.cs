using CanchasGambeta.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CanchasGambeta.ViewModels
{
    public class TablaReservaVM
    {
        private int idReserva;
        private DateTime? fecha;
        private int idHorario;
        private string horario;
        private int idCancha;
        private string cancha;
        private bool servicioAsador;
        private bool servicioInstrumento;
        private int estado;
        private List<Insumo> listaInsumosEnReserva;
        private string nombreCompleto;

        public int IdReserva { get => idReserva; set => idReserva = value; }
        public int IdHorario { get => idHorario; set => idHorario = value; }
        public string Horario { get => horario; set => horario = value; }
        public int IdCancha { get => idCancha; set => idCancha = value; }
        public string Cancha { get => cancha; set => cancha = value; }
        public bool ServicioAsador { get => servicioAsador; set => servicioAsador = value; }
        public bool ServicioInstrumento { get => servicioInstrumento; set => servicioInstrumento = value; }
        public DateTime? Fecha { get => fecha; set => fecha = value; }
        public int Estado { get => estado; set => estado = value; }
        public List<Insumo> ListaInsumosEnReserva { get => listaInsumosEnReserva; set => listaInsumosEnReserva = value; }
        public string NombreCompleto { get => nombreCompleto; set => nombreCompleto = value; }
    }

    public class ActualizarReservaVM
    {
        private int idReserva;
        private int idCancha;
        private string tipoCancha;
        private int idHorario;
        private string horario;
        private DateTime fecha;
        private bool servicioAsador;
        private bool servicioInstrumento;
        private bool enviarMails;
        private string nombreCliente;
        private List<InsumosAPedir> listaInsumosEnLaReserva;
        public List<SelectListItem> Canchas { get; set; }
        public List<SelectListItem> Horarios { get; set; }

        public ActualizarReservaVM()
        {
            this.Canchas = new List<SelectListItem>();
            this.Horarios = new List<SelectListItem>();
            this.fecha = new DateTime();
        }

        public ActualizarReservaVM(int idReserva, List<InsumosAPedir> listaInsumosEnLaReserva)
        {
            this.idReserva = idReserva;
            this.ListaInsumosEnLaReserva = listaInsumosEnLaReserva;
        }

        public int IdReserva { get => idReserva; set => idReserva = value; }
        public int IdCancha { get => idCancha; set => idCancha = value; }
        public int IdHorario { get => idHorario; set => idHorario = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public bool ServicioAsador { get => servicioAsador; set => servicioAsador = value; }
        public bool ServicioInstrumento { get => servicioInstrumento; set => servicioInstrumento = value; }
        public List<InsumosAPedir> ListaInsumosEnLaReserva { get => listaInsumosEnLaReserva; set => listaInsumosEnLaReserva = value; }
        public bool EnviarMails { get => enviarMails; set => enviarMails = value; }
        public string Horario { get => horario; set => horario = value; }
        public string NombreCliente { get => nombreCliente; set => nombreCliente = value; }
        public string TipoCancha { get => tipoCancha; set => tipoCancha = value; }
    }

    public class NuevaReservaVM
    {
        public int idReserva { get; set; }
        public int idCancha { get; set; }
        public int idHorario { get; set; }
        public DateTime fecha { get; set; }
        public bool servicioAsador { get; set; }
        public bool servicioInstrumento { get; set; }
        public bool enviarMails { get; set; }

        public List<Insumo> listaInsumos;
        public List<Insumo> ListaInsumos { get => listaInsumos; set => listaInsumos = value; }

        public List<SelectListItem> Canchas { get; set; }
        public List<SelectListItem> Horarios { get; set; }

        public NuevaReservaVM()
        {
            this.Canchas = new List<SelectListItem>();
            this.Horarios = new List<SelectListItem>();
            this.fecha = new DateTime();
        }

        public NuevaReservaVM(int idReserva)
        {
            this.idReserva = idReserva;
        }
    }

    public class DatosReserva
    {
        private DateTime fecha;
        private string tipoCancha;
        private string horario;
        private bool servicioAsador;
        private bool servicioInstrumentos;

        public DateTime Fecha { get => fecha; set => fecha = value; }
        public string TipoCancha { get => tipoCancha; set => tipoCancha = value; }
        public string Horario { get => horario; set => horario = value; }
        public bool ServicioAsador { get => servicioAsador; set => servicioAsador = value; }
        public bool ServicioInstrumentos { get => servicioInstrumentos; set => servicioInstrumentos = value; }
    }

    public class VistaReserva
    {
        public List<TablaReservaVM> TablaReservaVM { get; set; }
        public NuevaReservaVM NuevaReservaVM { get; set; }
    }

    public class VistaReservarInsumos
    {
        public NuevaReservaVM NuevaReservaVM { get; set; }
        public List<BuscarInsumos> BuscarInsumos { get; set; }
        public List<InsumosAPedir> InsumosAPedir { get; set; }
    }

    public class VistaModificarInsumosReserva
    {
        public List<BuscarInsumos> BuscarInsumos { get; set; }
        public List<InsumosAPedir> InsumosAPedir { get; set; }
    }
}