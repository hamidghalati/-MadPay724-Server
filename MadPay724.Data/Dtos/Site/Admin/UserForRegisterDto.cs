using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MadPay724.Data.Dtos.Site.Admin
{
  public  class UserForRegisterDto
    {
        [Required]
        [EmailAddress(ErrorMessage ="ایمیل وارد شده صحیح نیست")]
        public string UserName { get; set; }
        [Required]
        [StringLength(12,MinimumLength =4,ErrorMessage ="کلمه عبور باید بین 4 تا 12 رقم باشد")]
        public string Password { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
