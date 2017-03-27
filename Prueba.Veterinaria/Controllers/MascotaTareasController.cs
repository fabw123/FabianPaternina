using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Prueba.Veterinaria.Entities;
using Prueba.Veterinaria.Models;

namespace Prueba.Veterinaria.Controllers
{
    public class MascotaTareasController : Controller
    {
        private VeterinariaContext db = new VeterinariaContext();
        private MascotaModel mascotaModel = new MascotaModel();
        private TareaModel tareaModel = new TareaModel();
        private MascotaTareaModel mascotaTareaModel = new MascotaTareaModel();

        // GET: MascotaTareas
        public ActionResult Index()
        {
            int idUsuario = (int)Session["IdUsuario"];
            var mascotasTareas = mascotaTareaModel.GetByIdUsuario(idUsuario);
            return View(mascotasTareas.ToList());
        }

        // GET: MascotaTareas/Create
        public ActionResult Crear()
        {
            int idUsuario = (int)Session["IdUsuario"];
            var mascotas = mascotaModel.GetMascotasByUsuario(idUsuario);
            var tareas = tareaModel.GetAll();
            ViewBag.IdMascota = new SelectList(mascotas, "IdMascota", "Nombre");
            ViewBag.IdTarea = new SelectList(tareas, "IdTarea", "Nombre");
            return View();
        }

        // POST: MascotaTareas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(TareaViewModel mascotaTarea)
        {
            if (ModelState.IsValid)
            {
                DateTime fechaInicio = mascotaTarea.Fecha.Date + new TimeSpan(0, 0, 0);
                DateTime fechaFin = mascotaTarea.Fecha.Date + new TimeSpan(23, 59, 59);
                var tareasDias = mascotaTareaModel.GetByIdMascota(mascotaTarea.IdMascota, fechaInicio, fechaFin);
                if (tareasDias == null || tareasDias.Count < 5)
                {
                    var _mascotaTarea = new MascotaTarea
                    {
                        Fecha = mascotaTarea.Fecha + mascotaTarea.Hora,
                        Finalizada = mascotaTarea.Finalizada,
                        IdMascota = mascotaTarea.IdMascota,
                        IdMascotaTarea = mascotaTarea.IdMascotaTarea,
                        IdTarea = mascotaTarea.IdTarea,
                    };
                    _mascotaTarea = mascotaTareaModel.Crear(_mascotaTarea);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Sólo puede asignarle 5 tareas al dia a una mascota");
                }
            }

            int idUsuario = (int)Session["IdUsuario"];
            var mascotas = mascotaModel.GetMascotasByUsuario(idUsuario);
            var tareas = tareaModel.GetAll();
            ViewBag.IdMascota = new SelectList(mascotas, "IdMascota", "Nombre");
            ViewBag.IdTarea = new SelectList(tareas, "IdTarea", "Nombre");
            return View(mascotaTarea);
        }

        // GET: MascotaTareas/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var mascotaTarea = mascotaTareaModel.GetViewModelById(id.Value);
            if (mascotaTarea == null)
            {
                return HttpNotFound();
            }
            int idUsuario = (int)Session["IdUsuario"];
            var mascotas = mascotaModel.GetMascotasByUsuario(idUsuario);
            var tareas = tareaModel.GetAll();
            ViewBag.IdMascota = new SelectList(mascotas, "IdMascota", "Nombre",mascotaTarea.IdMascota);
            ViewBag.IdTarea = new SelectList(tareas, "IdTarea", "Nombre", mascotaTarea.IdTarea);
            return View(mascotaTarea);
        }

        // POST: MascotaTareas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar( TareaViewModel mascotaTarea)
        {
            if (ModelState.IsValid)
            {
                DateTime fechaInicio = mascotaTarea.Fecha.Date + new TimeSpan(0, 0, 0);
                DateTime fechaFin = mascotaTarea.Fecha.Date + new TimeSpan(23, 59, 59);
                var tareasDias = mascotaTareaModel.GetByIdMascota(mascotaTarea.IdMascota, fechaInicio, fechaFin, mascotaTarea.IdMascotaTarea);
                if (tareasDias == null || tareasDias.Count < 5)
                {
                    var _mascotaTarea = new MascotaTarea
                    {
                        Fecha = mascotaTarea.Fecha + mascotaTarea.Hora,
                        Finalizada = mascotaTarea.Finalizada,
                        IdMascota = mascotaTarea.IdMascota,
                        IdMascotaTarea = mascotaTarea.IdMascotaTarea,
                        IdTarea = mascotaTarea.IdTarea,
                    };
                    _mascotaTarea = mascotaTareaModel.Editar(_mascotaTarea);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Sólo puede asignarle 5 tareas al dia a una mascota");
                }
            }
            int idUsuario = (int)Session["IdUsuario"];
            var mascotas = mascotaModel.GetMascotasByUsuario(idUsuario);
            var tareas = tareaModel.GetAll();
            ViewBag.IdMascota = new SelectList(mascotas, "IdMascota", "Nombre");
            ViewBag.IdTarea = new SelectList(tareas, "IdTarea", "Nombre");
            return View(mascotaTarea);
        }

        // GET: MascotaTareas/Delete/5
        public ActionResult Eliminar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var mascotaTarea = mascotaTareaModel.GetViewModelById(id.Value);
            if (mascotaTarea == null)
            {
                return HttpNotFound();
            }
            return View(mascotaTarea);
        }

        // POST: MascotaTareas/Delete/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MascotaTarea mascotaTarea = mascotaTareaModel.GetById(id);
            mascotaTareaModel.Eliminar(mascotaTarea);
            return RedirectToAction("Index");
        }
    }
}
