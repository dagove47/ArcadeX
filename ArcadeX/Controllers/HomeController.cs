using arcadeX.baseDatos;
using arcadeX.Entidades;
using arcadeX.Models;
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


    }
}
