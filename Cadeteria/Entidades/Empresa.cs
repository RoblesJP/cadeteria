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
       
    }
}
