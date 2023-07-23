using Application.Common.Interfaces;
using Application.Features.Products.Commands.Add;
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
    public class CreateOrderCommandHandler:IRequestHandler<CreateOrderCommand,Response<Guid>>
    {
        IApplicationDbContext _applicationDbContext;

        public CreateOrderCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Response<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order()
            {
                ExpectedCount = request.ExpectedCount,

                StartingDate = (DateTimeOffset)request.StartingDate,
                UserId = request.UserId.ToString()
             

            };
            await _applicationDbContext.Orders.AddAsync(order,cancellationToken);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new Response<Guid>($"The order successfully created", order.Id);

        }
    }
}
