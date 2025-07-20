﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{ 
   public interface ISpecifications<TEntity,TKey> where TEntity:BaseEntity<TKey>
    {
        public Expression<Func<TEntity,bool>>? Criterias { get; set; }
        public List<Expression<Func<TEntity,object>>> IncludeExpretion { get; set; }

        public Expression<Func<TEntity,object>>? OrderBy { get; set; }
        public Expression<Func<TEntity,object>>? OrderByDescending { get; set; }
    }
}

