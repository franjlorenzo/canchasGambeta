using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CanchasGambeta.ViewModels
{
    public class HorariosMasReservados
    {
        private string horario;
        private int cantidad;

        public string Horario { get => horario; set => horario = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
    }
}