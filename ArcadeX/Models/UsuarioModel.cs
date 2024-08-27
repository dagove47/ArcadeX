using arcadeX.Entidades;
using arcadeX.baseDatos;
using System.Collections.Generic;
using System.Linq;
using System;

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

        public bool ExisteCorreo(string email)
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
        public bool GuardarTokenRecuperacion(string email, string token, DateTime expiracion)
        {
            using (var context = new ArcadeXEntities())
            {
                var usuario = context.Usuarios.FirstOrDefault(u => u.Email == email);
                if (usuario != null)
                {
                    usuario.TokenRecuperacion = token;
                    usuario.TokenExpiracion = expiracion;
                    return context.SaveChanges() > 0;
                }
                return false;
            }
        }

        public bool ValidarTokenRecuperacion(string token)
        {
            using (var context = new ArcadeXEntities())
            {
                var usuario = context.Usuarios.FirstOrDefault(u => u.TokenRecuperacion == token);
                return usuario != null && usuario.TokenExpiracion > DateTime.Now;
            }
        }

        public bool RestablecerContrasena(string token, string nuevaContrasena)
        {
            using (var context = new ArcadeXEntities())
            {
                var usuario = context.Usuarios.FirstOrDefault(u => u.TokenRecuperacion == token);
                if (usuario != null && usuario.TokenExpiracion > DateTime.Now)
                {
                    usuario.Contrasenna = nuevaContrasena; // Considera encriptar la contraseña aquí
                    usuario.TokenRecuperacion = null;
                    usuario.TokenExpiracion = null;
                    return context.SaveChanges() > 0;
                }
                return false;

            }
        }
    }
}
