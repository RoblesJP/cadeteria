using NLog;
using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Text;

namespace Cadeteria
{
    public class Helper
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static readonly string[] NombresClientes = { "Juan", "Pedro", "Maria", "Carlos", "Aracely", "Claudio" };
        private static readonly string[] NombresCadetes = { "Gustavo", "Ramiro", "Luciana", "Paula", "Fernando", "Nelson" };

        public static Empresa GenerarEmpresaAleatoria(int cantidadDeCadetes, int cantidadDeClientes, int cantidadDePedidos)
        {
            // instancia de una empresa
            Empresa empresaAleatoria = new Empresa("Una empresa de mentira");
            // otras variables
            Random rnd = new Random();
            string direccionCliente = "Direccion del cliente";
            string direccionCadete = "Direccion del cadete";
            long telefonoCualquiera = 3811234567;

            // agregar cadetes
            for (int i = 0; i < cantidadDeCadetes; i++)
            {
                empresaAleatoria.AgregarCadete(NombresCadetes[rnd.Next(0, NombresCadetes.Length)], direccionCadete, telefonoCualquiera, (Vehiculo)rnd.Next(0, Enum.GetNames(typeof(Vehiculo)).Length));
            }

            // agregar clientes
            for (int i = 0; i < cantidadDeClientes; i++)
            {
                empresaAleatoria.AgregarCliente(NombresClientes[rnd.Next(0, NombresClientes.Length)], direccionCliente, telefonoCualquiera);
            }

            // realizar pedidos
            for (int i = 0; i < cantidadDePedidos; i++)
            {
                // un cliente aleatorio realiza un pedido
                empresaAleatoria.ListaDeClientes[rnd.Next(0, empresaAleatoria.ListaDeClientes.Count)].RealizarPedido((Tipo)rnd.Next(0, Enum.GetNames(typeof(Tipo)).Length),"Un pedido cualquiera", rnd.Next(0, 2) == 1);
            }

            // asignar pedidos a cadetes
            for (int i = 0; i < cantidadDePedidos; i++)
            {
                Cliente miCliente = null;
                Pedido suPedido = null;
                miCliente = empresaAleatoria.ListaDeClientes.Find(x => x.ListaDePedidosRealizados.Exists(y => y.Estado == Estado.Pendiente));
                suPedido = miCliente.ListaDePedidosRealizados.Find(x => x.Estado == Estado.Pendiente);
                foreach(Cadete miCadete in empresaAleatoria.ListaDeCadetes)
                {
                    try
                    {
                        miCadete.TomarPedido(suPedido);
                        break;
                    }
                    catch (InvalidOperationException ex)
                    {
                        Logger.Error(ex);
                        continue;
                    }
                }
            }

            // entregar pedidos
            foreach (Cadete miCadete in empresaAleatoria.ListaDeCadetes)
            {
                foreach (Pedido suPedido in miCadete.ListaDePedidos)
                {
                    // habrá un 50%  de posibilidad de que entregue el pedido
                    if (rnd.Next(0, 2) == 1)
                    {
                        miCadete.EntregarPedido(suPedido);
                    }
                }
            }
            return empresaAleatoria;
        }

        public static void InformeDeEmpresa(Empresa miEmpresa)
        {
            Console.WriteLine("####### {0} #######", miEmpresa.Nombre.ToUpper());
            Console.WriteLine("-- Lista de pedidos --");
            foreach (Cliente miCliente in miEmpresa.ListaDeClientes)
            {
                foreach (Pedido suPedido in miCliente.ListaDePedidosRealizados)
                { 
                    Console.WriteLine(" > PEDIDO {0}: {1}", suPedido.Nro, suPedido.Descripcion);
                    Console.WriteLine("\t[{0}]", suPedido.GetType().Name);
                    Console.WriteLine("\t[{0}]", suPedido.Estado);
                    Console.WriteLine("\t[Realizado por cliente ID{0}][Cupon: {1}]", miCliente.Id, suPedido.TieneCuponDeDescuento);
                    if (suPedido.Estado == Estado.Entregado)
                    {
                        Cadete cadeteQueEntregoElPedido = miEmpresa.ListaDeCadetes.Find(x => x.ListaDePedidos.Exists(y => y.Nro == suPedido.Nro));
                        Console.WriteLine("\t[Entregado por cadete ID{0}]", cadeteQueEntregoElPedido.Id);
                        Console.WriteLine("\t[Monto: ${0}]", suPedido.Precio);
                    }
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
            Console.WriteLine("-- Lista de cadetes --");
            foreach (Cadete miCadete in miEmpresa.ListaDeCadetes)
            {
                Console.WriteLine(" > CADETE ID{0}: {1} | Vehiculo: {2} | Pedidos asignados: {5} | Pedidos entregados: {3} | Promedio: {6:0.##}% |  Jornal: ${4}", 
                    miCadete.Id, miCadete.Nombre, miCadete.Vehiculo, miCadete.CantidadDePedidosEntregados(), miCadete.Jornal(), miCadete.CantidadDePedidos(), miCadete.PromedioDePedidosEntregados()*100);
            }
            Cadete cadeteConMasPedidosEntregados = miEmpresa.CadeteConMasPedidosEntregados();
            Console.WriteLine();
            Console.WriteLine("Cadete con mas pedidos entregados: Cadete {0} - {1} con {2} pedidos entregados", 
                cadeteConMasPedidosEntregados.Id, cadeteConMasPedidosEntregados.Nombre, cadeteConMasPedidosEntregados.CantidadDePedidosEntregados());

            Console.WriteLine();
            Console.WriteLine("-- Lista de clientes --");
            foreach (Cliente miCliente in miEmpresa.ListaDeClientes)
            {
                Console.WriteLine(" > CLIENTE ID{0}: {1} | Pedidos realizados: {2}", miCliente.Id, miCliente.Nombre, miCliente.CantidadDePedidos());
            }
        }
    }
}
