//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AccesoADatos
{
    using System;
    using System.Collections.Generic;
    
    public partial class Platillo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Platillo()
        {
            this.PlatilloPedidos = new HashSet<PlatilloPedido>();
            this.PlatilloIngredientes = new HashSet<PlatilloIngrediente>();
        }
    
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public System.DateTime FechaDeCreacion { get; set; }
        public System.DateTime FechaDeModificacion { get; set; }
        public bool Activo { get; set; }
        public string Codigo { get; set; }
        public string Notas { get; set; }
        public string Descripcion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlatilloPedido> PlatilloPedidos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlatilloIngrediente> PlatilloIngredientes { get; set; }
    }
}

/// CU33/CU34Visualizar lista de pedidos pendientes/ver ingredientes de pedido