using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CanchasGambeta.ViewModels
{
    public class MailEquipoVM
    {
        public int idEquipo;
        public string nombreEquipo;
        public int idEmail;
        public string email;

        public int IdEquipo { get => idEquipo; set => idEquipo = value; }
        public string NombreEquipo { get => nombreEquipo; set => nombreEquipo = value; }
        public int IdEmail { get => idEmail; set => idEmail = value; }
        public string Email { get => email; set => email = value; }
    }
}