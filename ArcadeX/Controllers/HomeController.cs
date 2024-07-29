using arcadeX.Entidades;
using arcadeX.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace arcadeX.Controllers
{
    public class HomeController : Controller
    {
        ClienteModel clienteM = new ClienteModel();
        ConsolaModel consolaM = new ConsolaModel();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RegistroCliente()
        {

            return View();
        }

        [HttpPost]
        public ActionResult RegistroCliente(Cliente user)
        {
            var respuesta = clienteM.RegistrarCliente(user);
            if (respuesta)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.msj = "Su información no se ha registrado. El correo ya existe.";
                return View();
            }
        }
        [HttpGet]
        public ActionResult RegistroConsolas()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistroConsolas(Consola consola, HttpPostedFileBase Imagen)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Imagen != null && Imagen.ContentLength > 0)
                    {
                        using (var reader = new BinaryReader(Imagen.InputStream))
                        {
                            consola.Imagen = reader.ReadBytes(Imagen.ContentLength);
                        }
                    }

                    var respuesta = consolaM.RegistrarConsola(consola);
                    if (respuesta)
                    {
                        ViewBag.msj = "Consola registrada exitosamente.";
                    }
                    else
                    {
                        ViewBag.msj = "Error al registrar la consola.";
                    }
                }
                else
                {
                    ViewBag.msj = "Hay errores en el formulario.";
                }
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                ViewBag.msj = "Ocurrió un error: " + ex.Message;
            }

            return View(consola);
        }



        public ActionResult ConsultaJuegos() {
            return View();
        }

        public ActionResult Consolas() {
            return View();
        }

        public ActionResult ConsultaUsuarios() {
            return View();
        }
    }
}