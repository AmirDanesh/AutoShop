using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShopping.Models.ViewModel.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "وارد نموندن {0} اجباری است")]
        [Display(Name = "نام کاربری")]
        [Remote("IsUserNameInUse","Account", AdditionalFields = "__RequestVerificationToken", HttpMethod = "POST")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "وارد نموندن {0} اجباری است")]
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "فرمت {0} نادرست است")]
        [Remote("IsEmailInUse", "Account",AdditionalFields = "__RequestVerificationToken", HttpMethod = "POST")]
        public string Email { get; set; }

        [Required(ErrorMessage = "وارد نموندن {0} اجباری است")]
        [Display(Name = "رمزعبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "وارد نموندن {0} اجباری است")]
        [Display(Name = "تکرار رمزعبور")]
        [Compare(nameof(Password),ErrorMessage = "{1} با {0} مطابقت ندارد")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
