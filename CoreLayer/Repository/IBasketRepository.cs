using CoreLayer.Entities.Basket_Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Repository
{
    public interface IBasketRepository
    {
         Task<CustomerBasket> GetBasketAsync(string basketId);

         Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customerBasket);

         Task<bool> DeleteBasketAsync(string basketId);
    }
}
