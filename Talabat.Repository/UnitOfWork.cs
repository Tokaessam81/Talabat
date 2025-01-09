using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contract;
using Talabat.Repository.Data;
using Talabat.Repository.Repositories;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _db;
        private readonly Hashtable _repo;

        public UnitOfWork(StoreDbContext db)
        {
            _db = db;
            _repo = new Hashtable();
        }

        public async Task<int> completeAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _db.DisposeAsync();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            //if Key is not available create object from TEntity
            var Key = typeof(TEntity).Name;
            if (!_repo.ContainsKey(Key))
            {
                var repository=new GenericRepository<TEntity>(_db);
                _repo.Add(Key,repository);
            } 
            return _repo[Key] as IGenericRepository<TEntity>;
        }
    }
}
