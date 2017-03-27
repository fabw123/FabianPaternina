using Prueba.Veterinaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prueba.Veterinaria.Controllers
{
    public class HomeController : Controller
    {
        private HomeViewModel homeModel = new HomeViewModel();
        public ActionResult Index(string sortOrder)
        {
             
            int idUsuario = (int)Session["IdUsuario"];
            var homeViewModels = homeModel.GetHomeViewModels(idUsuario);
            var CostoDia = homeModel.CostoTotalDia(idUsuario);
            ViewBag.CostoTotalDia = CostoDia;


            ViewBag.OrderRaza = String.IsNullOrEmpty(sortOrder) ? "raza_desc" : "";
            ViewBag.OrderMascota = sortOrder == "mascota" ? "mascota_desc" : "mascota";
            ViewBag.OrderApodo = sortOrder == "apodo" ? "apodo_desc" : "apodo";
            ViewBag.OrderCosto = sortOrder == "costo" ? "costo_desc" : "costo";
            ViewBag.OrderFecha = sortOrder == "fecha" ? "fecha_desc" : "fecha";
            ViewBag.OrderPendiente = sortOrder == "pendiente" ? "pendiente_desc" : "pendiente";
            ViewBag.OrderPorcentaje = sortOrder == "porcentaje" ? "porcentaje_desc" : "porcentaje";


            switch (sortOrder)
            {
                case "raza_desc":
                    homeViewModels = homeViewModels.OrderByDescending(x => x.Raza).ToList();
                    break;
                case "mascota":
                    homeViewModels = homeViewModels.OrderBy(x => x.Mascota).ToList();
                    break;
                case "mascota_desc":
                    homeViewModels = homeViewModels.OrderByDescending(x => x.Mascota).ToList();
                    break;
                case "apodo":
                    homeViewModels = homeViewModels.OrderBy(x => x.Apodo).ToList();
                    break;
                case "apodo_desc":
                    homeViewModels = homeViewModels.OrderByDescending(x => x.Apodo).ToList();
                    break;
                case "costo":
                    homeViewModels = homeViewModels.OrderBy(x => x.CostoTotal).ToList();
                    break;
                case "costo_desc":
                    homeViewModels = homeViewModels.OrderByDescending(x => x.CostoTotal).ToList();
                    break;
                case "fecha":
                    homeViewModels = homeViewModels.OrderBy(x => x.ProximaTarea).ToList();
                    break;
                case "fecha_desc":
                    homeViewModels = homeViewModels.OrderByDescending(x => x.ProximaTarea).ToList();
                    break;
                case "pendiente":
                    homeViewModels = homeViewModels.OrderBy(x => x.Pendientes).ToList();
                    break;
                case "pendiente_desc":
                    homeViewModels = homeViewModels.OrderByDescending(x => x.Pendientes).ToList();
                    break;
                case "porcentaje":
                    homeViewModels = homeViewModels.OrderBy(x => x.PorcentajeRealizadas).ToList();
                    break;
                case "porcentaje_desc":
                    homeViewModels = homeViewModels.OrderByDescending(x => x.PorcentajeRealizadas).ToList();
                    break;
                default:
                    homeViewModels = homeViewModels.OrderBy(x => x.Raza).ToList();
                    break;
            }
            return View(homeViewModels);
        }

        public ActionResult Perfil()
        {
            int id = (int)Session["IdUsuario"];
             return RedirectToAction("Editar", "Usuarios", new { id = (int)Session["IdUsuario"] });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}