//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CanchasGambeta.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EquipoMails
    {
        public int idEquipoMails { get; set; }
        public int email { get; set; }
        public int equipo { get; set; }
    
        public virtual Email Email1 { get; set; }
        public virtual Equipo Equipo1 { get; set; }
    }
}