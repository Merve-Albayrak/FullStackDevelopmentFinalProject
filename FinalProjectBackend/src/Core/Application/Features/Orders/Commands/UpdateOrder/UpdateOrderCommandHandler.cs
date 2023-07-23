using Application.Common.Interfaces;
using Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Response<Guid>>
    {

        private readonly IApplicationDbContext _applicationDbContext;

        public UpdateOrderCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<Response<Guid>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _applicationDbContext.Orders.Where(x => x.Id == request.Id).FirstOrDefaultAsync();

            if (order == null)
            {

                throw new ArgumentException(nameof(request.Id));
            }
            else
            {

                order.StartingDate = request.StartingDate;
                order.FinishingDate= request.FinishingDate;
                order.IsFinished= request.IsFinished;
                order.OrderEventId = request.OrderEventId;
                order.ExpectedCount = request.ExpectedCount;
                order.FoundedCount = request.FoundedCount;
                
            }
           
            _applicationDbContext.Orders.Update(order);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new Response<Guid>($"The order status was successfully update");
        }
    }
}
