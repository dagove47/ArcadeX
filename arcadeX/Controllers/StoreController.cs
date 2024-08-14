using arcadeX.baseDatos;
using arcadeX.Entidades;
using arcadeX.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System;
using System.Linq;
using System.Web;

namespace arcadeX.Controllers
{
    public class StoreController : Controller
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        UsuarioModel usuarioM = new UsuarioModel(); // Instancia correcta

        [HttpGet]
        public async Task<ActionResult> HomePage()
        {
            // Fetch the game data from the external API
            var games = await GetGamesAsync();

            // Pass the games data to the view
            return View(games);
        }

        private async Task<List<Game>> GetGamesAsync()
        {
            var response = await _httpClient.GetAsync("https://www.freetogame.com/api/games");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var games = JsonConvert.DeserializeObject<List<Game>>(jsonString);

            return games;
        }

        [HttpGet]
        public ActionResult Game()
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
    }
}
