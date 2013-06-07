using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using AutoMapper;
using iTechArt.Shop.DataAccess.Repositories;
using iTechArt.Shop.Entities;
using Helpers;
using Ninject;

namespace iTechArt.Shop.Logic.Services
{
    public class CartService : Interfaces.ICartService
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
        public void Add(string userEmail, int productId, double count)
        {
            User us = GetUser(userEmail);

            Cart tmpCart = us.Carts.FirstOrDefault(m => m.Product_Id == productId);

            //Product add first time
            if (tmpCart == null)
            {
                tmpCart = new Cart {Product_Id = productId, Count = count};
                us.Carts.Add(tmpCart);
            }
            else //product exist
            {
                tmpCart.Count += count;
            }

            _repo.Update(us);

        }

        // Add new product to user cart.
        // WARING: If you add one product > 1 times, finnaly product in cart is NOT replace. Only summarize new and old count
        //
        public void AddArray(string userEmail, int[] productsId, double[] counts)
        {
            User us = GetUser(userEmail);

            for (int q = 0; q < productsId.Length; q++)
            {
                Cart tmpCart = us.Carts.FirstOrDefault(m => m.Product_Id == productsId[q]);

                //Product add first time
                if (tmpCart == null)
                {
                    tmpCart = new Cart();
                    tmpCart.Product_Id = productsId[q];
                    tmpCart.Count = counts[q];

                    us.Carts.Add(tmpCart);
                }
                else //product exist
                {
                    tmpCart.Count += counts[q];
                }
            }


            _repo.Update(us);

        }

        // Get list of product's in cart with additional information (count, total price)
        // 
        //
        public List<ProductInCart> GetAllChart(string userEmail)
        {
            User us = GetUser(userEmail);

            var outChart = new List<ProductInCart>();

            foreach (var cart in us.Carts)
            {
                ProductInCart tmpPic = Mapper.Map<Product, ProductInCart>(cart.Product);
                tmpPic.Count = cart.Count; //automapper map Products, but no all cart
                tmpPic.TotalPrice = cart.Product.Price*cart.Count; //automapper don't calculate total price

                outChart.Add(tmpPic);
            }

            return outChart;
        }

        // Delete product record from user's cart by Product ID
        public void DeleteProduct(string userEmail, int productId)
        {

            User us = GetUser(userEmail);

            Cart findCart = us.Carts.FirstOrDefault(m => m.Product_Id == productId);

            if (findCart == null)
                throw new InstanceNotFoundException("User not have this product");

            us.Carts.Remove(findCart);

            _repo.Update(us);

        }

        // Clear user's cart completely
        public void Clear(string userEmail)
        {
            User us = GetUser(userEmail);

            us.Carts.Clear();

            _repo.Update(us);

        }


        //Update count of any product
        //New count replace old count of ProductId
        //
        public double UpateCount(string userEmail, int productId, double newCount)
        {
            User us = GetUser(userEmail);

            Cart currentCart = us.Carts.FirstOrDefault(m => m.Product_Id == productId);

            if (currentCart == null)
                throw new InstanceNotFoundException("This product is not exist in this user account");

            currentCart.Count = newCount;

            _repo.Update(us);

            return currentCart.Product.Price*currentCart.Count;
        }

        public double GetTotalPrice(string userEmail)
        {
            User us = GetUser(userEmail);

            return us.Carts.Sum(cartItem => cartItem.Product.Price*cartItem.Count);
        }

        private User GetUser(string userEmail)
        {
            if (String.IsNullOrWhiteSpace(userEmail))
                throw new ArgumentException("Bad arguments. (Bad data, or null reference)");

            User us = _repo.ReadAll()
                .FirstOrDefault(m => (m.email == userEmail));

            if (us == null)
                throw new InstanceNotFoundException("User not found");

            return us;
        }
    }
}
