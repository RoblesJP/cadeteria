using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;

namespace Cadeteria
{
    public enum Vehiculo
    {
        Bicicleta,
        Moto,
        Auto
    }
    public class Cadete : Persona
    {
        private static int nextId = 0;
        // atributos
        private Vehiculo vehiculo;
        List<Pedido> listaDePedidos;

        // propiedades
        public List<Pedido> ListaDePedidos { get => listaDePedidos; set => listaDePedidos = value; }
        public Vehiculo Vehiculo { get => vehiculo; set => vehiculo = value; }

        // constructor
        public Cadete(string nombre, string direccion, long telefono, Vehiculo vehiculo) : base(nombre, direccion, telefono)
        {
            Id = ++nextId;
            Vehiculo = vehiculo;
            ListaDePedidos = new List<Pedido>();
        }

        // métodos
        public void TomarPedido(Pedido pedido)
        {
            if ((pedido.GetType().Name.ToString() == Tipo.PedidoExpress.ToString() && this.Vehiculo != Vehiculo.Moto)
               || (pedido.GetType().Name.ToString() == Tipo.PedidoDelicado.ToString() && this.Vehiculo != Vehiculo.Auto)
               || (pedido.GetType().Name.ToString() == Tipo.PedidoEcologico.ToString() && this.Vehiculo != Vehiculo.Bicicleta))
            {
                throw new InvalidOperationException($"El cadete ID{this.Id} no posee un vehiculo adecuado para entregar el pedido {pedido.Nro}");
            }
            else 
            {
                ListaDePedidos.Add(pedido);
                pedido.Estado = Estado.Asignado;
            } 
        }

        public double Jornal()
        {
            double jornal = 0;
            jornal = CantidadDePedidosEntregados() * 100;
            switch(this.Vehiculo)
            {
                case Vehiculo.Bicicleta:
                    jornal *= 1.05;
                    break;
                case Vehiculo.Moto:
                    jornal *= 1.20;
                    break;
                case Vehiculo.Auto:
                    jornal *= 1.25;
                    break;
            }
            return jornal;
        }

        public override int CantidadDePedidos()
        {
            return ListaDePedidos.Count;
        }

        public int CantidadDePedidosEntregados()
        {
            int cantidadDePedidosEntregados = (ListaDePedidos.Where(pedido => pedido.Estado == Estado.Entregado)).Count();
            return cantidadDePedidosEntregados;
        }

        public double PromedioDePedidosEntregados()
        {
            return (double) CantidadDePedidosEntregados() / (double) ListaDePedidos.Count;
        }

        public void EntregarPedido(Pedido suPedido)
        {
            suPedido.Estado = Estado.Entregado;
        }
    }
}
