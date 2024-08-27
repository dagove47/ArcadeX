using arcadeX.baseDatos;
using arcadeX.Entidades;
using System;
using System.Linq;

namespace arcadeX.Models
{
    public class accesoModel
    {
        public bool ExisteCorreo(string email)
        {
            using (var context = new ArcadeXEntities())
            {
                return context.Usuarios.Any(u => u.Email == email);
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
                    context.SaveChanges();
                    return true;
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
                    usuario.Contrasenna = nuevaContrasena;
                    usuario.TokenRecuperacion = null;
                    usuario.TokenExpiracion = null;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}