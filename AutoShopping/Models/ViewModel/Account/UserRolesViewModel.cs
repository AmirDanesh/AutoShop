using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShopping.Models.ViewModel.Account
{
    public class UserRolesViewModel
    {
        public UserRolesViewModel(string roleName)
        {
            RoleName = roleName;
        }

        public UserRolesViewModel(string roleName, bool isSelected)
        {
            RoleName = roleName;
            IsSelected = isSelected;
        }

        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}
