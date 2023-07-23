using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetAll
{
    public class ProductGetAllQueryHandler : IRequestHandler<ProductGetAllQuery, List<ProductGetAllDto>>
    {
        IApplicationDbContext _applicationDbContext;

        public ProductGetAllQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<ProductGetAllDto>> Handle(ProductGetAllQuery request, CancellationToken cancellationToken)
        {
           var dbQuery= _applicationDbContext.Products.AsQueryable();
            var products= await dbQuery
                .Select(x=>MapToDto(x)) 
                .ToListAsync(cancellationToken);


            return products;

        }
        private static ProductGetAllDto MapToDto(Product product)
        {


            return new ProductGetAllDto()
            {

                OrderId = product.OrderId,
                ProductId = product.Id,
                Name = product.Name,
                IsOnSale = product.IsOnSale,
                Picture = product.Picture,
                Price = product.Price,
                SalePrice = product.SalePrice,


            };
        }
    }
}
