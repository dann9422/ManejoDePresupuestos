﻿using ManejoDePresupuestos.vadilaciones;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ManejoDePresupuestos.Models
{
    public class TipoCuenta
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "La longitud del campo {0} debe de estra entre los {2} y {1}")]
        [PrimeraLetraMayuscula]
        [Remote(action: "VerificarExisteTipoCuenta",controller:"TiposCuentas")]
        public string Nombre { get; set; }

        public int UsuarioId { get; set; }

        public int Orden { get; set; }
    }
}
