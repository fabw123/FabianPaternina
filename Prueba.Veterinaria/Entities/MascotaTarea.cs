using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Prueba.Veterinaria.Entities
{
    public class MascotaTarea
    {
        [Key]
        public int IdMascotaTarea { get; set; }
        [Required]
        [Display(Name ="Mascota")]
        public int IdMascota { get; set; }
        [Required]
        [Display(Name ="Tarea")]
        public int IdTarea { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        public bool Finalizada { get; set; }

        public Mascota Mascota { get; set; }
        public Tarea Tarea { get; set; }
    }
}