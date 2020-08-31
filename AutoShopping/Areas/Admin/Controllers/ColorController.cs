using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoShopping.Models;
using AutoShopping.Models.ViewModel;
using AutoShopping.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

namespace AutoShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ColorController : Controller
    {
        private readonly IAutoRepository _repository;
        public ColorController(IAutoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index(bool? status)
        {
            if (status != null)
            {
                if (status == true)
                {
                    ViewBag.success = true;
                }
                else if (status == false)
                {
                    ViewBag.success = false;
                }
            }

            var data = await _repository.GetAllColor();

            return View(data);
        }

        [HttpGet]
        public IActionResult AddColor() => View();

        [HttpPost]
        public async Task<IActionResult> AddColor(ColorViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var result = await _repository.AddNewColor(viewModel);
                if (result)
                    return RedirectToAction("Index", new RouteValueDictionary(new { status = result }));

                return View(viewModel);
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditColor(int? id)
        {
            if(id != null)
            {
                var data = await _repository.GetColor(id);
                if(data.isAccept)
                    return View(data.Color);
                return RedirectToAction("Index", new RouteValueDictionary(new { Status = data.isAccept }));
            }
            return RedirectToAction("Index", new RouteValueDictionary(new { Status = false }));
        }

        [HttpPost]
        public async Task<IActionResult> EditColor(ColorViewModel colorView)
        {
            if(ModelState.IsValid)
            {
                var data = await _repository.UpdateColor(colorView);
                if (data)
                    return RedirectToAction("Index", new RouteValueDictionary(new { status = data }));
            }
            return View(colorView);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteColor(int? id)
        {
            if(id != null)
            {
                var data = await _repository.GetColor(id);
                if (data.isAccept)
                    return View(data.Color);
                return RedirectToAction("Index", new RouteValueDictionary(new { Status = data.isAccept }));
            }
            return RedirectToAction("Index", new RouteValueDictionary(new { Status = false }));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteColor(ColorViewModel colorView)
        {
            if(ModelState.IsValid)
            {
                var result = await _repository.DeleteColor(colorView);
                if (result)
                    return RedirectToAction("Index", new RouteValueDictionary(new { status = result }));
                return View(colorView);
            }
            return RedirectToAction("Index", new RouteValueDictionary(new { status = false }));
        }

    }
}