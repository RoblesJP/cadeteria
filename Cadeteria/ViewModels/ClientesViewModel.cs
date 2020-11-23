using Cadeteria.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadeteria.ViewModels
{
    public class ClientesViewModel
    {
        public List<Cliente> Clientes { get; set; }
        public string PageTitle { get; set; }
    }
}
