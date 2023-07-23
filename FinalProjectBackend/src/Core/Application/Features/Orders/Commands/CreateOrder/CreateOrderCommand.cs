using Domain.Common;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Commands.CreateOrder
{
    //aslında response döncek
    public class CreateOrderCommand:IRequest<Response<Guid>>
      

    {
        public int? ExpectedCount { get; set; }
        public int? OrderStatus { get; set; }
        public DateTimeOffset? StartingDate { get; set; }

        public int? FoundedCount { get; set; }
        public string? ProductType { get; set; }
        public string? AllOrCount { get; set; }
        public DateTimeOffset? FinishingDate { get; set; }
        public Guid? OrderEventId { get; set; }
        public Guid? UserId { get; set; }
      
        public bool? IsFinished { get; set; }

    }
}
