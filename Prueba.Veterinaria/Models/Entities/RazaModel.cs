using Prueba.Veterinaria.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prueba.Veterinaria.Models
{
    public class RazaModel
    {
        public List<Raza> GetRazas()
        {
            using (var db = new VeterinariaContext())
            {
                List<Raza> razas = db.Razas.ToList();
                return razas;
            }
        }
    }
}