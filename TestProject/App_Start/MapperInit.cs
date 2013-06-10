using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using iTechArt.Shop.Entities;
using iTechArt.Shop.Entities.PresentationModels;
using iTechArt.Shop.Web.Models;

namespace iTechArt.Shop.Web.App_Start
{
    public static class MapperInit
    {
        public static void Init()
        {
            Mapper.CreateMap<Product, ProductInCart>();
            Mapper.CreateMap<RegisterModel, User>();
            Mapper.CreateMap<User, UserDetails>();
            Mapper.CreateMap<DeliverySpot, BookingSlot>();
            Mapper.CreateMap<Order, OrdersDetails>();
        }
    }
}