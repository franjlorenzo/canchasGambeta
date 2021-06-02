using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CanchasGambeta.ViewModels
{
    public class InstrumentoRotoVM
    {
        private int idInstrumento;
        private string instrumento;
        private DateTime fechaRotura;

        public string Instrumento { get => instrumento; set => instrumento = value; }
        public DateTime FechaRotura { get => fechaRotura; set => fechaRotura = value; }
        public int IdInstrumento { get => idInstrumento; set => idInstrumento = value; }
    }

    public class InstrumentoDisponible
    {
        private int idInstrumento;
        private string instrumento;
        private DateTime fechaCompra;

        public int IdInstrumento { get => idInstrumento; set => idInstrumento = value; }
        public string Instrumento { get => instrumento; set => instrumento = value; }
        public DateTime FechaCompra { get => fechaCompra; set => fechaCompra = value; }
    }

    public class VistaInstrumentos
    {
        public List<InstrumentoRotoVM> TablaInstrumentosRotos { get; set; }
        public List<InstrumentoDisponible> TablaInstrumentosDisponibles { get; set; }
        public InstrumentoDisponible InstrumentoNuevo { get; set; }
    }
}