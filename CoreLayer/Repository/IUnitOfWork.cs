using CoreLayer.Entities;
using CoreLayer.Entities.Order_Agregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Repository
{
    public interface IUnitOfWork : IAsyncDisposable 
    {


        public IGenericRepository<TEntity>? Repository<TEntity>() where TEntity : BaseEntity; 

        public Task<int> CompleteAsync();


    }
}
