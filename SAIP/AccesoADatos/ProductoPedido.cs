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
    
    public partial class ProductoPedido
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
    
        public virtual Pedido Pedido { get; set; }
        public virtual Producto Productos { get; set; }
    }
}
