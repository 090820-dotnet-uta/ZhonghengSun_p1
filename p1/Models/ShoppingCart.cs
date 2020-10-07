using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace p1.Models
{
    public class ShoppingCart
    {
       
       public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }

        
    }
}
