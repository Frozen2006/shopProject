using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using DAL.membership;
using Entities;
using Interfaces;
using Ninject;
using Helpers;

namespace BLL
{
    public class OrderService : IOrderService
    {
        private UserRepository _userRepository;
        private TimeSlotsRepository _timeSlots;
        private OrdersRepository _ordersRepository;

        [Inject]
        public OrderService(UserRepository userRepository, TimeSlotsRepository tsr, OrdersRepository or)
        {
            this._userRepository = userRepository;
            this._timeSlots = tsr;
            _ordersRepository = or;
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

            int DiscountValue = timeSlot.Type;

            ICollection<Buye> buyes = new Collection<Buye>();

            Order ord = new Order();
            double totalCost = 0.0;
            //copy all products from cart to order
            foreach (var cart in user.Carts)
            {
                Buye tmpBuye = new Buye()
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
            ord.Discount = DiscountValue;
            ord.User = user;
            ord.Status = (int)OrderStatus.WaitForPaid;
            ord.CreationTime = DateTime.Now;
            user.Orders.Add(ord);

            //clear user chart
            user.Carts.Clear();


            _userRepository.Update(user);
            int orderId = _userRepository.ReadAll()
                           .FirstOrDefault(m => m.email == userEmail)
                           .Orders.Last(m => (m.TotalPrice == totalCost) && (m.DeliverySpotId == timeSlot.Id))
                           .Id;

            return orderId;
        }

        public List<OrdersInList> GetUserOrders(string userEmail)
        {
            User user = _userRepository.ReadAll().FirstOrDefault(m => m.email == userEmail);

            if (user == null)
                throw new InstanceNotFoundException("User not found");

            List<OrdersInList> ordList = new List<OrdersInList>();

            foreach (var order in user.Orders)
            {
                DateTime dt;
                if (order.CreationTime != null)
                    dt = (DateTime)order.CreationTime;
                else
                     dt = DateTime.Now;

                ordList.Add(new OrdersInList()
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

        public OrdersDetails GetOrderDetails(int Id)
        {
            Order order = _ordersRepository.ReadAll().FirstOrDefault(m => m.Id == Id);

            if (order == null)
                throw new InstanceNotFoundException("Order not found");

            //generate list of products 
            List<ProductInCart> prod = new List<ProductInCart>();

            foreach (var buye in order.Buyes)
            {
                prod.Add(new ProductInCart()
                    {
                        Count = buye.Count,
                        AverageWeight = buye.Product.AverageWeight,
                        CategoryId = buye.Product.CategoryId,
                        CategoryName = buye.Product.Category.Name,
                        Id = buye.Product.Id,
                        Name = buye.Product.Name,
                        UnitOfMeasure = buye.Product.UnitOfMeasure,
                        SellByWeight = buye.Product.SellByWeight,
                        PriceOfOneItem = buye.Product.Price,
                        TotalPrice = buye.Product.Price*buye.Count
                    });
            }

            DateTime dt;
            if (order.CreationTime != null)
                dt = (DateTime)order.CreationTime;
            else
                dt = DateTime.Now;

            OrdersDetails od = new OrdersDetails()
                {
                    Comments = order.Comments,
                    userEmail = order.User.email,
                    Id = Id,
                    TotalPrice = order.TotalPrice,
                    PriceWithDiscount = order.TotalPrice*Convert.ToDouble(100 - order.Discount)/100.0,
                    Products = prod,
                    startDeliveryTime = order.DeliverySpot.StartTime,
                    endErliveryTime = order.DeliverySpot.EndTime,
                    OrderStatus = (OrderStatus)(order.Status),
                    CreationTime = dt
                };

            return od;
        }

        public void UpdateOrder(int Id, OrderStatus orderStatus)
        {
            Order order = _ordersRepository.ReadAll().FirstOrDefault(m => m.Id == Id);

            if (order == null)
                throw new InstanceNotFoundException("Order not found");

            order.Status =(int)orderStatus;

            _ordersRepository.Update(order);
        }

    }
}
