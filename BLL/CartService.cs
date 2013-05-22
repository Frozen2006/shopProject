using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces;

namespace BLL
{
    public class FakeCartService : ICart
    {
        public void Add(string UserEmail, int ProductId, int Count)
        {
            return;
        }
        public List<Helpers.ProductInCart> GetAllChart(string UserEmail)
        {
            throw new NotImplementedException();
        }
        public void DeleteProduct(string UserEmail, int ProductId)
        {
            throw new NotImplementedException();
        }
        public void Clear(string UserEmail)
        {
            throw new NotImplementedException();
        }
        public void UpateCount(string UserEmail, int NewCount)
        {
            throw new NotImplementedException();
        }
        public double GetTotalPrice(string UserEmail)
        {
            throw new NotImplementedException();
        }
        
    }
}
