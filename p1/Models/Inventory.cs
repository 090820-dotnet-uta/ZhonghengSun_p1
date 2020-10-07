﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace p1.Models
{
    public class Inventory
    {

        [Key]
        public int InventoryId
        {
            get; set;
        }

       
        public int ProductId
        {
            get; set;
        }

        public Product Product
        {
            get; set;
        }


        public int StoreId { get; set; }
        public Store Store
        {
            get;
            set;
        }

        

        public int Quantity
        {
            get; set;
        }


    }
}
