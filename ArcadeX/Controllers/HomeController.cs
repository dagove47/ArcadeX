using arcadeX.baseDatos;
using arcadeX.Entidades;
using arcadeX.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace arcadeX.Controllers
{
    public class HomeController : Controller
    {
        UsuarioModel usuarioM = new UsuarioModel(); // Instancia correcta
        ConsolaModel consolaM = new ConsolaModel(); // Instancia correcta

        
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult ConsultaJuegos()
        {
            return View();
        }

        public ActionResult Consolas()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ConsultaUsuarios()
        {
            var result = usuarioM.Consultar();
            return View(result);
        }


        [HttpGet]
        public ActionResult RegistroConsolas()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistroConsolas(Consola consola)
        {
            var respuesta = consolaM.RegistrarConsola(consola);

            if (respuesta)
                return RedirectToAction("Index", "Home");
            else
            {
                ViewBag.msj = "Su información ya existe en nuestro sistema";
                return View();
            }
        }
        [HttpGet]
        public ActionResult MostrarConsolas()
        {
            var result = consolaM.ConsultarConsolas();
            return View(result);
        }

        [HttpGet]
        public ActionResult Iniciarsesion()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Iniciarsesion(Usuario user)

        {
            var respuesta = usuarioM.IniciarSesion(user);

            if (respuesta != null)
            {
                //if (respuesta.EsClaveTemporal == true && respuesta.ClaveVencimiento <= DateTime.Now)
                //{
                //    ViewBag.msj = "Su contraseña temporal ha caducado";
                //    return View();
                //}

                Session["NombreUsuario"] = respuesta.Nombre;
                Session["EmailUsuario"] = respuesta.Email;
                Session["RolIdUsuario"] = respuesta.RolID.ToString();
                Session["IdentificacionUsuario"] = respuesta.Identificacion;
                return RedirectToAction("HomePage", "Store");
            }
            else
            {
                ViewBag.msj = "Su información no es correcta";
                return View();
            }
        }
        [HttpGet]
        public ActionResult Contacto()
        {
            var result = consolaM.ConsultarConsolas();
            return View(result);
        }


    }


}
