using Application.Common.Interfaces;
using Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Features.Products.Commands.Add
{
    public class ProductAddCommandHandler : IRequestHandler<ProductAddCommand, Response<Guid>>
    {


        IApplicationDbContext _applicationDbContext;

        public ProductAddCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        async Task<Response<Guid>> IRequestHandler<ProductAddCommand, Response<Guid>>.Handle(ProductAddCommand request, CancellationToken cancellationToken)
        {


            var product = new Product()
            {

                Picture = request.Picture,
                Name = request.Name,
                OrderId = request.OrderId,
                IsOnSale = request.IsOnSale,
                Price = request.Price,
                SalePrice = request.SalePrice,



            };
            await _applicationDbContext.Products.AddAsync
                (product,cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);


            return new Response<Guid>($"The product name \"{product.Name} successfully added", product.Id);
        }        

    }
}
