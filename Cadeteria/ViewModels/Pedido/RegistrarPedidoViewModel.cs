using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cadeteria.ViewModels
{
    public class RegistrarPedidoViewModel
    {

        [Required(ErrorMessage = "Este campo no puede estar vacio")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "Este campo no puede estar vacio")]
        public int IdCadete { get; set; }

        [Required(ErrorMessage = "Este campo no puede estar vacio")]
        public Entidades.Tipo Tipo { get; set; }

        [Required(ErrorMessage = "Este campo no puede estar vacio")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "La descripcion debe ser entre 10 y 50 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Este campo no puede estar vacio")]
        public bool TieneCuponDeDescuento { get; set; }

        public List<SelectListItem> clientesSelectListItem { get; set; }
        public List<SelectListItem> cadetesSelectListItem { get; set; }

    }
}
