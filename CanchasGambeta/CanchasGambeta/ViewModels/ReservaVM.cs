using CanchasGambeta.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CanchasGambeta.ViewModels
{
    public class NuevaReservaVM
    {
        private int idReserva;
        private int idCancha;
        private int idHorario;
        private DateTime fecha;
        private bool servicioAsador;
        private bool servicioInstrumento;
        private List<Insumo> listaInsumos = AccesoBD.AD_Insumo.obtenerInsumos();

        public int IdReserva { get => idReserva; set => idReserva = value; }
        public int IdCancha { get => idCancha; set => idCancha = value; }
        public int IdHorario { get => idHorario; set => idHorario = value; }
        public bool ServicioAsador { get => servicioAsador; set => servicioAsador = value; }
        public bool ServicioInstrumento { get => servicioInstrumento; set => servicioInstrumento = value; }
        public List<Insumo> ListaInsumos { get => listaInsumos; set => listaInsumos = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
    }

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
        private bool estado;

        public int IdReserva { get => idReserva; set => idReserva = value; }
        public int IdHorario { get => idHorario; set => idHorario = value; }
        public string Horario { get => horario; set => horario = value; }
        public int IdCancha { get => idCancha; set => idCancha = value; }
        public string Cancha { get => cancha; set => cancha = value; }
        public bool ServicioAsador { get => servicioAsador; set => servicioAsador = value; }
        public bool ServicioInstrumento { get => servicioInstrumento; set => servicioInstrumento = value; }
        public DateTime? Fecha { get => fecha; set => fecha = value; }
        public bool Estado { get => estado; set => estado = value; }
    }

    public class ActualizarReservaVM
    {
        private int idReserva;
        private int idCancha;
        private string cancha;
        private int idHorario;
        private string horario;
        private DateTime fecha;
        private bool servicioAsador;
        private bool servicioInstrumento;
        private List<Insumo> listaInsumosEnLaReserva;

        public int IdReserva { get => idReserva; set => idReserva = value; }
        public int IdCancha { get => idCancha; set => idCancha = value; }
        public string Cancha { get => cancha; set => cancha = value; }
        public int IdHorario { get => idHorario; set => idHorario = value; }
        public string Horario { get => horario; set => horario = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public bool ServicioAsador { get => servicioAsador; set => servicioAsador = value; }
        public bool ServicioInstrumento { get => servicioInstrumento; set => servicioInstrumento = value; }
        public List<Insumo> ListaInsumosEnLaReserva { get => listaInsumosEnLaReserva; set => listaInsumosEnLaReserva = value; }
    }

    public class NuevaReservaConDropDownList
    {
        public int idReserva { get; set; }
        public int idCancha { get; set; }
        public int idHorario { get; set; }
        public DateTime fecha { get; set; }
        public bool servicioAsador { get; set; }
        public bool servicioInstrumento { get; set; }
        public bool enviarMails { get; set; }

        public List<Insumo> listaInsumos = AccesoBD.AD_Insumo.obtenerInsumos();
        public List<Insumo> ListaInsumos { get => listaInsumos; set => listaInsumos = value; }

        public List<SelectListItem> Canchas { get; set; }
        public List<SelectListItem> Horarios { get; set; }

        public NuevaReservaConDropDownList()
        {
            this.Canchas = new List<SelectListItem>();
            this.Horarios = new List<SelectListItem>();
            this.fecha = new DateTime();
        }
    }

    public class DatosReserva
    {
        private DateTime fecha;
        private string tipoCancha;
        private string horario;

        public DateTime Fecha { get => fecha; set => fecha = value; }
        public string TipoCancha { get => tipoCancha; set => tipoCancha = value; }
        public string Horario { get => horario; set => horario = value; }
    }

    public class VistaReserva
    {
        public NuevaReservaVM NuevaReservaVM { get; set; }
        public List<TablaReservaVM> TablaReservaVM { get; set; }
        public NuevaReservaConDropDownList NuevaReservaConDropDownList { get; set; }
    }
}