using AutoMapper;
using Cadeteria.Entidades;
using Cadeteria.Models;
using Cadeteria.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadeteria.Controllers
{
    public class PedidosController : Controller
    {
        private readonly IMapper _mapper;
        private PedidosRepository pedidosRepository = new PedidosRepository();
        private ClientesRepository clientesRepository = new ClientesRepository();
        private CadetesRepository cadetesRepository = new CadetesRepository();

        public PedidosController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Username") != null && HttpContext.Session.GetString("Rol") == "Admin")
            {
                List<Pedido> pedidos = pedidosRepository.GetAll();
                List<PedidoViewModel> pedidosViewModel = _mapper.Map<List<PedidoViewModel>>(pedidos);

                return View(pedidosViewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult RegistrarPedidoForm()
        {
            if (HttpContext.Session.GetString("Username") != null && HttpContext.Session.GetString("Rol") == "Admin")
            {
                List<Cliente> clientes = clientesRepository.GetAll();
                List<Cadete> cadetes = cadetesRepository.GetAll();

                List<SelectListItem> clientesSelectListItem = _mapper.Map<List<SelectListItem>>(clientes);
                List<SelectListItem> cadetesSelectListItem = _mapper.Map<List<SelectListItem>>(cadetes);

                RegistrarPedidoViewModel registrarPedidoViewModel = new RegistrarPedidoViewModel();
                registrarPedidoViewModel.cadetesSelectListItem = cadetesSelectListItem;
                registrarPedidoViewModel.clientesSelectListItem = clientesSelectListItem;

                return View(registrarPedidoViewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult RegistrarPedido(RegistrarPedidoViewModel registrarPedidoViewModel)
        {
            if (HttpContext.Session.GetString("Username") != null && HttpContext.Session.GetString("Rol") == "Admin")
            {

                if (ModelState.IsValid)
                {
                    Cliente cliente = clientesRepository.GetCliente(registrarPedidoViewModel.IdCliente);
                    Cadete cadete = cadetesRepository.GetCadete(registrarPedidoViewModel.IdCadete);
                    Pedido pedido = _mapper.Map<Pedido>(registrarPedidoViewModel);
                    pedido.Cliente = cliente;
                    pedido.Cadete = cadete;
                    pedidosRepository.Insert(pedido);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("RegistrarPedidoForm");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult ModificarPedidoForm(int id)
        {
            if (HttpContext.Session.GetString("Username") != null && HttpContext.Session.GetString("Rol") == "Admin")
            {
                List<Cliente> clientes = clientesRepository.GetAll();
                List<Cadete> cadetes = cadetesRepository.GetAll();

                List<SelectListItem> clientesSelectListItem = _mapper.Map<List<SelectListItem>>(clientes);
                List<SelectListItem> cadetesSelectListItem = _mapper.Map<List<SelectListItem>>(cadetes);

                Pedido pedido = pedidosRepository.GetPedido(id);
                ModificarPedidoViewModel modificarPedidoViewModel = _mapper.Map<ModificarPedidoViewModel>(pedido);
                modificarPedidoViewModel.cadetesSelectListItem = cadetesSelectListItem;
                modificarPedidoViewModel.clientesSelectListItem = clientesSelectListItem;
                return View(modificarPedidoViewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult ModificarPedido(ModificarPedidoViewModel modificarPedidoViewModel)
        {
            if (HttpContext.Session.GetString("Username") != null && HttpContext.Session.GetString("Rol") == "Admin")
            {
                if (ModelState.IsValid)
                {
                    Cliente cliente = clientesRepository.GetCliente(modificarPedidoViewModel.IdCliente);
                    Cadete cadete = cadetesRepository.GetCadete(modificarPedidoViewModel.IdCadete);
                    Pedido pedido = _mapper.Map<Pedido>(modificarPedidoViewModel);
                    pedido.Cliente = cliente;
                    pedido.Cadete = cadete;
                    pedidosRepository.Update(pedido);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("ModificarPedidoForm", modificarPedidoViewModel);
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult EliminarPedido(int id)
        {
            if (HttpContext.Session.GetString("Username") != null && HttpContext.Session.GetString("Rol") == "Admin")
            {
                if (id > 0)
                {
                    pedidosRepository.Delete(id);
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
