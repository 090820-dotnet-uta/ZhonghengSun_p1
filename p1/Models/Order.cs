using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace p1.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        //   public string[] ProductName { get; set; }

        // public string[] Quantity { get; set; }
        public OrderDetails Details { get; set; }
        public DateTime OrderTime { get; set; }

        public double totalamount { get; set; }
    }
}
