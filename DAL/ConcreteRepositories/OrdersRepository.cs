﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories.DbFirstRepository;
using Entities;
using Interfaces;

namespace DAL.membership
{
    public class OrdersRepository : RepositoryBase<Order>, IOrdersRepository
    {
        public OrdersRepository(DbContext context)
            : base(context)
        {
        }


        public override void Update(Order tiem)
        {
            Order ord = CurrentDbSet.FirstOrDefault(m => m.Id == tiem.Id);

            if (ord != null)
            {
                ord.Buyes = tiem.Buyes;
                ord.DeliverySpot = tiem.DeliverySpot;
                ord.Status = tiem.Status;
                ord.User = tiem.User;
                
            }
            Context.SaveChanges();
        }
    }
}
