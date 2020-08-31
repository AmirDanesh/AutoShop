using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShopping.Models.ViewModel.Account
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Required,Display(Name = "نام کاربری")]
        public string UserName { get; set; }
        [Required, Display(Name = "ایمیل")]
        public string Email { get; set; }
    }
}
