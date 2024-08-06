using arcadeX.Entidades;
using arcadeX.baseDatos;
using System.Collections.Generic;
using System.Linq;
using System;

namespace arcadeX.Models
{
    public class ConsolaModel
    {
        public bool RegistrarConsola(Consola consola)
        {
            var rowsAffected = 0;

            using (var context = new ArcadeXEntities())
            {
                // Llamar al procedimiento almacenado para registrar la consola
                rowsAffected = context.RegistrarConsola(consola.Nombre, consola.Fabricante, consola.FechaLanzamiento, consola.Precio, consola.URLImagen);
            }

            return rowsAffected > 0;
        }


        public List<ConsultaConsolas> ConsultarConsolas()
        {
            using (var context = new ArcadeXEntities())
            {
                // Llamar al procedimiento almacenado para consultar consolas
                var result = context.ConsultarConsolas().Select(u => new ConsultaConsolas
                {
                    Nombre = u.Nombre,
                    Fabricante = u.Fabricante,
                    FechaLanzamiento = u.FechaLanzamiento ?? DateTime.MinValue, // Manejar valor nulo
                    Precio = u.Precio ?? 0, // Manejar valor nulo
                    URLImagen = u.URLImagen
                }).ToList();

                return result;
            }
        }
    }
}
