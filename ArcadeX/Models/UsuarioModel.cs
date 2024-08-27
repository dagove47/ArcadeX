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

        public IniciarSesion_Result IniciarSesion(Usuario user)
        {
            using (var context = new ArcadeXEntities())
            {
                
                var usuario = (from x in context.Usuarios
                               where x.Email == user.Email
                               && x.Contrasenna == user.Contrasena
                               select x).FirstOrDefault();

                // Aquí debes determinar cómo quieres manejar el resultado

                if (usuario != null)
                {
                    // Si el usuario se encuentra, devolvemos la información necesaria
                    return new IniciarSesion_Result
                    {
                        Nombre = usuario.Nombre,
                        Identificacion = usuario.Identificacion,
                        Email = usuario.Email,
                        RolID = usuario.RolID,
                        // Inicializa otras propiedades según sea necesario
                    };
                }
                else
                {
                    // Si no se encuentra el usuario, devolvemos null
                    return null;
                }
            }
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
