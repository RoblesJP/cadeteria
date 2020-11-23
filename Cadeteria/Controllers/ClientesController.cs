using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using Cadeteria.Entidades;
using Cadeteria.Models;
using Cadeteria.ViewModels;

namespace Cadeteria.Controllers
{
    public class ClientesController : Controller
    {
        private ClientesRepository clientesRepository= new ClientesRepository();

        public IActionResult Index()
        {
            ClientesViewModel clientesViewModel = new ClientesViewModel()
            {
                Clientes = clientesRepository.GetAll(),
                PageTitle = "Lista de Clientes"
            };

            return View(clientesViewModel);
        }

        public IActionResult RegistrarClienteForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarCliente(Cliente cliente)
        {
            clientesRepository.Insert(cliente);
            return Redirect("Index");
        }
    }
}
