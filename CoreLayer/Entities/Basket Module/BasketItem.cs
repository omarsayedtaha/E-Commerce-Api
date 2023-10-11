using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities.Basket_Module
{
    public class BasketItem:BaseEntity
    {

        public string ProductName { get; set; }

        //public string Descreption { get; set; }

        public string ProductPictureUrl { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }
        public string Brand { get; set; }

        public string Type { get; set; }


    }
}
