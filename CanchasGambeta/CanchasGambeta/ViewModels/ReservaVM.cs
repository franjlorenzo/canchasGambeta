using CanchasGambeta.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace CanchasGambeta.ViewModels
{
    public class NuevaReservaVM
    {
        private int idReserva;
        private int idCancha;
        private int idHorario;
        private DateTime? fecha;
        private bool servicioAsador;
        private bool servicioInstrumento;
        private List<Insumo> listaInsumos;

        public int IdReserva { get => idReserva; set => idReserva = value; }
        public int IdCancha { get => idCancha; set => idCancha = value; }
        public int IdHorario { get => idHorario; set => idHorario = value; }
        public bool ServicioAsador { get => servicioAsador; set => servicioAsador = value; }
        public bool ServicioInstrumento { get => servicioInstrumento; set => servicioInstrumento = value; }
        public List<Insumo> ListaInsumos { get => listaInsumos; set => listaInsumos = value; }
        public DateTime? Fecha { get => fecha; set => fecha = value; }
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

        public int IdReserva { get => idReserva; set => idReserva = value; }
        public int IdHorario { get => idHorario; set => idHorario = value; }
        public string Horario { get => horario; set => horario = value; }
        public int IdCancha { get => idCancha; set => idCancha = value; }
        public string Cancha { get => cancha; set => cancha = value; }
        public bool ServicioAsador { get => servicioAsador; set => servicioAsador = value; }
        public bool ServicioInstrumento { get => servicioInstrumento; set => servicioInstrumento = value; }
        public DateTime? Fecha { get => fecha; set => fecha = value; }
    }

    public class VistaReserva
    {
        public NuevaReservaVM NuevaReservaVM { get; set; }
        public List<TablaReservaVM> TablaReservaVM { get; set; }
    }
}