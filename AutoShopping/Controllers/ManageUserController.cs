using System.Linq;
using System.Threading.Tasks;
using AutoShopping.Models.ViewModel.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoShopping.Controllers
{
    public class ManageUserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ManageUserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.Select(s => new UserViewModel()
            {
                Id = s.Id,
                UserName = s.UserName,
                Email = s.Email
            }).ToList();

            return View(users);
        }


        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var user = await _userManager.FindByIdAsync(id);

            if (user == null) return NotFound();

            var data = new UserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(UserViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(viewModel.Id);

                if (user == null) RedirectToAction("Index");

                user.UserName = viewModel.UserName;
                user.Email = viewModel.Email;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded) return RedirectToAction("Index");

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddUserToRole(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var roles = _roleManager.Roles.AsTracking().Select(s => s.Name).ToList();
            var userRoles = await _userManager.GetRolesAsync(user);
            var validRoles = roles.Where(p => userRoles.Contains(p))
                .Select(r => new UserRolesViewModel(r)).ToList();
            var model = new AddUserToRoleViewModel(id,validRoles);

            //foreach (var role in roles)
            //{
            //    if(!await _userManager.IsInRoleAsync(user,role.Name))
            //    {
            //        model.userRoles.Add(new UserRolesViewModel()
            //        {
            //            RoleName = role.Name,
            //            IsSelected = false
            //        });
            //    }
            //    else
            //    {
            //        model.userRoles.Add(new UserRolesViewModel()
            //        {
            //            RoleName = role.Name,
            //            IsSelected = true
            //        });
            //    }
            //}

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUserToRole(AddUserToRoleViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(viewModel.UserId);
                if (user == null) return NotFound();

                var requestRole = viewModel.UserRoles.Where(p => p.IsSelected)
                    .Select(s => s.RoleName)
                    .ToList();

                var result = await _userManager.AddToRolesAsync(user, requestRole);
                if (result.Succeeded) return RedirectToAction("Index");

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var user = await _userManager.FindByIdAsync(id);

            if (user == null) return NotFound();

            var data = new UserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(UserViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(viewModel.Id);
                if (user == null) return NotFound();

                var result = await _userManager.DeleteAsync(user);

                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveUserFromRole(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var roles = await _userManager.GetRolesAsync(user);
            var validRoles = roles.Select(s => new UserRolesViewModel(s)).ToList();

            var model = new AddUserToRoleViewModel(id,validRoles);

            //foreach (var item in roles)
            //{
            //    if(await _userManager.IsInRoleAsync(user,item.Name))
            //    {
            //        model.userRoles.Add(new UserRolesViewModel()
            //        {
            //            RoleName = item.Name
            //        });
            //    }
            //}

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveUserFromRole(AddUserToRoleViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(viewModel.UserId);
                if (user == null) return NotFound();

                var requestRoles = viewModel.UserRoles.Where(p => p.IsSelected)
                    .Select(s => s.RoleName)
                    .ToList();

                var result = await _userManager.RemoveFromRolesAsync(user,requestRoles);

                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
                
            }

            return View(viewModel);
        }

        
    }
}
