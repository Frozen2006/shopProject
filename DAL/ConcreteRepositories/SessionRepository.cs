using System.Data.Entity;
using System.Linq;
using AutoMapper;
using iTechArt.Shop.Common.Repositories;
using iTechArt.Shop.DataAccess.Base;
using iTechArt.Shop.Entities;

namespace iTechArt.Shop.DataAccess.ConcreteRepositories
{
    public class SessionRepository : RepositoryBase<Session>, ISessionRepository
    {
        public SessionRepository(DbContext context)
            : base(context)
        {
        }


        public override void Update(Session item)
        {
            Session session = CurrentDbSet.FirstOrDefault(m => m.Id == item.Id);

            if (session != null)
            {
                session = Mapper.Map(item, session);

                Context.SaveChanges();
            }

        }
    }
}
