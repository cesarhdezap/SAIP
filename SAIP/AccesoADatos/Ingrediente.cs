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
    
    public partial class Ingrediente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ingrediente()
        {
            this.PlatilloIngredientes = new HashSet<PlatilloIngrediente>();
            this.RelacionIngredientesHijo = new HashSet<RelacionIngrediente>();
        }
    
        public int Id { get; set; }
        public short UnidadDeMedida { get; set; }
        public string Nombre { get; set; }
        public double CantidadEnInventario { get; set; }
        public string CodigoDeBarras { get; set; }
        public double Costo { get; set; }
        public System.DateTime FechaDeCreacion { get; set; }
        public System.DateTime FechaDeModiciacion { get; set; }
        public string NombreCreador { get; set; }
        public bool Activo { get; set; }
        public string Codigo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlatilloIngrediente> PlatilloIngredientes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RelacionIngrediente> RelacionIngredientesHijo { get; set; }
    }
}
