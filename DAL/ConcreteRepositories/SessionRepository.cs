using System.Data.Entity;
using System.Linq;
using iTechArt.Shop.Common.Repositories;
using iTechArt.Shop.Entities;

namespace iTechArt.Shop.DataAccess.Repositories
{
    public class SessionRepository : RepositoryBase<Session>, ISessionRepository
    {
        public SessionRepository(DbContext context)
            : base(context)
        {
        }


        public override void Update(Session tiem)
        {
            Session session = CurrentDbSet.FirstOrDefault(m => m.Id == tiem.Id);

            if (session != null)
            {
                session.Guid = tiem.Guid;
                session.User = tiem.User;

                Context.SaveChanges();
            }

        }
    }
}
