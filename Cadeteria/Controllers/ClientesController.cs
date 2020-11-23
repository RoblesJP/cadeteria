using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using Cadeteria.Entidades;
using Cadeteria.Models;
using Cadeteria.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

        public IActionResult RegistrarClienteForm(ModelStateDictionary M)
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarCliente(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                clientesRepository.Insert(cliente);
                return RedirectToAction("Index");
            }
            else
            {
                return View("RegistrarClienteForm");
            }
            
        }
    }
}
