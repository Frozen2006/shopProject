using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Instrumentation;
using AutoMapper;
using iTechArt.Shop.DataAccess.Repositories;
using iTechArt.Shop.Entities;
using iTechArt.Shop.Common.Services;
using Ninject;
using Helpers;

namespace iTechArt.Shop.Logic.Services
{
    public class OrderService : IOrderService
    {
        private readonly UserRepository _userRepository;
        private readonly TimeSlotsRepository _timeSlots;
        private readonly OrdersRepository _ordersRepository;

        private IUserService UserService { get; set; }

        [Inject]
        public OrderService(UserRepository userRepository, TimeSlotsRepository tsr, OrdersRepository or, IUserService userService)
        {
            _userRepository = userRepository;
            _timeSlots = tsr;
            _ordersRepository = or;

            UserService = userService;
        }

        // Create order from user cart
        // return order ID
        //
        public int CreateOrder(string userEmail, int timeSlotId, string comments)
        {
            User user = _userRepository.ReadAll().FirstOrDefault(m => m.email == userEmail);

            if (user == null)
                throw new InstanceNotFoundException("User not found");

            DeliverySpot timeSlot = _timeSlots.ReadAll().FirstOrDefault(m => m.Id == timeSlotId);

            if (timeSlot == null)
                throw new InstanceNotFoundException("Time slot not found");

            int discountValue = timeSlot.Type;

            ICollection<Buye> buyes = new Collection<Buye>();

            var ord = new Order();
            double totalCost = 0.0;
            //copy all products from cart to order
            foreach (var cart in user.Carts)
            {
                var tmpBuye = new Buye
                    {
                        Count = cart.Count,
                        Product = cart.Product,
                        Order = ord
                    };
                buyes.Add(tmpBuye);

                totalCost += cart.Product.Price*cart.Count;
            }

            ord.Buyes = buyes;
            ord.Comments = comments;
            ord.DeliverySpot = timeSlot;
            ord.TotalPrice = totalCost;
            ord.Discount = discountValue;
            ord.User = user;
            ord.Status = (int)OrderStatus.WaitForPaid;
            ord.CreationTime = DateTime.Now;
            user.Orders.Add(ord);

            //clear user chart
            user.Carts.Clear();


            _userRepository.Update(user);
            var firstOrDefault = _userRepository.ReadAll().FirstOrDefault(m => m.email == userEmail);
            if (firstOrDefault != null)
            {
                int orderId = firstOrDefault.Orders.Last(m => (Math.Abs(m.TotalPrice - totalCost) < 0.001) && (m.DeliverySpotId == timeSlot.Id)) //m.TotalPrice AND totalCost is float. We muct compare this by epsilon value (this is 0.001)
                                             .Id;

                return orderId;
            }
            return -1;
        }

        public List<OrdersInList> GetUserOrders(string userEmail)
        {
            User user = _userRepository.ReadAll().FirstOrDefault(m => m.email == userEmail);

            if (user == null)
            {
                return null;
            }

            var ordList = new List<OrdersInList>();

            foreach (var order in user.Orders)
            {
                DateTime dt;
                if (order.CreationTime != null)
                    dt = (DateTime)order.CreationTime;
                else
                     dt = DateTime.Now;

                ordList.Add(new OrdersInList
                    {
                        Id = order.Id,
                        startOrderTime = order.DeliverySpot.StartTime,
                        endOrderTime = order.DeliverySpot.EndTime,
                        OrderStatus = (OrderStatus)order.Status,
                        price = order.TotalPrice * Convert.ToDouble(100 - order.Discount) / 100.0,
                        CreationTime = dt
                    });
            }

            return ordList;
        }

        /// <summary>
        /// Returns null if user is not logged in or if the order doesn't belong to him.
        /// </summary>
        /// <param name="orderId">Id of the order for payment</param>
        /// <returns>Order detail</returns>
        public OrdersDetails GetOrderForPayment(int orderId)
        {
            OrdersDetails order = GetOrderDetails(orderId);

            //Order not found
            if (order == null)
                return null;

            string email = UserService.GetEmailIfLoginIn();
            //Access forbidden
            if (order.userEmail != email)
                return null;

            return order;
        }

        public OrdersDetails GetOrderDetails(int id)
        {
            Order order = _ordersRepository.ReadAll().FirstOrDefault(m => m.Id == id);

            if (order == null)
            {
                return null;
            }

            //generate list of products 
            var prod = new List<ProductInCart>();

            foreach (var buye in order.Buyes)
            {
                ProductInCart tmpPic = Mapper.Map<Product, ProductInCart>(buye.Product);
                tmpPic.Count = buye.Count; //automapper map Products, but no all cart
                tmpPic.TotalPrice = buye.Product.Price * buye.Count; //automapper don't calculate total price

                prod.Add(tmpPic);
            }

            var od = Mapper.Map<Order, OrdersDetails>(order);
            //map field's, wich not mapped
            od.startDeliveryTime = order.DeliverySpot.StartTime;
            od.endErliveryTime = order.DeliverySpot.EndTime;
            od.OrderStatus = (OrderStatus) (order.Status);
            od.PriceWithDiscount = order.TotalPrice*Convert.ToDouble(100 - order.Discount)/100.0;
            od.Products = prod;

            return od;
        }

        public void UpdateOrder(int id, OrderStatus orderStatus)
        {
            Order order = _ordersRepository.ReadAll().FirstOrDefault(m => m.Id == id);

            if (order == null)
                throw new InstanceNotFoundException("Order not found");

            order.Status = (int)orderStatus;

            _ordersRepository.Update(order);
        }

    }
}
