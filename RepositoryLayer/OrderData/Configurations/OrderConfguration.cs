using CoreLayer.Entities.Order_Agregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.OrderData.Configurations
{
    internal class OrderConfguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShippingAddress, ShippingAddress => ShippingAddress.WithOwner());

            builder.Property(O => O.status)
                .HasConversion(

                 OStatus => OStatus.ToString(),
                 OStatus => (OrderStatus) Enum.Parse(typeof(OrderStatus),OStatus)
                 );

            builder.Property(O => O.Subtotal).HasColumnType("decimal(18,2)");
        }
    }
}
