using iTechArt.Shop.DataAccess.Repositories;
using Entities;
using ProductDescriptionGenerator;

namespace iTechArt.Shop.ProductDescriptionGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var randText = new RandomText();
            var context = new ShopContext();
            var prodRep = new ProductRepository(context);

            int i = 0;
            foreach (var prod in context.Products)
            {
                i++;
                prod.Description = randText.Paragraph(10);
            }

            context.SaveChanges();
        }
    }
}
