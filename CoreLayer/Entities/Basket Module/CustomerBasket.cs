using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities.Basket_Module
{
    public class CustomerBasket
    {
        public string Id { get; set; }

        public List<BasketItem> Items { get; set; }

        public CustomerBasket(string id)
        {
            Id = id;
        }
    }
}
