using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cadeteria.ViewModels
{
    public class UsuarioViewModel
    {
        [Required(ErrorMessage = "Este campo no puede estar vacio")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Este campo no puede estar vacio")]
        public string Password { get; set; }

        
        public string Rol { get; set; }
    }
}
