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
    
    public partial class DetallePedido
    {
        public int idDetallePedido { get; set; }
        public int pedido { get; set; }
        public int insumo { get; set; }
        public int cantidadPedida { get; set; }
        public Nullable<int> cantidadRecibida { get; set; }
        public Nullable<System.DateTime> fechaRecibido { get; set; }
    
        public virtual Insumo Insumo1 { get; set; }
        public virtual Pedido Pedido1 { get; set; }
    }
}
