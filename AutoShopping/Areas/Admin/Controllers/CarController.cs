using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoShopping.Models.ViewModel;
using AutoShopping.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace AutoShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CarController : Controller
    {
        private readonly IAutoRepository _repository;
        private readonly IHostingEnvironment _env;
        public CarController(IAutoRepository repository, IHostingEnvironment env)
        {
            _repository = repository;
            _env = env;
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

            var data = await _repository.GetAllCar();
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> AddCar()
        {
            var model = await _repository.GetAllModels();
            var brand = await _repository.GetAllBrands();
            var color = await _repository.GetAllColor();

            var obj = new CarViewModel()
            {
                BrandList = new SelectList(brand, "ID", "BrandName"),
                ColorList = new SelectList(color, "ID", "ColorName"),
                ModelList = new SelectList(model, "ID", "ModelName")
            };

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> AddCar(CarViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.CarImages = HttpContext.Request.Form.Files;

                var result = await _repository.AddCar(viewModel);
                if (result)
                    return RedirectToAction("Index", new RouteValueDictionary(new { status = result }));
            }

            var model = await _repository.GetAllModels();
            var brand = await _repository.GetAllBrands();
            var color = await _repository.GetAllColor();

            var obj = new CarViewModel()
            {
                BrandList = new SelectList(brand, "ID", "BrandName"),
                ColorList = new SelectList(color, "ID", "ColorName"),
                ModelList = new SelectList(model, "ID", "ModelName")
            };


            return View(obj);
        }

        [HttpGet]
        public async Task<IActionResult> EditCar(int? id)
        {
            var model = await _repository.GetAllModels();
            var brand = await _repository.GetAllBrands();
            var color = await _repository.GetAllColor();

            if (id != null)
            {
                var data = await _repository.GetCar(id);
                if (data.isAccept)
                {
                    data.CarViewModel.ModelList = new SelectList(model, "ID", "ModelName");
                    data.CarViewModel.BrandList = new SelectList(brand, "ID", "BrandName");
                    data.CarViewModel.ColorList = new SelectList(color, "ID", "ColorName");

                    return View(data.CarViewModel);
                }
            }
            return RedirectToAction("Index", new RouteValueDictionary(new { status = false }));
        }

        [HttpPost]
        public async Task<IActionResult> EditCar(CarViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var data = await _repository.UpdateCar(viewModel);
                if (data)
                {
                    return RedirectToAction("Index", new RouteValueDictionary(new { status = data }));
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> FilterModels(int brand)
        {
            var model = await _repository.GetAllModels();
            model = model.Where(p => p.BrandID == brand).ToList();

            return Json(JsonConvert.SerializeObject(model));
        }


        [HttpGet]
        public async Task<IActionResult> DeleteCar(int? id)
        {
            if (id != null)
            {
                var result = await _repository.GetCar(id);
                if (result.isAccept)
                {
                    return View(result.CarViewModel);
                }
            }

            return RedirectToAction("Index", new RouteValueDictionary(new { status = false }));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCar(CarViewModel viewModel)
        {
            var data = await _repository.DeleteCar(viewModel);
            return RedirectToAction("Index", new RouteValueDictionary(new { status = data }));
        }
    }
}