using System;
using System.Collections.Generic;
using System.Text;

namespace Cadeteria
{
    public class Cliente : Persona
    {
        private static int nextId = 0;
        // atributos
        private List<Pedido> listaDePedidosRealizados;

        // propiedades
        public List<Pedido> ListaDePedidosRealizados { get => listaDePedidosRealizados; set => listaDePedidosRealizados = value; }

        // constructor
        public Cliente(string nombre, string direccion, long telefono) : base(nombre, direccion, telefono)
        { 
            Id = ++nextId;
            ListaDePedidosRealizados = new List<Pedido>();
        }

        // métodos
        public override int CantidadDePedidos()
        {
            return ListaDePedidosRealizados.Count;
        }

        public Pedido RealizarPedido(Tipo tipo, string descripcion, bool tieneCuponDeDescuento)
        {
            Pedido suPedido = null;
            switch (tipo)
            {
                case Tipo.PedidoExpress:
                    suPedido = new PedidoExpress(descripcion, tieneCuponDeDescuento);
                    break;
                case Tipo.PedidoDelicado:
                    suPedido = new PedidoDelicado(descripcion, tieneCuponDeDescuento);
                    break;
                case Tipo.PedidoEcologico:
                    suPedido = new PedidoEcologico(descripcion, tieneCuponDeDescuento);
                    break;
            }
            ListaDePedidosRealizados.Add(suPedido);
            return suPedido;
        }
    }
}
