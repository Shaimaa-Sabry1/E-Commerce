﻿using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly EcommerceDbContext _context;

        public GenericRepository(EcommerceDbContext context)
        {
            this._context = context;
        }
        public async Task AddSync(TEntity entity)
        {
            await _context.AddAsync(entity);

        }

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GellAllAsync(bool trackChanges = false)
        {
            if(typeof(TEntity)==typeof(Product))
            {
                return trackChanges ?
                            await _context.Products.Include(P=>P.ProductBrand).Include(P=>P.ProductType).ToListAsync() as IEnumerable<TEntity>

                            : await _context.Products.Include(P => P.ProductBrand).Include(P => P.ProductType).ToListAsync() as IEnumerable<TEntity>;


            }
            return trackChanges?
                             await _context.Set<TEntity>().ToListAsync()
                            
                             :await _context.Set<TEntity>().AsNoTracking().ToListAsync();

        }

        public async Task<TEntity?> GetAsync(TKey id)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return await _context.Products.Include(P => P.ProductBrand).Include(P => P.ProductType).FirstOrDefaultAsync(P => P.Id == id as int?) as TEntity;
                    }
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }
    }
}
