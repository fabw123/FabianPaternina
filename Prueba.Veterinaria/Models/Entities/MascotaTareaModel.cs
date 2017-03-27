using Prueba.Veterinaria.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Prueba.Veterinaria.Models
{
    public class MascotaTareaModel
    {
        public List<TareaViewModel> GetAll()
        {
            using (var db = new VeterinariaContext())
            {
                var tareas = new List<TareaViewModel>();
                var mascotasTareas = db.MascotasTareas
                    .Include(x => x.Mascota)
                    .Include(x => x.Tarea).ToList();
                mascotasTareas.ForEach(x =>
                {
                    tareas.Add(
                    new TareaViewModel
                    {
                        IdMascotaTarea = x.IdMascotaTarea,
                        Fecha = x.Fecha.Date,
                        Hora = x.Fecha.TimeOfDay,
                        Finalizada = x.Finalizada,
                        IdMascota = x.IdMascota,
                        IdTarea = x.IdTarea,
                        Mascota = x.Mascota,
                        Tarea = x.Tarea
                    });
                });

                return tareas;
            }
        }

        public List<TareaViewModel> GetByIdMascota(int IdMascota, DateTime fechaInicial , DateTime fechaFinal, int idMascotaTarea = 0)
        {
            using (var db = new VeterinariaContext())
            {
                var tareas = new List<TareaViewModel>();
                var mascotasTareas = db.MascotasTareas
                    .Include(x => x.Mascota)
                    .Include(x => x.Tarea)
                    .Where(x => x.IdMascota == IdMascota && x.IdMascotaTarea != idMascotaTarea && x.Fecha>= fechaInicial && x.Fecha <= fechaFinal)
                    .ToList();
                mascotasTareas.ForEach(x =>
                {
                    tareas.Add(
                    new TareaViewModel
                    {
                        IdMascotaTarea = x.IdMascotaTarea,
                        Fecha = x.Fecha.Date,
                        Hora = x.Fecha.TimeOfDay,
                        Finalizada = x.Finalizada,
                        IdMascota = x.IdMascota,
                        IdTarea = x.IdTarea,
                        Mascota = x.Mascota,
                        Tarea = x.Tarea
                    });
                });

                return tareas;
            }
        }

        public List<TareaViewModel> GetByIdUsuario(int IdUsuario)
        {
            using (var db = new VeterinariaContext())
            {
                var tareas = new List<TareaViewModel>();
                var mascotaTareas = db.MascotasTareas
                    .Include(x => x.Mascota)
                    .Include(x => x.Tarea)
                    .Where(x => x.Mascota.IdUsuario == IdUsuario)
                    .ToList();

                mascotaTareas.ForEach(x =>
                {
                    tareas.Add(
                    new TareaViewModel
                    {
                        IdMascotaTarea = x.IdMascotaTarea,
                        Fecha = x.Fecha.Date,
                        Hora = x.Fecha.TimeOfDay,
                        Finalizada = x.Finalizada,
                        IdMascota = x.IdMascota,
                        IdTarea = x.IdTarea,
                        Mascota = x.Mascota,
                        Tarea = x.Tarea
                    });
                });
                return tareas;
            }
        }

        public TareaViewModel GetViewModelById(int IdMascotaTarea)
        {
            using (var db = new VeterinariaContext())
            {
                var mascotaTarea = db.MascotasTareas
                    .Include(x => x.Mascota)
                    .Include(x => x.Tarea)
                    .FirstOrDefault(x => x.IdMascotaTarea == IdMascotaTarea);

                var tarea = new TareaViewModel
                {
                    IdMascotaTarea = mascotaTarea.IdMascotaTarea,
                    Fecha = mascotaTarea.Fecha.Date,
                    Hora = mascotaTarea.Fecha.TimeOfDay,
                    Finalizada = mascotaTarea.Finalizada,
                    IdMascota = mascotaTarea.IdMascota,
                    IdTarea = mascotaTarea.IdTarea,
                    Mascota = mascotaTarea.Mascota,
                    Tarea = mascotaTarea.Tarea
                };

                return tarea;
            }
        }

        public MascotaTarea GetById(int IdMascotaTarea)
        {
            using (var db = new VeterinariaContext())
            {
                var mascotaTarea = db.MascotasTareas
                    .Include(x => x.Mascota)
                    .Include(x => x.Tarea)
                    .FirstOrDefault(x => x.IdMascotaTarea == IdMascotaTarea);

                return mascotaTarea;
            }
        }

        public MascotaTarea Crear(MascotaTarea mascotaTarea)
        {
            using (var db = new VeterinariaContext())
            {
                db.MascotasTareas.Add(mascotaTarea);
                db.SaveChanges();
                return mascotaTarea;
            }
        }

        public MascotaTarea Editar(MascotaTarea mascotaTarea)
        {
            using (var db = new VeterinariaContext())
            {
                db.Entry(mascotaTarea).State = EntityState.Modified;
                db.SaveChanges();
                return mascotaTarea; 
            }
        }

        public bool Eliminar(MascotaTarea mascotaTarea)
        {
            bool eliminado = false;
            using (var db = new VeterinariaContext())
            {
                var x = db.MascotasTareas.Find(mascotaTarea.IdMascotaTarea);
                db.MascotasTareas.Remove(x);
                db.SaveChanges();
                eliminado = true;
            }
            return eliminado;
        }
    }
}