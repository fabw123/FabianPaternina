using Prueba.Veterinaria.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Prueba.Veterinaria.Models
{
    public class MascotaViewModel
    {
        [Key]
        public int IdMascota { get; set; }
        [Required]
        public string Nombre { get; set; }

        public HttpPostedFileBase Foto { get; set; }

        [Required]
        public string Apodo { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de nacimiento")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime FechaNacimiento { get; set; }
        public int IdUsuario { get; set; }

        [Required]
        [Display(Name = "Raza")]
        public int IdRaza { get; set; }

        [Required]
        [Display(Name = "Sexo")]
        public int IdSexo { get; set; }
        public Usuario Usuario { get; set; }
        public Raza Raza { get; set; }
        public Sexo Sexo { get; set; }
        public List<MascotaTarea> MascotasTareas { get; set; }
    }
}