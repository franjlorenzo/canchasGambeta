using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CanchasGambeta.Models
{
    public class Cliente
    {
        private int idCliente;
        [Required]
        private string nombreCompleto;
        [Required]
        private string email;
        [Required]
        private int telefono;
        [Required]
        private string password;
        private int equipo;

        public int IdCliente { get => idCliente; set => idCliente = value; }
        public string NombreCompleto { get => nombreCompleto; set => nombreCompleto = value; }
        public string Email { get => email; set => email = value; }
        public int Telefono { get => telefono; set => telefono = value; }
        public string Password { get => password; set => password = value; }
        public int Equipo { get => equipo; set => equipo = value; }
    }
}