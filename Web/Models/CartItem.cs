using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    [Serializable]
    public class CartItem
    {
        public long ProductId { get; set; }
        public string name { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
        public double Disc { get; set; }

        public double getAmount()
        {
            return Qty * Price * (1 - Disc);
        }
    }
}