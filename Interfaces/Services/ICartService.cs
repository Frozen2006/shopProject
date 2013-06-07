using System.Collections.Generic;
using Helpers;

namespace iTechArt.Shop.Common.Services
{
    public interface ICartService
    {
        void Add(string userEmail, int productId, double count);
        void AddArray(string userEmail, int[] productsId, double[] counts);
        List<ProductInCart> GetAllChart(string userEmail);
        void DeleteProduct(string userEmail, int productId);
        void Clear(string userEmail);
        
        //return finally cost of changed item
        double UpateCount(string userEmail, int productId, double newCount);
        double GetTotalPrice(string userEmail);
        
        //Other methods be realize in future
        //void Buy(string UserEmail);
    }
}
