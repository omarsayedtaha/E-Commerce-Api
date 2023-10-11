using CoreLayer.Entities;
using CoreLayer.Entities.Order_Agregate;
using CoreLayer.Repository;
using RepositoryLayer.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class UnitOfWork : IUnitOfWork
    {

        public IGenericRepository<Product> ProductsRepo { get ; set ; }
        public IGenericRepository<ProductBrand> ProductBrandsRepo { get ; set ; }
        public IGenericRepository<ProductType> ProductstypesRepo { get ; set ; }
        public IGenericRepository<DeliveryMethode> DeliveryMethodesRepo { get; set ; }
        public IGenericRepository<OrderItem> OrderItemsRepo { get; set; }
        public IGenericRepository<Order> OrdersRepo { get ; set ; }
        public StoreContext DbContext { get; }

        Hashtable _repository; 
        public UnitOfWork(StoreContext dbContext)
        {
            DbContext = dbContext;
        }
        public IGenericRepository<TEntity>? Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repository is  null)
                _repository = new Hashtable(); 

            var type = typeof(TEntity).Name;

            if (!_repository.ContainsKey(type))
            { 
                var repository = new GenericRepository<TEntity>(DbContext) ;
                _repository.Add(type, repository);
            }

            return  _repository[type] as IGenericRepository<TEntity>;
        }

        public async Task<int> CompleteAsync()
          => await DbContext.SaveChangesAsync();
        

        public async ValueTask DisposeAsync()
         => await DbContext.DisposeAsync();

   
    }
}
