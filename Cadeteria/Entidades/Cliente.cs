using System;
using System.Collections.Generic;
using System.Text;

namespace Cadeteria.Entidades
{
    public class Cliente : Persona
    {
        //private static int nextId = 0;
        // atributos
        private List<Pedido> listaDePedidosRealizados;
        private int id;

        // propiedades
        public List<Pedido> ListaDePedidosRealizados { get => listaDePedidosRealizados; set => listaDePedidosRealizados = value; }
        public int Id { get => id; set => id = value; }

        // constructores
        public Cliente() : base() { }

        public Cliente(string nombre, string direccion, string telefono) : base(nombre, direccion, telefono)
        {
            Id = 0;
            ListaDePedidosRealizados = new List<Pedido>();
        }

        public Cliente(int id, string nombre, string direccion, string telefono) : base(nombre, direccion, telefono)
        {
            Id = id;
            ListaDePedidosRealizados = new List<Pedido>();
        }

        // métodos
        public override int CantidadDePedidos()
        {
            return ListaDePedidosRealizados.Count;
        }
    }
}
