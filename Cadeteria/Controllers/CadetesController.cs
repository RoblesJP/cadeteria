using AutoMapper;
using Cadeteria.Entidades;
using Cadeteria.Models;
using Cadeteria.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadeteria.Controllers
{
    public class CadetesController : Controller
    {
        private readonly IMapper _mapper;
        private CadetesRepository cadetesRepository = new CadetesRepository();

        public CadetesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            List<Cadete> cadetes = cadetesRepository.GetAll();
            List<CadeteViewModel> cadetesViewModel = _mapper.Map<List<CadeteViewModel>>(cadetes);

            return View(cadetesViewModel);
        }

        public IActionResult RegistrarCadeteForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarCadete(CadeteViewModel cadeteViewModel)
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

        public IActionResult ModificarCadeteForm(int id)
        {
            Cadete cadete = cadetesRepository.GetCadete(id);
            CadeteViewModel cadeteViewModel = _mapper.Map<CadeteViewModel>(cadete);
            return View(cadeteViewModel);
        }

        [HttpPost]
        public IActionResult ModificarCadete(CadeteViewModel cadeteViewModel)
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

        public IActionResult EliminarCadete(int id)
        {
            if (id > 0)
            {
                cadetesRepository.Delete(id);
            }
            return RedirectToAction("Index");
        }
    }
}
