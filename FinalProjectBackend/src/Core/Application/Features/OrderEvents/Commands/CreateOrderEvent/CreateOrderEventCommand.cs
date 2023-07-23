using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderEvents.Commands.CreateOrderEvent
{
    public class CreateOrderEventCommand:IRequest<Response<Guid>>
    {
        public Guid OrderId { get; set; }

    

        public OrderStatus OrderStatus { get; set; }
    }
}
