using arcadeX.Entidades;
using arcadeX.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace arcadeX.Controllers
{
    public class HomeController : Controller
    {
        UsuarioModel usuarioM = new UsuarioModel();
    

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RegistroUsuario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistroUsuario(Usuario user)
        {
            var respuesta = usuarioM.RegistrarCliente(user);
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

   

        public ActionResult ConsultaJuegos()
        {
            return View();
        }

        public ActionResult Consolas()
        {
            return View();
        }

        public ActionResult ConsultaUsuarios()
        {
            return View();
        }
    }
}
