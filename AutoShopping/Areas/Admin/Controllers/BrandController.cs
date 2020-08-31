using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoShopping.Models.ViewModel;
using AutoShopping.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace AutoShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BrandController : Controller
    {
        private readonly IAutoRepository _repository;
        public BrandController(IAutoRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index(bool? status)
        {
            if(status != null)
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

            var data = await _repository.GetAllBrands();
            return View(data);
        }

        public IActionResult Addbrand() => View();

        [HttpPost]
        public async Task<IActionResult> AddBrand(BrandViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await _repository.AddNewBrand(viewModel);
                return RedirectToAction("Index", new RouteValueDictionary(new { status = result }));
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditBrand(int? id)
        {
            if (id != null)
            {
                var data = await _repository.GetBrand(id);
                if (data.isAccept)
                {
                    return View(data.Brand);
                }
                return RedirectToAction("Index", new RouteValueDictionary(new { status = data.isAccept }));
            }

            return RedirectToAction("Index", new RouteValueDictionary(new { status = false }));
        }

        [HttpPost]
        public async Task<IActionResult> EditBrand(BrandViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _repository.UpdateBrand(viewModel);
                return RedirectToAction("Index", new RouteValueDictionary(new { status = true }));
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteBrand(int? id)
        {
            if (id != null)
            {
                var data = await _repository.GetBrand(id);
                if (data.isAccept)
                {
                    return View(data.Brand);
                }
                return RedirectToAction("Index", new RouteValueDictionary(new { status = data.isAccept }));
            }

            return RedirectToAction("Index", new RouteValueDictionary(new { status = false }));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBrand(BrandViewModel viewModel)
        {
            var data = await _repository.DeleteBrand(viewModel);
            return RedirectToAction("Index", new RouteValueDictionary(new { status = data }));
        }
    }
}