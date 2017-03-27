using Prueba.Veterinaria.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Prueba.Veterinaria.Models
{
    public class TareaModel
    {
        public List<Tarea> GetAll()
        {
            using (var db = new VeterinariaContext())
            {
                var tareas = db.Tareas.Include(x => x.TipoTarea).ToList();
                return tareas;
            }
        }
    }
}