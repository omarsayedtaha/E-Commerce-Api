using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities.Order_Agregate
{
    public class Order:BaseEntity
    {
        public Order()
        {

        }
        public Order(string buyerEmail, Address shippingAddress, ICollection<OrderItem> items, DeliveryMethode deliveryMethode, decimal subtotal)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            Items = items;
            DeliveryMethode = deliveryMethode;
            Subtotal = subtotal;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderCreationTime { get; set; } = DateTimeOffset.Now;

        public OrderStatus status { get; set; } = OrderStatus.Pending;

        public Address ShippingAddress { get; set; }


        public ICollection<OrderItem> Items { get; set; }= new HashSet<OrderItem>();


        public DeliveryMethode DeliveryMethode { get; set; }

        public decimal Subtotal { get; set; }

        public decimal GetTolal() => Subtotal + DeliveryMethode.Cost; 
    }
}
