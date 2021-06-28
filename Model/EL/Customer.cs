namespace Model.EL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.ComponentModel;

    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Orders = new HashSet<Order>();
        }
        [Display(Name = "Tài Khoản: ")]
        [StringLength(20)]
        public string Id { get; set; }


        [Required(ErrorMessage = "Xin Vui Lòng Nhập Mật Khẩu")]
        [StringLength(maximumLength: 16, MinimumLength = 4, ErrorMessage = "Độ dài mật khẩu từ 4-16 kí tự")]
        [Display(Name = "Mật khẩu: ")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Xin Vui Lòng Nhập Tên Đầy Đủ Của Bạn")]

        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "ID error.")]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Password Eror.")]

        [StringLength(50)]
        [Display(Name ="Họ và Tên:")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Xin Vui Lòng Nhập Địa Chỉ Email ")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email not a valid email.")]
        [StringLength(50)]
        public string Photo { get; set; }

        public bool Activated { get; set; }

        public bool Admin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
