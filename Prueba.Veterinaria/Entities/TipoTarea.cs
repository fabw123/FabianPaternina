using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Prueba.Veterinaria.Entities
{
    public class TipoTarea
    {
        [Key]
        public int IdTipoTarea { get; set; }
        public string Nombre { get; set; }
        public List<Tarea> Tareas { get; set; }
    }
}