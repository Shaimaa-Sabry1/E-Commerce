using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> GetQuery<TEntity,TKey>(IQueryable<TEntity> inputQuery,ISpecifications<TEntity,TKey> specifications)
            where TEntity:BaseEntity<TKey>
        {
            var query = inputQuery;

            if(specifications.Criterias is not null)
            {
               query= query.Where(specifications.Criterias);
            }
            if(specifications.OrderBy is not null)
            {
                query = query.OrderBy(specifications.OrderBy);
            }
            else if(specifications.OrderByDescending is not null)
            {
                query = query.OrderByDescending(specifications.OrderByDescending);
            }
            if(specifications.IsPagination)
            {
                query = query.Skip(specifications.Skip).Take(specifications.Take);
            }
            query = specifications.IncludeExpretion.Aggregate(query, (currentQuery, includeExpresion) => currentQuery.Include(includeExpresion));
            return query;
        }
    }
}
