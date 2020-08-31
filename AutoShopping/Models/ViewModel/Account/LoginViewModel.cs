using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Security;
using System.Threading.Tasks;

namespace AutoShopping.Models.ViewModel.Account
{
    public class LoginViewModel
    {
        [Required, Display(Name= "نام کاربری")]
        public string Username { get; set; }


        [Required, Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "مرا بخاطر بسپار")]
        public bool Remember { get; set; }

        public string ReturnUrl { get; set; }
        public List<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
