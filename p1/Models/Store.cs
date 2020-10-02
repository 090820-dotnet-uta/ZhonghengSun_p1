using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace p1.Models
{
    public class Store
    {
        [Key]
        public int StoreId { get; set; }

        public string StoreName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public List<Order> Orders { get; set; }

        public List<Inventory> Inventories { get; set; }
    }
}
