using CanchasGambeta.Models;
using System;
using System.Collections.Generic;

namespace CanchasGambeta.ViewModels
{
    public class ReservasActivas
    {
        private int idReserva;
        private string nombreCompleto;
        private string tipoCancha;
        private string horario;
        private DateTime fecha;
        private bool servicioAsador;
        private bool servicioInstrumento;
        private List<Insumo> listaInsumosReserva;

        public string NombreCompleto { get => nombreCompleto; set => nombreCompleto = value; }
        public string TipoCancha { get => tipoCancha; set => tipoCancha = value; }
        public string Horario { get => horario; set => horario = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public bool ServicioAsador { get => servicioAsador; set => servicioAsador = value; }
        public bool ServicioInstrumento { get => servicioInstrumento; set => servicioInstrumento = value; }
        public int IdReserva { get => idReserva; set => idReserva = value; }
        public List<Insumo> ListaInsumosReserva { get => listaInsumosReserva; set => listaInsumosReserva = value; }
    }

    public class ReservasCanceladas
    {
        private string nombreCompleto;
        private string tipoCancha;
        private string horario;
        private DateTime fecha;
        private bool servicioAsador;
        private bool servicioInstrumento;

        public string NombreCompleto { get => nombreCompleto; set => nombreCompleto = value; }
        public string TipoCancha { get => tipoCancha; set => tipoCancha = value; }
        public string Horario { get => horario; set => horario = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public bool ServicioAsador { get => servicioAsador; set => servicioAsador = value; }
        public bool ServicioInstrumento { get => servicioInstrumento; set => servicioInstrumento = value; }
    }

    public class VistaReservasActivas
    {
        public List<ReservasActivas> ReservasActivas { get; set; }
        public List<Insumo> TotalCadaInsumo { get; set; }
    }
}