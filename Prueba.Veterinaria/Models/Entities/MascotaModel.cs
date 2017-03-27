using Prueba.Veterinaria.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Prueba.Veterinaria.Models
{
    public class MascotaModel
    {
        public List<Mascota> GetMascotasByUsuario(int IdUsuario)
        {
            using (var db = new VeterinariaContext())
            {
                List<Mascota> mascotas = db.Mascotas
                                            .Include(m => m.Raza)
                                            .Where(m => m.Habilitado && m.IdUsuario == IdUsuario)
                                            .ToList();
                return mascotas;
            }
        }

        public Mascota Crear(Mascota mascota)
        {
            using (var db = new VeterinariaContext())
            {
                db.Mascotas.Add(mascota);
                db.SaveChanges();
                return mascota;
            }
        }

        public Mascota Editar(Mascota mascota)
        {
            using (var db = new VeterinariaContext())
            {
                db.Entry(mascota).State = EntityState.Modified;
                db.SaveChanges();
                return mascota;
            }
        }

        public Mascota GetMascotaById(int IdMascota)
        {
            using(var db = new VeterinariaContext())
            {
                var mascota = db.Mascotas
                    .Include(m=> m.Raza)
                    .Include(m=> m.Sexo)
                    .FirstOrDefault(m=> m.IdMascota == IdMascota);
                return mascota;
            }
        }
    }
}