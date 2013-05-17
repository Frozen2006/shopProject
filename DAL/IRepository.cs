using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IRepository<T> : IDisposable
    {
        void Create(T item);
        void Delete(T item);
        T Read(Guid id);
        void Update(T item);
        IQueryable<T> ReadAll();
    }
}
