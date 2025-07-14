using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
   public interface IUnitOfWork
    {
        Task<int> savesChangeAsync();

        //Generate Any Of Repository

        IGenericRepository<TEntity, TKey> GetGenericRepository<TEntity, TKey>() where TEntity:BaseEntity<TKey>;
    }
}
