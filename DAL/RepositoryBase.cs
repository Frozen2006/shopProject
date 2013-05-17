using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.DbFirstRepository
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class, IEntity
    {
        //Вот этот контекст должен назначаться Ninject'ом. Поэтому попросим его в конструктор
        protected static DbContext Context { get; set; }
        protected DbSet<T> CurrentDbSet { get; set; }

        //И пускай теперь Ninject сам думает, как сделать, чтобы контекст был один.
        protected RepositoryBase(DbContext context)
        {
            Context = context;
            CurrentDbSet = Context.Set<T>();
        }
        public void Create(T item)
        {
            CurrentDbSet.Add(item);
            Context.SaveChanges();
        }
        public  void Delete(T item)
        {
            CurrentDbSet.Remove(item);
            Context.SaveChanges();
        }
        public T Read(int id)
        {
            return CurrentDbSet.FirstOrDefault(i => i.Id == id);
        }
        public abstract void Update(T tiem);

        public IQueryable<T> ReadAll()
        {
            return CurrentDbSet;
        }

        public IQueryable<T> Select(Predicate<T> predicate)
        {
            var query = from i in CurrentDbSet where predicate(i) select i;
            return query;
        }
        //Необходим нормальный Unit of Work, или хотябы человеческий Dispose
        //там нужна какая-то проверка


        //state of current object
        private bool _isDisposed = false;

        public void Dispose()
        {
            if (!_isDisposed)
            {
                Context.Dispose();
                _isDisposed = true;
            }
        }
    }
}
