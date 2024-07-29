﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace arcadeX.baseDatos
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ArcadeXEntities : DbContext
    {
        public ArcadeXEntities()
            : base("name=ArcadeXEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Consolas> Consolas { get; set; }
        public virtual DbSet<Errores> Errores { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
    
        public virtual int sp_RegistrarUsuario(string identificacion, string nombre, string correo, string contrasena, Nullable<int> rolID)
        {
            var identificacionParameter = identificacion != null ?
                new ObjectParameter("Identificacion", identificacion) :
                new ObjectParameter("Identificacion", typeof(string));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var correoParameter = correo != null ?
                new ObjectParameter("Correo", correo) :
                new ObjectParameter("Correo", typeof(string));
    
            var contrasenaParameter = contrasena != null ?
                new ObjectParameter("Contrasena", contrasena) :
                new ObjectParameter("Contrasena", typeof(string));
    
            var rolIDParameter = rolID.HasValue ?
                new ObjectParameter("RolID", rolID) :
                new ObjectParameter("RolID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_RegistrarUsuario", identificacionParameter, nombreParameter, correoParameter, contrasenaParameter, rolIDParameter);
        }
    
        public virtual int sp_RegistrarConsola(string nombre, string marca, Nullable<System.DateTime> fechaLanzamiento, Nullable<decimal> precio, byte[] imagen)
        {
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var marcaParameter = marca != null ?
                new ObjectParameter("Marca", marca) :
                new ObjectParameter("Marca", typeof(string));
    
            var fechaLanzamientoParameter = fechaLanzamiento.HasValue ?
                new ObjectParameter("FechaLanzamiento", fechaLanzamiento) :
                new ObjectParameter("FechaLanzamiento", typeof(System.DateTime));
    
            var precioParameter = precio.HasValue ?
                new ObjectParameter("Precio", precio) :
                new ObjectParameter("Precio", typeof(decimal));
    
            var imagenParameter = imagen != null ?
                new ObjectParameter("Imagen", imagen) :
                new ObjectParameter("Imagen", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_RegistrarConsola", nombreParameter, marcaParameter, fechaLanzamientoParameter, precioParameter, imagenParameter);
        }
    }
}