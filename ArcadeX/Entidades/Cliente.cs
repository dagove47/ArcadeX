using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArcadeX.Entidades
{
    public class Cliente
    {
        public String Identificacion { get; set; }
        public String Nombre { get; set; }
        public String Email { get; set; }
        public String Contrasenna { get; set; }
        public int RolID { get; set; }


    }
}