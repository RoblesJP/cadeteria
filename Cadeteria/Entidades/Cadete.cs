using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;

namespace Cadeteria.Entidades
{   
    public enum Vehiculo
    {
        Bicicleta = 1,
        Auto = 2,
        Moto = 3
    }

    public class Cadete : Persona
    {
        // atributos
        private Vehiculo vehiculo;
        private List<Pedido> listaDePedidos;
        private int id;

        // propiedades
        public List<Pedido> ListaDePedidos { get => listaDePedidos; set => listaDePedidos = value; }
        public Vehiculo Vehiculo { get => vehiculo; set => vehiculo = value; }
        public int Id { get => id; set => id = value; }

        // constructor
        public Cadete() : base() { }

        public Cadete(int id, string nombre, string direccion, string telefono, Vehiculo vehiculo) : base(nombre, direccion, telefono)
        {
            Id = id;
            Vehiculo = vehiculo;
            ListaDePedidos = new List<Pedido>();
        }

        // métodos
    }
}
