using arcadeX.baseDatos;
using arcadeX.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arcadeX.Models
{
    public class ClienteModel
    {
        public bool RegistrarCliente(Cliente user)
        {
            if (ExisteCorreo(user.Correo))
            {
                return false; // el correo ya existe
            }

            try
            {
                using (var context = new ArcadeXEntities())
                {
                    var result = context.sp_RegistrarUsuario(user.Identificacion, user.Nombre, user.Correo, user.Contrasena, user.RolID);
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                // Manejar el error si es necesario
                // Podrías registrar el error en la base de datos o en un archivo de log
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private bool ExisteCorreo(string correo)
        {
            using (var context = new ArcadeXEntities())
            {
                return context.Usuarios.Any(c => c.Correo == correo);
            }
        }
    }
}

