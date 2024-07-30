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

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RegistroUsuario()
        {
            // Cargar roles desde la base de datos y pasarlos a la vista
            using (var context = new ArcadeXEntities())
            {
                ViewBag.Roles = context.Roles.Select(r => new { r.RolID, r.Nombre }).ToList();
            }
            return View();
        }

        [HttpPost]
        public ActionResult RegistroUsuario(Usuario user)
        {
            // Cargar roles desde la base de datos y pasarlos a la vista en caso de error
            using (var context = new ArcadeXEntities())
            {
                ViewBag.Roles = context.Roles.Select(r => new { r.RolID, r.Nombre }).ToList();
            }

            var respuesta = usuarioM.RegistrarUsuario(user);
            if (respuesta)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.msj = "Su información no se ha registrado. La cédula ya existe.";
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

        [HttpGet]
        public ActionResult ConsultaUsuarios()
        {
            var result = usuarioM.Consultar();
            return View(result);
        }
    }
}
