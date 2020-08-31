using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoShopping.Models.ViewModel;
using AutoShopping.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;

namespace AutoShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ModelController : Controller
    {
        private readonly IAutoRepository _repository;
        public ModelController(IAutoRepository repository)
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

            var data = await _repository.GetAllModels();

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> AddModel()
        {
            var result = await _repository.GetAllBrands();
            var obj = new ModelViewModel();

            obj.BrandList = new SelectList(result, "ID", "BrandName");

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> AddModel(ModelViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await _repository.AddNewModel(viewModel);
                return RedirectToAction("Index", new RouteValueDictionary(new { status = result }));
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditModel(int? id)
        {
            if (id != null)
            {
                var data = await _repository.GetModel(id);
                if (data.isAccept)
                {
                    var obj = await _repository.GetAllBrands();
                    data.Model.BrandList = new SelectList(obj, "ID", "BrandName");
                    return View(data.Model);
                }
                return RedirectToAction("Index", new RouteValueDictionary(new { status = data.isAccept }));
            }

            return RedirectToAction("Index", new RouteValueDictionary(new { status = false }));
        }

        [HttpPost]
        public async Task<IActionResult> EditModel(ModelViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await _repository.UpdateModel(viewModel);

                return RedirectToAction("Index", new RouteValueDictionary(new { status = result }));
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteModel(int? id)
        {
            if (id != null)
            {
                var data = await _repository.GetModel(id);
                if (data.isAccept)
                    return View(data.Model);
                return RedirectToAction("Index", new RouteValueDictionary(new { status = data.isAccept }));
            }

            return RedirectToAction("Index", new RouteValueDictionary(new { status = false }));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteModel(ModelViewModel viewModel)
        {
            var result = await _repository.DeleteModel(viewModel);
            return RedirectToAction("Index", new RouteValueDictionary(new { status = result }));
        }
    }
}