using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Model.EL;

namespace Web.Models
{
    public class LoginCus
    {
        public class LoginModel
        {
            [Key]
            [Display(Name = "Email")]
            public string Email { get; set; }
            [Display(Name = "Password")]
            public string Password { get; set; }
        }
    }
}
