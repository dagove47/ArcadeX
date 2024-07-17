using ArcadeX.BaseDatos;
using ArcadeX.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace ArcadeX.Models
{
    public class ClienteModel
    {
        public bool RegistrarUsuario(Cliente user)
        {
            var rowsAffected = 0;

            using (var context = new ArcadeXEntities1())
            {
                rowsAffected = context.RegistrarCliente(user.Identificacion, user.Nombre, user.Email, user.Contrasenna,user.RolID);
            }

            return (rowsAffected > 0 ? true : false);
        }

        public IniciarSesion_Result IniciarSesion(Cliente user)
        {
         

            using (var context = new ArcadeXEntities1())
            {
                return context.IniciarSesion(user.Email, user.Contrasenna).FirstOrDefault();
            }
        }

    } }