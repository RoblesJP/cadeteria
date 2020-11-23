using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Cadeteria.Entidades
{
    public class Empresa
    {
        // atributos
        private string nombre;
        private List<Cadete> listaDeCadetes;
        private List<Cliente> listaDeClientes;

        // propiedades
        public string Nombre { get => nombre; set => nombre = value; }
        public List<Cadete> ListaDeCadetes { get => listaDeCadetes; set => listaDeCadetes = value; }
        public List<Cliente> ListaDeClientes { get => listaDeClientes; set => listaDeClientes = value; }

        // constructor
        public Empresa(string nombre)
        {
            Nombre = nombre;
            ListaDeCadetes = new List<Cadete>();
            ListaDeClientes = new List<Cliente>();
        }

        // métodos
        public void AgregarCadete(string nombre, string direccion, string telefono, Vehiculo vehiculo)
        {
            Cadete nuevoCadete = new Cadete(nombre, direccion, telefono, vehiculo);
            ListaDeCadetes.Add(nuevoCadete);
        }

        public void AgregarCliente(string nombre, string direccion, string telefono)
        {
            Cliente nuevoCliente = new Cliente(nombre, direccion, telefono);
            ListaDeClientes.Add(nuevoCliente);
        }

        public Cadete CadeteConMasPedidosEntregados()
        {
            Cadete miCadete = ListaDeCadetes[0];
            for (int i =  1; i < ListaDeCadetes.Count; i++)
            {
                if (ListaDeCadetes[i].CantidadDePedidosEntregados() > miCadete.CantidadDePedidosEntregados())
                {
                    miCadete = ListaDeCadetes[i];
                }
            }
            return miCadete;
        }
    }
}
