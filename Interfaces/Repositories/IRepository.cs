using System;
using System.Linq;

namespace iTechArt.Shop.Common.Repositories
{
    public interface IRepository<T> : IDisposable
    {
        void Create(T item);
        void Delete(T item);
        T Read(int id);
        void Update(T item);
        IQueryable<T> ReadAll();
    }
}
