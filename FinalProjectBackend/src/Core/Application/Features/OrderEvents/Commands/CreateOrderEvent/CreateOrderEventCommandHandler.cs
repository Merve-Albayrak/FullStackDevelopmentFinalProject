using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderEvents.Commands.CreateOrderEvent
{
    public class CreateOrderEventCommandHandler : IRequestHandler<CreateOrderEventCommand, Response<Guid>>
    {
        IApplicationDbContext _applicationDbContext;

        public CreateOrderEventCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Response<Guid>> Handle(CreateOrderEventCommand request, CancellationToken cancellationToken)
        {
            var orderEvent = new OrderEvent()
            {
               OrderStatus=request.OrderStatus,
               OrderId=request.OrderId,


            };
            await _applicationDbContext.OrderEvents.AddAsync(orderEvent, cancellationToken);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new Response<Guid>($"The Order Event successfully created", orderEvent.Id);
        }
    }
}
