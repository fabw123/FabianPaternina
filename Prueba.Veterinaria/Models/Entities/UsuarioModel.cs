using Prueba.Veterinaria.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Prueba.Veterinaria.Models
{
    public class UsuarioModel
    {
        public int Validar(LoginViewModel model)
        {
            int IdUsuario = -1;
            using(var db = new VeterinariaContext())
            {
                Usuario usuario = db.Usuarios.FirstOrDefault(u => u.Cedula == model.Cedula && u.Contrasenna == model.Password);
                if (usuario != null)
                    IdUsuario = usuario.IdUsuario;

            }
            return IdUsuario;
        }

        public bool Existe(Usuario usuario)
        {
            using(var db = new VeterinariaContext())
            {
                bool exist = db.Usuarios.Any(U => (U.Email != null && U.Email.ToUpper() == usuario.Email.ToUpper()) || U.Cedula == usuario.Cedula);
                return exist;
            }
        }

        public Usuario Crear(Usuario usuario)
        {
            using (var db = new VeterinariaContext())
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return usuario;
            }
        }

        public Usuario Editar(Usuario usuario)
        {
            using (var db = new VeterinariaContext())
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return usuario;
            }
        }

        public Usuario GetusuarioById(int id)
        {
            using (var db = new VeterinariaContext())
            {
                Usuario usuario = db.Usuarios.Find(id);
                return usuario;
            }
        }
    }
}