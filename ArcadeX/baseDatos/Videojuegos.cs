//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace arcadeX.baseDatos
{
    using System;
    using System.Collections.Generic;
    
    public partial class Videojuegos
    {
        public int VideojuegoID { get; set; }
        public string Titulo { get; set; }
        public string Desarrollador { get; set; }
        public Nullable<System.DateTime> FechaLanzamiento { get; set; }
        public string Genero { get; set; }
        public Nullable<decimal> Precio { get; set; }
        public int ConsolaID { get; set; }
    
        public virtual Consolas Consolas { get; set; }
    }
}
