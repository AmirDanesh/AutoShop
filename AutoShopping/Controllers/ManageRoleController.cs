﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoShopping.Models.ViewModel.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AutoShopping.Controllers
{
    [Authorize]
    public class ManageRoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public ManageRoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();

            return View(roles);
        }

        [HttpGet]
        public IActionResult AddRole() => View();

        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var role = new IdentityRole(viewModel.Name);
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded) return RedirectToAction("Index");

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var role = await _roleManager.FindByIdAsync(id);
            
            if (role == null) return NotFound();

            var data = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(RoleViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(viewModel.Id);
                if (role == null) return NotFound();

                role.Name = viewModel.Name;

                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded) return RedirectToAction("Index");

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRole(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null) return NotFound();

            var data = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRole(RoleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(viewModel.Id);
                if (role == null) return NotFound();

                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded) return RedirectToAction("Index");

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }

            return View(viewModel);
        }
    }
}