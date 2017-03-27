using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Prueba.Veterinaria.Entities
{
    public class Raza : BaseEntity
    {
        [Key]
        public int IdRaza { get; set; }
        public string Nombre { get; set; }
        public List<Mascota> Mascotas { get; set; }
    }
}