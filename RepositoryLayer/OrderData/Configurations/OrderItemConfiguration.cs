using CoreLayer.Entities.Order_Agregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.OrderData.Configurations
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(O=>O.Product , Product=>Product.WithOwner());

            builder.Property(O => O.Price).HasColumnType("decimal(18,2)"); 
        }
    }
}
