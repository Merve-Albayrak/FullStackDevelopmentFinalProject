using Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand:IRequest<Response<Guid>>
    {
        public Guid? Id { get; set; }
        public int? ExpectedCount { get; set; }
        public int OrderStatus { get; set; }
        public DateTimeOffset StartingDate { get; set; }

        public int? FoundedCount { get; set; }

        public DateTimeOffset? FinishingDate { get; set; }
        public Guid? OrderEventId { get; set; }
        public bool? IsFinished { get; set; }

    }
}
