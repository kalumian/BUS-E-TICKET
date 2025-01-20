using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        Task AddAsync(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
        IEnumerable<T> GetAll();
        T? GetById(object id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync();
        T? FirstOrDefault(Expression<Func<T, bool>> predicate);
        int Count();
        bool Exists(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAllQueryable();

    }
}
