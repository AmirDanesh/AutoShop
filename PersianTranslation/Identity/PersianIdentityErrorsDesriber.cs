using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersianTranslation.Identity
{
    class PersianIdentityErrorsDesriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateEmail(string email)
            => new IdentityError()
            {
                Code = nameof(DuplicateEmail),
                Description = $"mahsa '{email}' قبلا توسط شخص دیگری انتخاب شده است"
            };

        public override IdentityError InvalidUserName(string userName)
        {
            return base.InvalidUserName(userName);
        }
    }
}
