using System;
using System.Collections.Generic;
using System.Text;

namespace Cadeteria.Entidades
{
    public class PedidoDelicado : Pedido
    {
        // propiedades
        public override double Precio 
        {
            get 
            {
                double precioFinal = PrecioBase * 1.30;
                if (TieneCuponDeDescuento)
                {
                    precioFinal *= 0.90;
                }
                return precioFinal;
            }
            
        }

        // constructor
        public PedidoDelicado(string descripcion, bool tieneCuponDeDescuento) : base(descripcion, tieneCuponDeDescuento) { }
    }
}
