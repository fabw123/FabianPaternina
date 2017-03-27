using Prueba.Veterinaria.Entities;
using Prueba.Veterinaria.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Prueba.Veterinaria.Models
{
    public class VeterinariaContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Mascota> Mascotas { get; set; }
        public DbSet<Raza> Razas { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<TipoTarea> TiposTareas { get; set; }
        public DbSet<MascotaTarea> MascotasTareas { get; set; }
        public DbSet<Sexo> Sexos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<VeterinariaContext, Configuration>());

            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Mascota>().ToTable("Mascota");
            modelBuilder.Entity<Raza>().ToTable("Raza");
            modelBuilder.Entity<Tarea>().ToTable("Tarea");
            modelBuilder.Entity<TipoTarea>().ToTable("TipoTarea");
            modelBuilder.Entity<MascotaTarea>().ToTable("MascotaTarea");
            modelBuilder.Entity<Sexo>().ToTable("Sexo");

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Mascotas)
                .WithRequired(m => m.Usuario);

            modelBuilder.Entity<Raza>()
                .HasMany(r => r.Mascotas)
                .WithRequired(m => m.Raza);

            modelBuilder.Entity<TipoTarea>()
                .HasMany(t => t.Tareas)
                .WithRequired(t => t.TipoTarea);

            modelBuilder.Entity<Tarea>()
                .HasMany(t => t.MascotasTareas)
                .WithRequired(m => m.Tarea);

            modelBuilder.Entity<Mascota>()
                .HasMany(m => m.MascotasTareas)
                .WithRequired(m => m.Mascota);

            modelBuilder.Entity<Sexo>()
                .HasMany(s => s.Mascotas)
                .WithRequired(m => m.Sexo);

        }
    }
}