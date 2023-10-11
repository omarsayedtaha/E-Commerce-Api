using CoreLayer.Entities;
using CoreLayer.Entities.Order_Agregate;
using CoreLayer.Repository;
using CoreLayer.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepo;

        public OrderService(IUnitOfWork unitOfWork,
            IBasketRepository basketRepo
            )
        {
            _unitOfWork = unitOfWork;
            _basketRepo = basketRepo;
         
        }


        public async Task<Order>? CreateOrderAsync(string buyerEmail, string BasketId, int DeliveryMethodeId, Address shippingAddress)
        {
            //1- Get basket From basket Repo
            var basket = await _basketRepo.GetBasketAsync(BasketId);

            //2-Get Selected Items at Basket From Product Repo
            var OrderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productrepo = _unitOfWork.Repository<Product>();
                if (productrepo is not null)
                {
                    var product = await productrepo.GetByIdAsync(item.Id);

                    var ProductItemOrdered = new ProductItemOrdered(item.Id, product.Name, product.PictureUrl);

                    var OrderItem = new OrderItem(ProductItemOrdered, item.Quantity, product.Price);

                    OrderItems.Add(OrderItem);
                }

            }

            //3- Calculate Subtotal 
            var subtotal = OrderItems.Sum(item => item.Quantity * item.Price);

            //4-Get delivery Method from delivery Methode Repo
            var deliveryMethodeRepo = _unitOfWork.Repository<DeliveryMethode>();

            if (deliveryMethodeRepo is not null)
            {
                var deliveryMethode = await deliveryMethodeRepo.GetByIdAsync(DeliveryMethodeId);

              //5-Create Order 

                var Order = new Order(buyerEmail, shippingAddress, OrderItems, deliveryMethode, subtotal);

                var orderRepo = _unitOfWork.Repository<Order>();
                if (orderRepo is not null)
                   await orderRepo.Add(Order);

                //6-Save Order  To Database 
                 var result  = await _unitOfWork.CompleteAsync();

                if (result >0)
                  return Order;
            }


            return null ;  
        }
    }
}
