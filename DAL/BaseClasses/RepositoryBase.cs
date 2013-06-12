using System;
using System.Data.Entity;
using System.Linq;
using iTechArt.Shop.Common.Repositories;
using iTechArt.Shop.Entities;


namespace iTechArt.Shop.DataAccess.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class, IEntity
    {
        protected static DbContext Context { get; set; }
        protected DbSet<T> CurrentDbSet { get; set; }

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

        //Current context object state
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
