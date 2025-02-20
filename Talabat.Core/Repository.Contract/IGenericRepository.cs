using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repository.Contract
{
    public interface IGenericRepository<T> where T:BaseEntity
    {
        Task<T> GetById(int? id);
        Task<IReadOnlyList<T>> GetAll();
        Task<IReadOnlyList<T>> GetAllBySpecificationAsync(ISpecifications<T> spec);
        Task<T> GetByIdSpecification(ISpecifications<T> spec);
        Task<int> GetCountAsync(ISpecifications<T> spec);
        Task CreatAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
