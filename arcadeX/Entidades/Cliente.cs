using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arcadeX.Entidades
{
    public class Cliente
    {
        public String Identificacion { get; set; }
        public String Nombre { get; set; }
        public String Correo { get; set; }
        public String Contrasena { get; set; }
        public int RolID { get; set; }
    }
}