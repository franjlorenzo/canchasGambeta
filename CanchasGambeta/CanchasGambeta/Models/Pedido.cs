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
    
    public partial class Pedido
    {
        public int idPedido { get; set; }
        public int proveedor { get; set; }
        public string descripcion { get; set; }
    
        public virtual Proveedor Proveedor1 { get; set; }
    }
}
