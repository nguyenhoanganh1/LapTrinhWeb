using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage ="New Pass word required",AllowEmptyStrings =false)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật Khẩu")]
        public string NewPassWord { set; get; }

        [DataType(DataType.Password)]
        [Display(Name ="Xác Nhận Mật Khẩu")]
        [Compare("NewPassWord",ErrorMessage ="New Password and confirm password does not match")]
        public string ConfimPassword { set; get; }

        [Required]
        public string ResetCode { set; get;}
    }
}