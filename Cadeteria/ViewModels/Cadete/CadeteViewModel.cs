using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cadeteria.ViewModels
{
    public class CadeteViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo no puede estar vacio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo no puede estar vacio")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Este campo no puede estar vacio")]
        public Entidades.Vehiculo Vehiculo { get; set; }

        [Required(ErrorMessage = "Este campo no puede estar vacio")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "El numero de telefono debe ser de 10 digitos")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Ingrese un numero")]
        public string Telefono { get; set; }
    }
}
