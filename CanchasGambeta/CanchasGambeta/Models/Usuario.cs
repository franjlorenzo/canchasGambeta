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
    
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            this.Reserva = new HashSet<Reserva>();
        }
    
        public int idUsuario { get; set; }
        public string nombreCompleto { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Nullable<int> equipo { get; set; }
        public int rol { get; set; }
    
        public virtual Equipo Equipo1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reserva> Reserva { get; set; }
        public virtual Rol Rol1 { get; set; }
    }
}
