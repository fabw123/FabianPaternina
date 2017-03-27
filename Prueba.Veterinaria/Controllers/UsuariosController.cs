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
    public class UsuariosController : Controller
    {
        private VeterinariaContext db = new VeterinariaContext();
        private UsuarioModel model = new UsuarioModel();

        // GET: Usuarios
        public ActionResult Index()
        {
            return View(db.Usuarios.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Crear()
        {
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear([Bind(Include = "IdUsuario,Cedula,Contrasenna")] Usuario usuario)
        {
            try
            {

                bool existe = model.Existe(usuario);
                if (existe)
                {
                    ModelState.AddModelError("", "Usuario ya existe en el sistema");
                    return View(usuario);
                }
                else
                {
                    usuario.Habilitado = true;
                    usuario = model.Crear(usuario);
                    Session["IdUsuario"] = usuario.IdUsuario;
                    return RedirectToAction("Editar", "Usuarios", new { id = usuario.IdUsuario });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET: Usuarios/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Usuario usuario = model.GetusuarioById(id.Value);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "IdUsuario,Cedula,Nombre,Apellido,FechaNacimiento,Sexo,Email,Telefono,Habilitado")] Usuario usuario)
        {
            Usuario _usuario = model.GetusuarioById(usuario.IdUsuario);
            _usuario.Apellido = usuario.Apellido;
            _usuario.Cedula = usuario.Cedula;
            _usuario.Email = usuario.Email;
            _usuario.FechaNacimiento = usuario.FechaNacimiento;
            _usuario.Habilitado = true;
            _usuario.Nombre = usuario.Nombre;
            _usuario.Sexo = usuario.Sexo;
            _usuario.Telefono = usuario.Telefono;
            model.Editar(_usuario);

            return RedirectToAction("Index", "Home");
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
