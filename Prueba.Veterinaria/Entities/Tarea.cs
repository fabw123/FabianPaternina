using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Prueba.Veterinaria.Entities
{
    public class Tarea :BaseEntity
    {
        [Key]
        public int IdTarea { get; set; }
        public string Nombre { get; set; }
        [DataType(DataType.Currency)]
        public float Valor { get; set; }
        public int IdTipoTarea { get; set; }
        public TipoTarea TipoTarea { get; set; }
        public List<MascotaTarea> MascotasTareas { get; set; }
    }
}