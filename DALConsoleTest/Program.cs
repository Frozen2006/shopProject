using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Repositories.DbFirstRepository;
using Ninject;

namespace DALConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Ninject.IKernel kern = new StandardKernel(new NinjectKernel());


            ProductRepository prodRep = kern.Get<ProductRepository>();
            CategoryRepository catRep = kern.Get<CategoryRepository>();

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
