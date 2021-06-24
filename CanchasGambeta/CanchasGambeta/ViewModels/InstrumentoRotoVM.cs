using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CanchasGambeta.ViewModels
{
    public class InstrumentoRotoVM
    {
        private int idInstrumentoRoto;
        private string instrumento;
        private DateTime fechaRotura;
        private int idInstrumentoDisponible;
        private bool estado;


        public InstrumentoRotoVM(int idInstrumentoRoto, string instrumento, DateTime fechaRotura, int idInstrumentoDisponible, bool estado)
        {
            this.idInstrumentoRoto = idInstrumentoRoto;
            this.instrumento = instrumento;
            this.fechaRotura = fechaRotura;
            this.idInstrumentoDisponible = idInstrumentoDisponible;
            this.estado = estado;
        }

        public InstrumentoRotoVM()
        {

        }

        public string Instrumento { get => instrumento; set => instrumento = value; }
        public DateTime FechaRotura { get => fechaRotura; set => fechaRotura = value; }
        public int IdInstrumentoRoto { get => idInstrumentoRoto; set => idInstrumentoRoto = value; }
        public int IdInstrumentoDisponible { get => idInstrumentoDisponible; set => idInstrumentoDisponible = value; }
        public bool Estado { get => estado; set => estado = value; }
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