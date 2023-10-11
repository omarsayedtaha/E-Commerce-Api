using CoreLayer.Entities;
using CoreLayer.Repository;
using CoreLayer.Specification;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class GenericRepository<T> :IGenericRepository<T>  where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            if (typeof(T)==typeof(Product))
             return  (IReadOnlyList<T>)await _context.Products.Include(P=>P.ProductType).Include(P=>P.ProductBrand).ToListAsync();   
            else
                return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public  IQueryable<T> ApplySpecification(ISpecification<T>spec)
        {
            var query = SpecifictionEvaluator<T>.CreateQuery(_context.Set<T>(), spec);

            return query ;
        }

        public async Task<IReadOnlyList<T>> GetAllWihtSpecAsync(ISpecification<T> spec)
        {
            var result = await ApplySpecification(spec).ToListAsync();

            return result; 
        }

        public async Task<T> GetByIdWihtSpecAsync(ISpecification<T> spec)
        {
            var result = await ApplySpecification(spec).FirstOrDefaultAsync();

            return result;
        }

        public async Task Add(T entity)
           => await _context.Set<T>().AddAsync(entity);

        public void Update(T entity)
         => _context.Set<T>().Update(entity);

        public void Delete(T entity)
          => _context.Set<T>().Remove(entity);
    }
}
