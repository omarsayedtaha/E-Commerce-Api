using CoreLayer.Entities.Order_Agregate;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order = CoreLayer.Entities.Order_Agregate.Order;

namespace CoreLayer.Specification
{
    public class OrderSpecification:BaseSpecification<Order>
    {
        public OrderSpecification(string email):base(O=>O.BuyerEmail == email)
        {
            Includes.Add(O => O.DeliveryMethode);
            Includes.Add(O => O.Items);

            OrderByDescending = O => O.OrderCreationTime; 
        }
        public OrderSpecification(string email, int Id) :
            base(O => O.BuyerEmail == email && O.Id==Id)
        {
            Includes.Add(O => O.DeliveryMethode);
            Includes.Add(O => O.Items);
        }
    }
}
