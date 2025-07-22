using Domain.Contracts;
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

        public async Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await ApplySpecifications(specifications).CountAsync();
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

        public async Task<IEnumerable<TEntity>> GellAllAsync(ISpecifications<TEntity, TKey> specifications, bool trackChanges = false)
        {
          return await  SpecificationEvaluator.GetQuery(_context.Set<TEntity>(), specifications).ToListAsync();
        }

        public async Task<TEntity?> GetAsync(TKey id)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return await _context.Products.Include(P => P.ProductBrand).Include(P => P.ProductType).FirstOrDefaultAsync(P => P.Id == id as int?) as TEntity;
                    }
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity?> GetAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await SpecificationEvaluator.GetQuery(_context.Set<TEntity>(), specifications).FirstOrDefaultAsync();
        }

        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }
        private IQueryable<TEntity> ApplySpecifications(ISpecifications<TEntity,TKey> specifications)
        {
            return SpecificationEvaluator.GetQuery(_context.Set<TEntity>(), specifications);
        }
    }
}
