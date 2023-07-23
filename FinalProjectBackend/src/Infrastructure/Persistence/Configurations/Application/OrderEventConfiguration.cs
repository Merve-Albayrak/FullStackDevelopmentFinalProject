using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations.Application
{
    public class OrderEventConfiguration : IEntityTypeConfiguration<OrderEvent>
    {
        public void Configure(EntityTypeBuilder<OrderEvent> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x=>x.Order)
                .WithOne(x => x.OrderEvent) 
                .HasForeignKey<OrderEvent>(x => x.OrderId);
            builder.Property(x => x.OrderStatus).HasConversion<int>();
        }
    }
}
