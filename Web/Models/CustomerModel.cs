using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;


namespace Web.Models
{
    public class CustomerModel
    {
        [Required]
        public string Id { get; set; }

        
        public string Password { get; set; }

        
        [StringLength(50)]
        public string Fullname { get; set; }

        
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Photo { get; set; }

        public bool Activated { get; set; }

        public bool Admin { get; set; }
    }
}