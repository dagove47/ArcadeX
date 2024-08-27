// Controller: RecuperarAccesoController.cs
using arcadeX.Models;
using arcadeX.Entidades;
using System;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace arcadeX.Controllers
{
    public class RecuperarAccesoController : Controller
    {
        private UsuarioModel usuarioM = new UsuarioModel();

        [HttpGet]
        public ActionResult SolicitarRecuperacion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SolicitarRecuperacion(string email)
        {
            if (usuarioM.ExisteCorreo(email))
            {
                string token = Guid.NewGuid().ToString();
                DateTime expiracion = DateTime.Now.AddHours(24);
                if (usuarioM.GuardarTokenRecuperacion(email, token, expiracion))
                {
                    EnviarCorreoRecuperacion(email, token);
                    ViewBag.msj = "Se ha enviado un correo con las instrucciones para recuperar tu contraseña.";
                }
                else
                {
                    ViewBag.msj = "Ocurrió un error al procesar tu solicitud. Por favor, intenta de nuevo más tarde.";
                }
            }
            else
            {
                ViewBag.msj = "No se encontró una cuenta asociada a este correo electrónico.";
            }
            return View();
        }

        [HttpGet]
        public ActionResult RestablecerContrasena(string token)
        {
            if (usuarioM.ValidarTokenRecuperacion(token))
            {
                return View(new Usuario { tokenRecuperacion = token });
            }
            else
            {
                ViewBag.msj = "El enlace de recuperación no es válido o ha expirado.";
                return RedirectToAction("SolicitarRecuperacion");
            }
        }

        [HttpPost]
        public ActionResult RestablecerContrasena(Usuario user)
        {
            if (ModelState.IsValid)
            {
                if (usuarioM.RestablecerContrasena(user.tokenRecuperacion, user.Contrasena))
                {
                    ViewBag.msj = "Tu contraseña ha sido actualizada exitosamente.";
                    return RedirectToAction("Iniciarsesion", "Home");
                }
                else
                {
                    ViewBag.msj = "Ocurrió un error al restablecer tu contraseña. Por favor, intenta de nuevo.";
                }
            }
            return View(user);
        }

        private void EnviarCorreoRecuperacion(string email, string token)
        {
            string cuenta = ConfigurationManager.AppSettings["CuentaCorreo"];
            string contrasenna = ConfigurationManager.AppSettings["ContrasennaCorreo"];
            string asunto = "Recuperación de contraseña - ArcadeX";
            string contenido = $"Para restablecer tu contraseña, haz clic en el siguiente enlace: {Url.Action("RestablecerContrasena", "RecuperarAcceso", new { token = token }, protocol: Request.Url.Scheme)}";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(cuenta, "ArcadeX");
            message.To.Add(new MailAddress(email));
            message.Subject = asunto;
            message.Body = contenido;
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.office365.com", 587);
            client.Credentials = new NetworkCredential(cuenta, contrasenna);
            client.EnableSsl = true;

            try
            {
                client.Send(message);
            }
            catch (SmtpException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al enviar el correo: {ex.Message}");
                // Considera manejar este error de una manera que informe al usuario
            }
        }
    }
}
