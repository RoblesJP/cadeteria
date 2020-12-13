using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadeteria.ViewModels
{
    public class PedidoViewModel
    {
        public int Id { get; set; }
        public string NombreDeCliente { get; set; }
        public string NombreDeCadete { get; set; }
        public Entidades.Tipo Tipo { get; set; }
        public Entidades.Estado Estado { get; set; }
        public string Descripcion { get; set; }
        public bool TieneCuponDeDescuento { get; set; }
        public double Precio { get; set; }
    }
}
