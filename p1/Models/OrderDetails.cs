using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace p1.Models
{
    public class OrderDetails
    {
        public int OrderDetailsId { get; set; }


        public Product Product { get; set; }


        public int OrderId { get; set; }

        public Order Order { get; set; }

        public string[] productNames { get; set; }

        public int Quantity { get; set; }

       
    }
}
