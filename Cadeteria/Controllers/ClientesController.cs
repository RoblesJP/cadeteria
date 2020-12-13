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
using AutoMapper;

namespace Cadeteria.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IMapper _mapper;
        private ClientesRepository clientesRepository = new ClientesRepository();

        public ClientesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            List<Cliente> clientes = clientesRepository.GetAll();
            List<ClienteViewModel> clientesViewModel = _mapper.Map<List<ClienteViewModel>>(clientes);

            return View(clientesViewModel);
        }

        public IActionResult RegistrarClienteForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarCliente(ClienteViewModel clienteViewModel)
        {
            Cliente cliente = _mapper.Map<Cliente>(clienteViewModel);
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
        
        public IActionResult ModificarClienteForm(int id)
        {
            Cliente cliente = clientesRepository.GetCliente(id);
            ClienteViewModel clienteViewModel = _mapper.Map<ClienteViewModel>(cliente);
            return View(clienteViewModel);
        }

        [HttpPost]
        public IActionResult ModificarCliente(ClienteViewModel clienteViewModel)
        {
            if (ModelState.IsValid)
            {
                Cliente cliente = _mapper.Map<Cliente>(clienteViewModel);
                clientesRepository.Update(cliente);
                return RedirectToAction("Index");
            } 
            else
            {
                return View("ModificarClienteForm", clienteViewModel);
            }
        }

        public IActionResult EliminarCliente(int id)
        {
            if (id > 0)
            {
                clientesRepository.Delete(id);
            }
            return RedirectToAction("Index");
        }
    }
}
