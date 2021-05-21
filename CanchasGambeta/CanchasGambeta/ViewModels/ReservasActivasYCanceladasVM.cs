using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CanchasGambeta.ViewModels
{
    public class ReservasActivas
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

    public class VistaReservasActivasYCanceladas
    {
        public List<ReservasActivas> listaReservasActivas { get; set; }
        public List<ReservasCanceladas> listaReservasCanceladas { get; set; }
    }
}