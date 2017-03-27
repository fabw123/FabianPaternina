using Prueba.Veterinaria.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Prueba.Veterinaria.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<VeterinariaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Prueba.Veterinaria.Models.VeterinariaContext";
        }

        protected override void Seed(Prueba.Veterinaria.Models.VeterinariaContext context)
        {
        }
    }
}