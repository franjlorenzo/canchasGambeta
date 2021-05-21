using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CanchasGambeta.ViewModels
{
    public class ClientesConMasReservas
    {
        private string nombreCompleto;
        private string email;
        private string telefono;
        private int cantidadReservas;

        public string NombreCompleto { get => nombreCompleto; set => nombreCompleto = value; }
        public string Email { get => email; set => email = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public int CantidadReservas { get => cantidadReservas; set => cantidadReservas = value; }
    }
}