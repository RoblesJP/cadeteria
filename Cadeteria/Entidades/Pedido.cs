using System;

namespace Cadeteria
{
    public enum Estado
    {
        Pendiente,
        Asignado,
        Entregado
    }

    public enum Tipo
    {
        PedidoExpress,
        PedidoDelicado,
        PedidoEcologico
    }

    public class Pedido
    {
        // atributos
        protected static double PrecioBase = 150;
        private int nro;
        private string descripcion;
        private bool tieneCuponDeDescuento;
        Estado estado;

        // propiedades
        public int Nro { get => nro; set => nro = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public Estado Estado { get => estado; set => estado = value; }
        public bool TieneCuponDeDescuento { get => tieneCuponDeDescuento; set => tieneCuponDeDescuento = value; }
        public virtual double Precio { get => 0;}

        // constructor
        public Pedido(string descripcion, bool tieneCuponDeDescuento)
        {
            Nro = new Random().Next(1000, 10000);
            Descripcion = descripcion;
            TieneCuponDeDescuento = tieneCuponDeDescuento;
            Estado = Estado.Pendiente;
        }
    }
}
