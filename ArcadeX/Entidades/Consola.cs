using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arcadeX.Entidades
{
    public class Consola
    {
        public String Nombre { get; set; }

        public String Fabricante { get; set; }

        public DateTime FechaLanzamiento { get; set; }

        public decimal Precio { get; set; }

        public String URLImagen { get; set; }
    }
}