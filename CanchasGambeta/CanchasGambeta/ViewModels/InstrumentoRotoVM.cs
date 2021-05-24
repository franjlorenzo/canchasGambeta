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
}