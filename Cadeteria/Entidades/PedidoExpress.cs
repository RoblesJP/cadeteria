using System;
using System.Collections.Generic;
using System.Text;

namespace Cadeteria.Entidades
{
    class PedidoExpress : Pedido
    {
        // propiedades
        public override double Precio
        {
            get
            {
                double precioFinal = PrecioBase * 1.25;
                if (TieneCuponDeDescuento)
                {
                    precioFinal *= 0.90;
                }
                return precioFinal;
            }

        }

        // constructor
        public PedidoExpress(string descripcion, bool tieneCuponDeDescuento) : base(descripcion, tieneCuponDeDescuento) { }
    }
}
