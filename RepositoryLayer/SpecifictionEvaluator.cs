using CoreLayer.Entities;
using CoreLayer.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class SpecifictionEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> CreateQuery(IQueryable<T> inputQuery ,ISpecification<T> spec )
        {
            var query = inputQuery;

            if (spec.Crieteria is not null)
                query = query.Where(spec.Crieteria);
           
            if (spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);

            if (spec.OrderByDescending is not null)
                query = query.OrderByDescending(spec.OrderByDescending);

            if (spec.ispaginationEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);


            if (spec.Includes is not null)
                query = spec.Includes.Aggregate(query, (initialquery, IncludeExpression) => initialquery.Include(IncludeExpression));

            return query; 
        }

    }
}
