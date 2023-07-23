using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order:EntityBase<Guid>
    {
        
        public string UserId { get; set; }
       // public string? ProductType { get; set; }
        public  int? ExpectedCount { get; set; }
        public  int? FoundedCount { get; set; }
        public DateTimeOffset StartingDate { get; set; }
        public DateTimeOffset? FinishingDate { get; set; }
        public Guid? OrderEventId { get; set; }
        public OrderEvent OrderEvent { get; set; }

        public List<Product>? Products { get; set; }
        public bool? IsFinished { get; set; } 

        //   public Order Order { get; set; }

        //public OrderStatus Status { get; set; }
    }
}
