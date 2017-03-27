using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Web;

namespace Prueba.Veterinaria.Models
{
    public class HomeViewModel
    {
        public int IdMascota { get; set; }
        public string Mascota { get; set; }
        public string Apodo { get; set; }
        public string Raza { get; set; }

        [DataType(DataType.Currency)]
        public float CostoTotal { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ProximaTarea { get; set; }
        public int Pendientes { get; set; }

        public float PorcentajeRealizadas { get; set; }

        public List<HomeViewModel> GetHomeViewModels(int idUsuario)
        {
            List<HomeViewModel> homeViewModels = new List<HomeViewModel>();
            using (var db = new VeterinariaContext())
            {
                db.Database.Initialize(force: false);
                var cmd = db.Database.Connection.CreateCommand();
                cmd.CommandText = "[dbo].[GetViewTareasHome]";
                cmd.CommandType = CommandType.StoredProcedure;

                DateTime fechaInicio = DateTime.Now.Date + new TimeSpan(0, 0, 0);
                DateTime fechaFin = DateTime.Now.Date + new TimeSpan(23, 59, 59);
                var pIdUsuario = cmd.CreateParameter();
                var pFechaInicio = cmd.CreateParameter();
                var pFechaFin = cmd.CreateParameter();
                pIdUsuario.ParameterName = "@IdUsuario";
                pFechaInicio.ParameterName = "@FechaInicio";
                pFechaFin.ParameterName = "@FechaFin";
                pIdUsuario.Value = idUsuario;
                pFechaInicio.Value = fechaInicio;
                pFechaFin.Value = fechaFin;
                cmd.Parameters.Add(pIdUsuario);
                cmd.Parameters.Add(pFechaInicio);
                cmd.Parameters.Add(pFechaFin);

                try
                {
                    db.Database.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    HomeViewModel homeViewModel;
                    while (reader.Read())
                    {
                        homeViewModel = new HomeViewModel();
                        homeViewModel.IdMascota = Convert.ToInt32(reader["idMascota"]);
                        homeViewModel.Mascota = reader["Mascota"].ToString();
                        homeViewModel.Raza = reader["Raza"].ToString();
                        homeViewModel.CostoTotal = float.Parse(reader["CostoTotal"].ToString());
                        homeViewModel.ProximaTarea = Convert.ToDateTime(reader["FechaProximaTarea"]);
                        homeViewModel.Pendientes = Convert.ToInt32(reader["Pendientes"]);
                        homeViewModel.PorcentajeRealizadas = float.Parse(reader["PorcentajeRealizadas"].ToString());
                        homeViewModel.Apodo = reader["Apodo"].ToString();
                        homeViewModels.Add(homeViewModel);
                    }
                    db.Database.Connection.Close();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return homeViewModels;
        }

        public float CostoTotalDia(int IdUsuario)
        {
            float valor = 0;
            DateTime fechaInicio = DateTime.Now.Date + new TimeSpan(0, 0, 0);
            DateTime fechaFinal = DateTime.Now.Date + new TimeSpan(23, 59, 59);

            using (var db = new VeterinariaContext())
            {
                var mascotasTareas = db.MascotasTareas
                                        .Include(x => x.Tarea)
                                        .Where(x => x.Mascota.IdUsuario == IdUsuario
                                                    && x.Fecha >= fechaInicio && x.Fecha <= fechaFinal)
                                        .ToList();
                mascotasTareas.ForEach(x => valor += x.Tarea.Valor);
            }
            return valor;
        }
    }
}