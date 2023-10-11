using CoreLayer.Entities;
using CoreLayer.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Specification
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Crieteria { get; set ; }
        public List<Expression<Func<T, object>>> Includes { get ; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get ; set; }
        public Expression<Func<T, object>> OrderByDescending { get; set ; }
        public int Skip { get ; set ; }
        public int Take { get ; set ; }
        public bool ispaginationEnabled { get ; set ; }

        public BaseSpecification()
        {

        }

        public BaseSpecification(Expression<Func<T,bool>> CriteriaExpression)
        {
            Crieteria = CriteriaExpression; 
        }

        public void ApplyPagination(int skip, int take)
        {
            ispaginationEnabled = true;
            Skip = skip;
            Take = take;    
        }
    }
}
