using CoreLayer.Entities.Order_Agregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
    public interface IOrderService
    {
        public Task<Order>? CreateOrderAsync(string buyerEmail, string BasketId, int DeliveryMethodeId,Address shippingAddress);
    }
}
