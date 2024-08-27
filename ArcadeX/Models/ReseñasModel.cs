using arcadeX.baseDatos;
using arcadeX.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace arcadeX.Models
{
    public class ReseñasModel
    {

        public List<ObtenerReseñas_Result> ObtenerReseñas()
        {
            using (var context = new ArcadeXEntities())
            {
                return context.ObtenerReseñas().ToList();
            }
        }



        public bool InsertarReseña(string comentario)
        {
            var rowsAffected = 0;

            using (var context = new ArcadeXEntities())
            {
                rowsAffected = context.Database.ExecuteSqlCommand("InsertarReseña @Comentario",
                    new SqlParameter("Comentario", comentario));
            }

            return rowsAffected > 0;
        }

        public bool ActualizarReseña(int reseñaId, string comentario)
        {
            var rowsAffected = 0;

            using (var context = new ArcadeXEntities())
            {
                rowsAffected = context.Database.ExecuteSqlCommand("ActualizarReseña @ReseñaId, @Comentario",
                    new SqlParameter("ReseñaId", reseñaId),
                    new SqlParameter("Comentario", comentario));
            }

            return rowsAffected > 0;
        }

        public bool EliminarReseña(int reseñaId)
        {
            var rowsAffected = 0;

            using (var context = new ArcadeXEntities())
            {
                rowsAffected = context.Database.ExecuteSqlCommand("EliminarReseña @ReseñaId",
                    new SqlParameter("ReseñaId", reseñaId));
            }

            return rowsAffected > 0;
        }




    }
}