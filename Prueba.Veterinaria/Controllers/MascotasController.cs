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
using System.IO;

namespace Prueba.Veterinaria.Controllers
{
    public class MascotasController : Controller
    {
        private VeterinariaContext db = new VeterinariaContext();
        private MascotaModel mascotaModel = new MascotaModel();
        private MascotaTareaModel mascotaTareaModel = new MascotaTareaModel();
        private RazaModel razaModel = new RazaModel();
        private SexoModel sexoModel = new SexoModel();

        // GET: Mascotas
        public ActionResult Index()
        {
            int idUsuario = (int)Session["IdUsuario"];
            var mascotas = mascotaModel.GetMascotasByUsuario(idUsuario);
            return View(mascotas);
        }

        // GET: Mascotas/Details/5
        public ActionResult Detalles(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var mascota = mascotaModel.GetMascotaById(id.Value);
            DateTime fechaInicio = DateTime.Now;
            DateTime fechaFin = DateTime.MaxValue;
            var tareas = mascotaTareaModel.GetByIdMascota(id.Value, fechaInicio, fechaFin);
            ViewBag.Tareas = tareas;
            if (mascota == null)
            {
                return HttpNotFound();
            }
            return View(mascota);
        }

        // GET: Mascotas/Create
        public ActionResult Crear()
        {
            var razas = razaModel.GetRazas();
            var sexos = sexoModel.GetSexos();
            ViewBag.IdRaza = new SelectList(razas, "IdRaza", "Nombre");
            ViewBag.IdSexo = new SelectList(sexos, "IdSexo", "Nombre");
            return View();
        }

        // POST: Mascotas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear( MascotaViewModel model)
        {
            if (ModelState.IsValid)
            {
                int idUsuario = (int)Session["IdUsuario"];
                Mascota mascota = new Mascota();
                mascota.Apodo = model.Apodo;
                mascota.FechaNacimiento = model.FechaNacimiento;
                mascota.Habilitado = true;
                mascota.IdUsuario = idUsuario;
                mascota.IdMascota = model.IdMascota;
                mascota.IdRaza = model.IdRaza;
                mascota.IdSexo = model.IdSexo;
                mascota.Nombre = model.Nombre;
                mascota = mascotaModel.Crear(mascota);

                if (model.Foto != null)
                {
                    if (!FileHelper.MimeTypeExtensiones.Any(x => x.Key == model.Foto.ContentType))
                    {
                        ModelState.AddModelError("", "La extensión de archivo de imagen del usuario no es permitida");
                        return View("Editar", model);
                    }

                    var fileName = Path.GetFileName(model.Foto.FileName);
                    var name = string.Format("{0}.{1}", mascota.IdMascota, fileName.Split('.')[1]);

                    var uploadFilesDir = System.Web.HttpContext.Current.Server.MapPath("~/FotoMascotas/");
                    if (!Directory.Exists(uploadFilesDir))
                    {
                        Directory.CreateDirectory(uploadFilesDir);
                    }
                    var fileSavePath = Path.Combine(uploadFilesDir, name);

                    // Save the uploaded file to "UploadedFiles" folder
                    model.Foto.SaveAs(fileSavePath);
                }


                return RedirectToAction("Index");
            }
            var razas = razaModel.GetRazas();
            var sexos = sexoModel.GetSexos();
            ViewBag.IdRaza = new SelectList(razas, "IdRaza", "Nombre");
            ViewBag.IdSexo = new SelectList(sexos, "IdSexo", "Nombre");
            return View(model);
        }

        // GET: Mascotas/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mascota mascota = mascotaModel.GetMascotaById(id.Value);
            var razas = razaModel.GetRazas();
            var sexos = sexoModel.GetSexos();
            if (mascota == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdRaza = new SelectList(razas, "IdRaza", "Nombre", mascota.IdRaza);
            ViewBag.IdSexo = new SelectList(sexos, "IdSexo", "Nombre", mascota.IdSexo);
            return View(mascota);
        }

        // POST: Mascotas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "IdMascota,Nombre,Apodo,FechaNacimiento,IdUsuario,IdRaza,IdSexo,Habilitado")] Mascota mascota)
        {
            if (ModelState.IsValid)
            {
                mascotaModel.Editar(mascota);
                return RedirectToAction("Index");
            }
            var razas = razaModel.GetRazas();
            var sexos = sexoModel.GetSexos();
            ViewBag.IdRaza = new SelectList(razas, "IdRaza", "Nombre", mascota.IdRaza);
            ViewBag.IdSexo = new SelectList(sexos, "IdSexo", "Nombre", mascota.IdSexo);
            return View(mascota);
        }

        // GET: Mascotas/Delete/5
        public ActionResult Eliminar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mascota mascota = mascotaModel.GetMascotaById(id.Value);
            if (mascota == null)
            {
                return HttpNotFound();
            }
            return View(mascota);
        }

        // POST: Mascotas/Delete/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mascota mascota = mascotaModel.GetMascotaById(id);
            mascota.Habilitado = false;
            mascotaModel.Editar(mascota);
            return RedirectToAction("Index");
        }

 
    }
}
