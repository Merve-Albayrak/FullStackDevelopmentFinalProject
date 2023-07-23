
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class Product
    {
      public Guid orderId { get; set; }

      //  public Order Order { get; set; }

        public string name { get; set; }

        public string? picture { get; set; }

        public bool? isOnSale { get; set; }

        public decimal price { get; set; }

        public decimal? salePrice { get; set; }

    }
}
