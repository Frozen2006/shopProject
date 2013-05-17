using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Repositories.DbFirstRepository;

namespace DALConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new ShopContext();
            var prodRep = new ProductRepository(context);
            var catRep = new CategoryRepository(context);

            var prods = prodRep.ReadAll().Take(20).ToArray();

            //foreach (var product in prods)
            //{
            //    Console.WriteLine(product+"\n");
            //}

            var cats = catRep.ReadAll().Take(20).ToArray();

            //foreach (var category in cats)
            //{
            //    Console.WriteLine(category + "\n");
            //}

            var c = cats.Skip(4).First();
            var p = prods.Skip(3).First();

            Console.WriteLine(c);
            Console.WriteLine(p.Category);

            //p.CategoryId = c.Id;
            //prodRep.Update(p);


            Console.Read();
        }
    }
}
