using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;

namespace Interfaces
{
    public interface ICart
    {
        void Add(string UserEmail, int ProductId, int Count);
        List<Helpers.ProductInCart> GetAllChart(string UserEmail);
        void DeleteProduct(string UserEmail, int ProductId);
        void Clear(string UserEmail);
        void UpateCount(string UserEmail, int NewCount);
        double GetTotalPrice(string UserEmail);
        
        //Other methods be realize in future
        //void Buy(string UserEmail);
    }
}
