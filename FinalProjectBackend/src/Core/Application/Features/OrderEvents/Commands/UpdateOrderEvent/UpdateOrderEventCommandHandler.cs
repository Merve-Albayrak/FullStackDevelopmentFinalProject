using Application.Common.Interfaces;
using Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderEvents.Commands.UpdateOrderEvent
{
    public class UpdateOrderEventCommandHandler : IRequestHandler<UpdateOrderEventCommand, Response<Guid>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public UpdateOrderEventCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<Response<Guid>> Handle(UpdateOrderEventCommand request, CancellationToken cancellationToken)
        {
            var orderEvent = await _applicationDbContext.OrderEvents.Where(x => x.Id == request.Id).FirstOrDefaultAsync();

            if (orderEvent == null)
            {

                throw new ArgumentException(nameof(request.Id));
            }
            else
            {
                orderEvent.OrderStatus = request.OrderStatus;
                // orderEvent.OrderId=request.OrderId;


            }

            _applicationDbContext.OrderEvents.Update(orderEvent);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new Response<Guid>($"The order status was successfully update");
        }
    }
}
