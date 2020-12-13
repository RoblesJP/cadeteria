using AutoMapper;
using Cadeteria.Entidades;
using Cadeteria.Models;
using Cadeteria.ViewModels;
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
            List<Pedido> pedidos = pedidosRepository.GetAll();
            List<PedidoViewModel> pedidosViewModel = _mapper.Map<List<PedidoViewModel>>(pedidos);

            return View(pedidosViewModel);
        }

        public IActionResult RegistrarPedidoForm()
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

        [HttpPost]
        public IActionResult RegistrarPedido(RegistrarPedidoViewModel registrarPedidoViewModel)
        {
            Cliente cliente = clientesRepository.GetCliente(registrarPedidoViewModel.IdCliente);
            Cadete cadete = cadetesRepository.GetCadete(registrarPedidoViewModel.IdCadete);
            Pedido pedido = _mapper.Map<Pedido>(registrarPedidoViewModel);
            pedido.Cliente = cliente;
            pedido.Cadete = cadete;
            if (ModelState.IsValid)
            {
                pedidosRepository.Insert(pedido);
                return RedirectToAction("Index");
            }
            else
            {
                return View("RegistrarPedidoForm");
            }
        }

        public IActionResult ModificarPedidoForm(int id)
        {
            Pedido pedido = pedidosRepository.GetPedido(id);
            PedidoViewModel pedidoViewModel = _mapper.Map<PedidoViewModel>(pedido);
            return View(pedidoViewModel);
        }

        [HttpPost]
        public IActionResult ModificarPedido(PedidoViewModel pedidoViewModel)
        {
            if (ModelState.IsValid)
            {
                Pedido pedido = _mapper.Map<Pedido>(pedidoViewModel);
                pedidosRepository.Update(pedido);
                return RedirectToAction("Index");
            }
            else
            {
                return View("ModificarPedidoForm", pedidoViewModel);
            }
        }

        public IActionResult EliminarPedido(int id)
        {
            if (id > 0)
            {
                pedidosRepository.Delete(id);
            }
            return RedirectToAction("Index");
        }
    }
}
