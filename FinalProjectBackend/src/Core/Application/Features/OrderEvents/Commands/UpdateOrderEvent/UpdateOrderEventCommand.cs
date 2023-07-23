using Domain.Common;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderEvents.Commands.UpdateOrderEvent
{
    public class UpdateOrderEventCommand:IRequest<Response<Guid>>
    {
       // public Guid OrderId { get; set; }
        public Guid Id { get; set; }



        public OrderStatus OrderStatus { get; set; }
    }
}
