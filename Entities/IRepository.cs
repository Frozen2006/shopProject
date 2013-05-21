using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
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
