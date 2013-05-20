﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories.DbFirstRepository;

namespace DAL.membership
{
    public class SessionRepository : RepositoryBase<Session>
    {
        public SessionRepository(ShopContext context)
            : base(context)
        {
        }


        public override void Update(Session tiem)
        {
            Session session = CurrentDbSet.FirstOrDefault(m => m.Id == tiem.Id);

            if (session != null)
            {
                session.guid = session.guid;
                session.User = session.User;

                Context.SaveChanges();
            }

        }
    }
}
