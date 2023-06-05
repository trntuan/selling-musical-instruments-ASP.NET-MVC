using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ch_nhac_cu.Models
{
    public class CartModel
    {
        public NhacCu Product { get; set; }
        public int Quantity { get; set; }
    }
}