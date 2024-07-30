using arcadeX.baseDatos;
using arcadeX.Entidades;
using System;
using System.Linq;

namespace arcadeX.Models
{
    public class UsuarioModel
    {
        public bool RegistrarCliente(Usuario user)
        {
            if (ExisteCorreo(user.Email))
            {
                return false; // El correo ya existe
            }

            try
            {
                using (var context = new ArcadeXEntities())
                {
                    // Llamar al procedimiento almacenado para registrar el usuario
                    var result = context.RegistrarUsuario(user.Identificacion, user.Nombre, user.Email, user.Contrasena, user.RolID);
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

        private bool ExisteCorreo(string email)
        {
            using (var context = new ArcadeXEntities())
            {
                return context.Usuarios.Any(u => u.Email == email);
            }
        }
    }
}
