using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShopping.Models.ViewModel.Account
{
    public class AddUserToRoleViewModel
    {
        public AddUserToRoleViewModel()
        {
            UserRoles = new List<UserRolesViewModel>();
        }

        public AddUserToRoleViewModel(string userId, List<UserRolesViewModel> userRoles)
        {
            UserId = userId;
            UserRoles = new List<UserRolesViewModel>();
        }

        public string UserId { get; set; }
        public List<UserRolesViewModel> UserRoles { get; set; }
    }
}
