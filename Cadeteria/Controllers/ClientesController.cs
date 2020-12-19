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
using Microsoft.AspNetCore.Http;

namespace Cadeteria.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IMapper _mapper;
        private ClientesRepository clientesRepository = new ClientesRepository();
        private PedidosRepository pedidosRepository = new PedidosRepository();

        public ClientesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Username") != null && HttpContext.Session.GetString("Rol") == "Admin")
            {
                List<Cliente> clientes = clientesRepository.GetAll();
                List<ClienteViewModel> clientesViewModel = _mapper.Map<List<ClienteViewModel>>(clientes);

                return View(clientesViewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        public IActionResult RegistrarClienteForm()
        {
            if (HttpContext.Session.GetString("Username") != null && HttpContext.Session.GetString("Rol") == "Admin")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult RegistrarCliente(ClienteViewModel clienteViewModel)
        {
            if (HttpContext.Session.GetString("Username") != null && HttpContext.Session.GetString("Rol") == "Admin")
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
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        
        public IActionResult ModificarClienteForm(int id)
        {
            if (HttpContext.Session.GetString("Username") != null && HttpContext.Session.GetString("Rol") == "Admin")
            {
                Cliente cliente = clientesRepository.GetCliente(id);
                ClienteViewModel clienteViewModel = _mapper.Map<ClienteViewModel>(cliente);
                return View(clienteViewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult ModificarCliente(ClienteViewModel clienteViewModel)
        {
            if (HttpContext.Session.GetString("Username") != null && HttpContext.Session.GetString("Rol") == "Admin")
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
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult EliminarCliente(int id)
        {
            if (HttpContext.Session.GetString("Username") != null && HttpContext.Session.GetString("Rol") == "Admin")
            {
                if (id > 0)
                {
                    PedidosRepository pedidosRepository = new PedidosRepository();
                    Pedido pedido = pedidosRepository.GetAll().Find(x => x.Cliente.Id == id);
                    pedidosRepository.Delete(pedido.Id);
                    clientesRepository.Delete(id); 
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult PedidosRealizados()
        {
            if (HttpContext.Session.GetString("Username") != null && HttpContext.Session.GetString("Rol") == "Cliente")
            {
                int? id = HttpContext.Session.GetInt32("IdUsuario");
                string username = HttpContext.Session.GetString("Username");
                string rol = HttpContext.Session.GetString("Rol");

                Cliente cliente = clientesRepository.GetAll().Find(c => c.Nombre == username);
                List<Pedido> pedidosDelCliente = pedidosRepository.GetAll().Where(p => p.Cliente.Id == cliente.Id).ToList();
                List<PedidoViewModel> pedidosViewModel = _mapper.Map<List<PedidoViewModel>>(pedidosDelCliente);

                return View(pedidosViewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
