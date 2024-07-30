using arcadeX.Entidades;
using arcadeX.baseDatos;
using System.Collections.Generic;
using System.Linq;

namespace arcadeX.Models
{
    public class UsuarioModel
    {
        public bool RegistrarUsuario(Usuario user)
        {
            if (ExisteCorreo(user.Email))
            {
                return false; // El correo ya existe
            }

            var rowsAffected = 0;

            using (var context = new ArcadeXEntities())
            {
                rowsAffected = context.RegistrarUsuario(user.Identificacion, user.Nombre, user.Email, user.Contrasena, user.RolID);
            }

            return rowsAffected > 0;
        }

        private bool ExisteCorreo(string email)
        {
            using (var context = new ArcadeXEntities())
            {
                return context.Usuarios.Any(u => u.Email == email);
            }
        }

        public List<Consulta> Consultar()
        {
            using (var context = new ArcadeXEntities())
            {
                var result = context.ConsultarUsuarios().Select(u => new Consulta
                {
                    Identificacion = u.Identificacion,
                    Nombre = u.Nombre,
                    Email = u.Email,
                    UsuarioID = u.UsuarioID
                }).ToList();

                return result;
            }
        }
    }
}
