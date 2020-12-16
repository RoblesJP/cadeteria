using AutoMapper;
using Cadeteria.Entidades;
using Cadeteria.Models;
using Cadeteria.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace Cadeteria.Controllers
{
    public class CadetesController : Controller
    {
        private readonly IMapper _mapper;
        private CadetesRepository cadetesRepository = new CadetesRepository();
        private PedidosRepository pedidosRepository = new PedidosRepository();

        public CadetesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Username") != null && HttpContext.Session.GetString("Rol") == "Admin")
            {
                List<Cadete> cadetes = cadetesRepository.GetAll();
                List<CadeteViewModel> cadetesViewModel = _mapper.Map<List<CadeteViewModel>>(cadetes);

                return View(cadetesViewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult RegistrarCadeteForm()
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
        public IActionResult RegistrarCadete(CadeteViewModel cadeteViewModel)
        {
            if (HttpContext.Session.GetString("Username") != null && HttpContext.Session.GetString("Rol") == "Admin")
            {
                Cadete cadete = _mapper.Map<Cadete>(cadeteViewModel);
                if (ModelState.IsValid)
                {
                    cadetesRepository.Insert(cadete);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("RegistrarCadeteForm");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult ModificarCadeteForm(int id)
        {
            if (HttpContext.Session.GetString("Username") != null && HttpContext.Session.GetString("Rol") == "Admin")
            {
                Cadete cadete = cadetesRepository.GetCadete(id);
                CadeteViewModel cadeteViewModel = _mapper.Map<CadeteViewModel>(cadete);
                return View(cadeteViewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult ModificarCadete(CadeteViewModel cadeteViewModel)
        {
            if (HttpContext.Session.GetString("Username") != null && HttpContext.Session.GetString("Rol") == "Admin")
            {
                if (ModelState.IsValid)
                {
                    Cadete cadete = _mapper.Map<Cadete>(cadeteViewModel);
                    cadetesRepository.Update(cadete);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("ModificarCadeteForm", cadeteViewModel);
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult EliminarCadete(int id)
        {
            if (HttpContext.Session.GetString("Username") != null && HttpContext.Session.GetString("Rol") == "Admin")
            {
                if (id > 0)
                {
                    cadetesRepository.Delete(id);
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult PedidosAsignados()
        {
            if (HttpContext.Session.GetString("Username") != null && HttpContext.Session.GetString("Rol") == "Cadete")
            {
                int? id = HttpContext.Session.GetInt32("IdUsuario");
                string username = HttpContext.Session.GetString("Username");
                string rol = HttpContext.Session.GetString("Rol");

                Cadete cadete = cadetesRepository.GetAll().Find(c => c.Nombre == username);
                List<Pedido> pedidosDelCadete = pedidosRepository.GetAll().Where(p => p.Cadete.Id == cadete.Id).ToList();
                List<PedidoViewModel> pedidosViewModel = _mapper.Map<List<PedidoViewModel>>(pedidosDelCadete);

                return View(pedidosViewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult EntregarPedido(int id)
        {
            if (HttpContext.Session.GetString("Username") != null && HttpContext.Session.GetString("Rol") == "Cadete")
            {
                Pedido pedido = pedidosRepository.GetPedido(id);
                if (pedido.Estado != Estado.Entregado)
                {
                    pedido.Estado = Estado.Entregado;
                    pedidosRepository.Update(pedido);
                }
                return RedirectToAction("PedidosAsignados");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
