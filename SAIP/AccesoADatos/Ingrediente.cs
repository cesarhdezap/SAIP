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
            this.AlimentoIngrediente = new HashSet<AlimentoIngrediente>();
            this.IngredienteIngredientes = new HashSet<IngredienteIngrediente>();
            this.IngredienteIngredientes1 = new HashSet<IngredienteIngrediente>();
        }
    
        public int Id { get; set; }
        public string UnidadDeMedida { get; set; }
        public string Nombre { get; set; }
        public double CantidadEnInventario { get; set; }
        public string CodigoDeBarras { get; set; }
        public double Costo { get; set; }
        public System.DateTime FechaDeCreacion { get; set; }
        public System.DateTime FechaDeModiciacion { get; set; }
        public string Creador { get; set; }
        public byte[] Activo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlimentoIngrediente> AlimentoIngrediente { get; set; }
        public virtual Producto Producto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IngredienteIngrediente> IngredienteIngredientes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IngredienteIngrediente> IngredienteIngredientes1 { get; set; }
    }
}
