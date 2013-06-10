using System.Collections.Generic;
using iTechArt.Shop.Entities;
using iTechArt.Shop.Entities.PresentationModels;

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

        string GetAddingReport(Product product, double count);
        string GetAddingReport(Product[] products, double[] counts);

        //Other methods be realize in future
        //void Buy(string UserEmail);
    }
}
