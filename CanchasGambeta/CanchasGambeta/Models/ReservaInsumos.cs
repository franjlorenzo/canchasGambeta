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
    
    public partial class ReservaInsumos
    {
        public int idReservaInsumos { get; set; }
        public int reserva { get; set; }
        public int insumo { get; set; }
        public int cantidad { get; set; }
    
        public virtual Insumo Insumo1 { get; set; }
        public virtual Reserva Reserva1 { get; set; }
    }
}
