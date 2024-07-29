using arcadeX.baseDatos;
using arcadeX.Entidades;
using System;
using System.Linq;

namespace arcadeX.Models
{
    public class ConsolaModel
    {
        public bool RegistrarConsola(Consola consola)
        {
            if (consola == null)
                throw new ArgumentNullException(nameof(consola));

            try
            {
                using (var context = new ArcadeXEntities())
                {
                    // Asegúrate de que el archivo de imagen no sea nulo antes de pasarlo al procedimiento almacenado
                    var imagenParam = consola.Imagen ?? new byte[0];

                    var result = context.sp_RegistrarConsola(
                        consola.Nombre,
                        consola.Marca,
                        consola.FechaLanzamiento,
                        consola.Precio,
                        imagenParam
                    );

                    // Devolvemos true si el resultado es mayor a 0, indicando éxito
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                // Manejar el error, podrías registrar el error en un archivo de log o base de datos
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
