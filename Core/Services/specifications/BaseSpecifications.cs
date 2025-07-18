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
        public BaseSpecifications(Expression<Func<TEntity, bool>>? expression)
        {
            Criterias = expression;
        }

        protected void AddInclude(Expression<Func<TEntity, object>> expressions)
        {
            IncludeExpretion.Add(expressions);
        }
    }
}
