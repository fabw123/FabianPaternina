using Prueba.Veterinaria.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prueba.Veterinaria.Models
{
    public class SexoModel
    {
        public List<Sexo> GetSexos()
        {
            using (var db = new VeterinariaContext())
            {
                List<Sexo> sexos = db.Sexos.ToList();
                return sexos;
            }
        }
    }
}