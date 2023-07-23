
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class CreateOrderEventModel:EntityBase<Guid>
    {
        public Guid OrderId { get; set; }



        public OrderStatus OrderStatus { get; set; }
    }
}
