using Prueba.Veterinaria.Entities;
using Prueba.Veterinaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prueba.Veterinaria.Controllers
{
    public class AccountController : Controller
    {

        UsuarioModel usuarioModel;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                usuarioModel = new UsuarioModel();
                int IdUsuario = usuarioModel.Validar(model);
                if (IdUsuario > 0)
                {
                    Session["IdUsuario"] = IdUsuario;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Intento de inicio de sesión no válido.");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        public ActionResult Register(LoginViewModel model)
        {

            return View();

        }
    }
}