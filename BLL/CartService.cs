using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
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
        public void Add(string UserEmail, int ProductId, int Count)
        {
            if (String.IsNullOrWhiteSpace(UserEmail) || (ProductId == 0) || (Count <=0 ) )
                throw new ArgumentException("Bad arguments. (Bad data, or null reference)");
            
            User us = _repo.ReadAll().FirstOrDefault(m => String.Compare(m.email, UserEmail) == 0);

            if (us == null)
                throw new InstanceNotFoundException("User not found");

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

        // Get list of product's in cart with additional information (count, total price)
        // 
        //
        public List<ProductInCart> GetAllChart(string UserEmail)
        {
            if (String.IsNullOrWhiteSpace(UserEmail))
                throw new ArgumentException("Bad arguments. (Bad data, or null reference)");

            User us = _repo.ReadAll().FirstOrDefault(m => String.Compare(m.email, UserEmail) == 0);

            if (us == null)
                throw new InstanceNotFoundException("User not found");

            List<ProductInCart> outChart = new List<ProductInCart>();

            foreach (var cart in us.Carts)
            {
                ProductInCart tmpPic = new ProductInCart()
                    {
                        Id = cart.Product.Id,
                        Name = cart.Product.Name,
                        CategoryName = cart.Product.Category.Name,
                        CategoryId = cart.Product.CategoryId,
                        PriceOfOneItem = cart.Product.Price,
                        AverageWeight = cart.Product.AverageWeight,
                        SellByWeight = cart.Product.SellByWeight,
                        UnitOfMeasure = cart.Product.UnitOfMeasure,
                        Count = cart.Count,
                        TotalPrice = cart.Product.Price * cart.Count
                    };

                outChart.Add(tmpPic);
            }

            return outChart;
        }

        // Delete product record from user's cart by Product ID
        public void DeleteProduct(string UserEmail, int ProductId)
        {
            if (String.IsNullOrWhiteSpace(UserEmail) || (ProductId == 0))
                throw new ArgumentException("Bad arguments. (Bad data, or null reference)");

            User us = _repo.ReadAll().FirstOrDefault(m => String.Compare(m.email, UserEmail) == 0);

            if (us == null)
                throw new InstanceNotFoundException("User not found");

            Cart findCart = us.Carts.FirstOrDefault(m => m.Product_Id == ProductId);

            if (findCart == null)
                throw new InstanceNotFoundException("User not have this product");

            us.Carts.Remove(findCart);

            _repo.Update(us);

        }

        // Clear user's cart completely
        public void Clear(string UserEmail)
        {
            if (String.IsNullOrWhiteSpace(UserEmail))
                throw new ArgumentException("Bad arguments. (Bad data, or null reference)");

            User us = _repo.ReadAll().FirstOrDefault(m => String.Compare(m.email, UserEmail) == 0);

            if (us == null)
                throw new InstanceNotFoundException("User not found");

            us.Carts.Clear();

            _repo.Update(us);

        }


        //Update count of any product
        //New count replace old count of ProductId
        //
        public double UpateCount(string UserEmail, int ProductId, int NewCount)
        {
            if (String.IsNullOrWhiteSpace(UserEmail) || (NewCount <= 0))
                throw new ArgumentException("Bad arguments. (Bad data, or null reference)");

            User us = _repo.ReadAll().FirstOrDefault(m => String.Compare(m.email, UserEmail) == 0);

            if (us == null)
                throw new InstanceNotFoundException("User not found");

            Cart currentCart = us.Carts.FirstOrDefault(m => m.Product_Id == ProductId);

            if (currentCart == null)
                throw new InstanceNotFoundException("This product is not exist in this user account");

            currentCart.Count = NewCount;

            _repo.Update(us);

            return currentCart.Product.Price*currentCart.Count;
        }

        public double GetTotalPrice(string UserEmail)
        {
            if (String.IsNullOrWhiteSpace(UserEmail))
                throw new ArgumentException("Bad arguments. (Bad data, or null reference)");

            User us = _repo.ReadAll().FirstOrDefault(m => String.Compare(m.email, UserEmail) == 0);

            if (us == null)
                throw new InstanceNotFoundException("User not found");

            double totalPrice = 0.0;

            foreach (var cartItem in us.Carts)
            {
                totalPrice += cartItem.Product.Price*cartItem.Count;
            }

            return totalPrice;
        }
    }
}
