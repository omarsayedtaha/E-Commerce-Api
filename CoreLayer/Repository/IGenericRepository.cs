using CoreLayer.Entities;
using CoreLayer.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Repository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
       public Task<IReadOnlyList<T>> GetAllAsync();

       public Task<T> GetByIdAsync(int id); 

       public Task<IReadOnlyList<T>> GetAllWihtSpecAsync(ISpecification<T> spec);
       public Task<T> GetByIdWihtSpecAsync(ISpecification<T> spec);

       Task Add(T entity);
       void Update(T entity);
       void Delete(T entity);

    }
}
