﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity,TKey> where TEntity :BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GellAllAsync(bool trackChanges =false);
        Task<IEnumerable<TEntity>> GellAllAsync(ISpecifications<TEntity,TKey> specifications,bool trackChanges =false);

        Task<TEntity?> GetAsync(TKey id);
        Task<TEntity?> GetAsync(ISpecifications<TEntity,TKey> specifications);

        Task AddSync(TEntity entity);

        void Update(TEntity entity);
        void Delete(TEntity entity);




    }
}
