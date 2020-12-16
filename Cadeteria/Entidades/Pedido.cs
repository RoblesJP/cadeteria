using System;

namespace Cadeteria.Entidades
{
    public enum Estado
    {
        Pendiente = 1,
        Entregado = 2
    }

    public enum Tipo
    {
        Express = 1,
        Delicado = 2,
        Ecologico = 3
    }

    public class Pedido
    {
        // atributos
        protected static double PrecioBase = 150;
        private int id;
        Cliente cliente;
        Cadete cadete;
        Tipo tipo;
        Estado estado;
        private string descripcion;
        private bool tieneCuponDeDescuento;


        // propiedades
        public int Id { get => id; set => id = value; }
        public Cliente Cliente { get => cliente; set => cliente = value; }
        public Cadete Cadete { get => cadete; set => cadete = value; }
        public Tipo Tipo { get => tipo; set => tipo = value; }
        public Estado Estado { get => estado; set => estado = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public bool TieneCuponDeDescuento { get => tieneCuponDeDescuento; set => tieneCuponDeDescuento = value; }

        // constructor
        public Pedido() { }

        public Pedido(int id, Cliente cliente, Cadete cadete, Tipo tipo, Estado estado, string descripcion, bool tieneCuponDeDescuento)
        {
            Id = id;
            Cliente = cliente;
            Cadete = cadete;
            Tipo = tipo;
            Estado = estado;
            Descripcion = descripcion;
            TieneCuponDeDescuento = tieneCuponDeDescuento;
        }

        public double GetPrecio ()
        {
            double precio = PrecioBase;
            switch (Tipo)
            {
                case (Tipo.Express):
                    precio = precio * 1.25;
                    break;
                case (Tipo.Delicado):
                    precio = precio * 1.3;
                    break;
            }

            if (TieneCuponDeDescuento == true)
            {
                precio = precio * 0.90;
            }

            return precio;
        }

        

    }
}
