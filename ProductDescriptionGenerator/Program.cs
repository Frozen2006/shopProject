using DAL.Repositories.DbFirstRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ProductDescriptionGenerator
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
