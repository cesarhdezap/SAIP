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
    
    public partial class IngredienteIngrediente
    {
        public int Id { get; set; }
        public double Cantidad { get; set; }
    
        public virtual Ingrediente IngredienteCompuesto { get; set; }
        public virtual Ingrediente IngredienteComponente { get; set; }
    }
}