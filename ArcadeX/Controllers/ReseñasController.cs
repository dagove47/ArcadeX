using arcadeX.Entidades;
using arcadeX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace arcadeX.Controllers
{
    public class ReseñasController : Controller
    {
        ReseñasModel reseñasM = new ReseñasModel(); // Instancia correcta

        // Acción para obtener todas las reseñas
        [HttpGet]
        public ActionResult ObtenerReseñas()
        {
            var respuesta = reseñasM.ObtenerReseñas();
            return View(respuesta);
        }

        // Acción para mostrar el formulario de inserción de una nueva reseña
        [HttpGet]
        public ActionResult InsertarReseñas()
        {
            return View();
        }

        // Acción para procesar el formulario de inserción de una nueva reseña
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertarReseñas(Reseñas model)
        {
            if (ModelState.IsValid)
            {
                reseñasM.InsertarReseña(model.Comentario);
                return RedirectToAction("ObtenerReseñas");
            }
            return View(model);
        }

        // Acción para mostrar el formulario de actualización de una reseña existente
        [HttpGet]
        public ActionResult ActualizarReseñas(int ReseñaId)
        {
            var reseña = reseñasM.ObtenerReseñas().FirstOrDefault(r => r.ReseñaId == ReseñaId);
            if (reseña == null)
            {
                return HttpNotFound();
            }
            return View(reseña);
        }

        // Acción para procesar el formulario de actualización de una reseña existente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarReseñas(Reseñas model)
        {
            if (ModelState.IsValid)
            {
                reseñasM.ActualizarReseña(model.ReseñaId, model.Comentario);
                return RedirectToAction("ObtenerReseñas");
            }
            return View(model);
        }

        // Acción para eliminar una reseña
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarReseñas(int ReseñaId)
        {
            reseñasM.EliminarReseña(ReseñaId);
            return RedirectToAction("ObtenerReseñas");
        }



    }
}