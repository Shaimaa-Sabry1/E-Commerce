using Domain.Contracts;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.specifications
{
    public class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey>  
        where TEntity:BaseEntity<TKey>
    {
        public Expression<Func<TEntity, bool>>? Criterias { get; set; }
        public List<Expression<Func<TEntity, object>>> IncludeExpretion { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, object>>? OrderBy { get; set; }
        public Expression<Func<TEntity, object>>? OrderByDescending { get ; set; }

        public BaseSpecifications(Expression<Func<TEntity, bool>>? expression)
        {
            Criterias = expression;
        }

        protected void AddInclude(Expression<Func<TEntity, object>> expressions)
        {
            IncludeExpretion.Add(expressions);
        }
        protected void AddOrderBy(Expression<Func<TEntity, object>> expression)
        {
            OrderBy = expression;
        }

        protected void AddOrderByDescending(Expression<Func<TEntity, object>> expression)
        {
            OrderByDescending = expression;
        }


    }
}
