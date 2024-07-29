using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arcadeX.Entidades
{
    public class Consola
    {
        public string Nombre { get; set; } // Nombre de la consola
        public string Marca { get; set; } // Marca de la consola
        public DateTime FechaLanzamiento { get; set; } // Fecha de lanzamiento de la consola
        public decimal Precio { get; set; } // Precio de la consola
        public byte[] Imagen { get; set; } // Imagen de la consola (almacenada como array de bytes)

    }
}