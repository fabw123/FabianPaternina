﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Prueba.Veterinaria.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Cedula")]
        public string Cedula { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
    }
}