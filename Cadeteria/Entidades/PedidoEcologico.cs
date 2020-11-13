using System;
using System.Collections.Generic;
using System.Text;

namespace Cadeteria
{
    public class PedidoEcologico : Pedido
    {
        // propiedades
        public override double Precio
        {
            get
            {
                double precioFinal = PrecioBase;
                if (TieneCuponDeDescuento)
                {
                    precioFinal *= 0.90;
                }
                return precioFinal;
            }

        }

        // constructor
        public PedidoEcologico(string descripcion, bool tieneCuponDeDescuento) : base(descripcion, tieneCuponDeDescuento) { }
    }
}
