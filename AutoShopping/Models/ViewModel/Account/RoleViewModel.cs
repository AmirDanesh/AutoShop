using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShopping.Models.ViewModel.Account
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        [Required,Display(Name = "نام نقش")]
        public string Name { get; set; }
    }
}
