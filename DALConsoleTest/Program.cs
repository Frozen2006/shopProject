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

            var prods = prodRep.ReadAll().Take(20).ToArray();

            foreach (var product in prods)
            {
                Console.WriteLine(product);
            }

            Console.Read();
        }
    }
}
