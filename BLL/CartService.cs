using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL.membership;
using Entities;
using Helpers;
using Ninject;

namespace BLL
{
    public class CartService : Interfaces.ICart
    {

        private readonly UserRepository _repo;

        [Inject]
        public CartService(UserRepository inputRepo)
        {
            _repo = inputRepo;
        }

        // Add new product to user cart.
        // WARING: If you add one product > 1 times, finnaly product in cart is NOT replace. Only summarize new and old count
        //
        public void Add(string UserEmail, int ProductId, double Count)
        {
            User us = GetUser(UserEmail);

            Cart _tmpCart = us.Carts.FirstOrDefault(m => m.Product_Id == ProductId);

            //Product add first time
            if (_tmpCart == null)
            {
                _tmpCart = new Cart();
                _tmpCart.Product_Id = ProductId;
                _tmpCart.Count = Count;

                us.Carts.Add(_tmpCart);
            }
            else //product exist
            {
                _tmpCart.Count += Count;
            }

            _repo.Update(us);

        }

        // Add new product to user cart.
        // WARING: If you add one product > 1 times, finnaly product in cart is NOT replace. Only summarize new and old count
        //
        public void AddArray(string UserEmail, int[] ProductsId, double[] Counts)
        {
            User us = GetUser(UserEmail);

            for (int q = 0; q < ProductsId.Length; q++)
            {
                Cart _tmpCart = us.Carts.FirstOrDefault(m => m.Product_Id == ProductsId[q]);

                //Product add first time
                if (_tmpCart == null)
                {
                    _tmpCart = new Cart();
                    _tmpCart.Product_Id = ProductsId[q];
                    _tmpCart.Count = Counts[q];

                    us.Carts.Add(_tmpCart);
                }
                else //product exist
                {
                    _tmpCart.Count += Counts[q];
                }
            }


            _repo.Update(us);

        }

        // Get list of product's in cart with additional information (count, total price)
        // 
        //
        public List<ProductInCart> GetAllChart(string UserEmail)
        {
            User us = GetUser(UserEmail);

            List<ProductInCart> outChart = new List<ProductInCart>();

            foreach (var cart in us.Carts)
            {
                ProductInCart tmpPic = Mapper.Map<Product, ProductInCart>(cart.Product);
                tmpPic.TotalPrice = cart.Product.Price*cart.Count; //automapper don't calculate total price ;)

                outChart.Add(tmpPic);
            }

            return outChart;
        }

        // Delete product record from user's cart by Product ID
        public void DeleteProduct(string UserEmail, int ProductId)
        {

            User us = GetUser(UserEmail);

            Cart findCart = us.Carts.FirstOrDefault(m => m.Product_Id == ProductId);

            if (findCart == null)
                throw new InstanceNotFoundException("User not have this product");

            us.Carts.Remove(findCart);

            _repo.Update(us);

        }

        // Clear user's cart completely
        public void Clear(string UserEmail)
        {
            User us = GetUser(UserEmail);

            us.Carts.Clear();

            _repo.Update(us);

        }


        //Update count of any product
        //New count replace old count of ProductId
        //
        public double UpateCount(string UserEmail, int ProductId, double NewCount)
        {
            User us = GetUser(UserEmail);

            Cart currentCart = us.Carts.FirstOrDefault(m => m.Product_Id == ProductId);

            if (currentCart == null)
                throw new InstanceNotFoundException("This product is not exist in this user account");

            currentCart.Count = NewCount;

            _repo.Update(us);

            return currentCart.Product.Price*currentCart.Count;
        }

        public double GetTotalPrice(string UserEmail)
        {
            User us = GetUser(UserEmail);

            double totalPrice = 0.0;

            foreach (var cartItem in us.Carts)
            {
                totalPrice += cartItem.Product.Price*cartItem.Count;
            }

            return totalPrice;
        }

        private User GetUser(string userEmail)
        {
            if (String.IsNullOrWhiteSpace(userEmail))
                throw new ArgumentException("Bad arguments. (Bad data, or null reference)");

            User us = _repo.ReadAll().FirstOrDefault(m => String.Compare(m.email, userEmail) == 0);

            if (us == null)
                throw new InstanceNotFoundException("User not found");

            return us;
        }
    }
}
