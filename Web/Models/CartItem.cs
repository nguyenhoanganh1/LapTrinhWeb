using Model.EL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    [Serializable]
    public class CartItem
    {
        public Product product { get; set; }
        public int quantity { get; set; }
    }
}