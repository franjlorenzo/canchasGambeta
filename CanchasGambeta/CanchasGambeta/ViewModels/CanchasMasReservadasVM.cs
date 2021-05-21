using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CanchasGambeta.ViewModels
{
    public class CanchasMasReservadasVM
    {
        private string cancha;
        private int cantidad;

        public string Cancha { get => cancha; set => cancha = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
    }
}