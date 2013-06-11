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
            Mapper.CreateMap<Cart, ProductInCart>()
                  .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
                  .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Product.Price*src.Count))
                  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Product.Id));
            Mapper.CreateMap<Buye, ProductInCart>()
                  .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
                  .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Product.Price * src.Count))
                  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Product.Id));


            //DAL mapps
            Mapper.CreateMap<User, User>();
            Mapper.CreateMap<Cart, Cart>();
            Mapper.CreateMap<Order, Order>();
            Mapper.CreateMap<Product, Product>();
            Mapper.CreateMap<DeliverySpot, DeliverySpot>();
            
            Mapper.CreateMap<Category, Category>();
            Mapper.CreateMap<RegisterModel, User>();
            Mapper.CreateMap<User, UserDetails>();
            Mapper.CreateMap<DeliverySpot, BookingSlot>();
            Mapper.CreateMap<Order, OrdersDetails>()
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src=>src.Comments))
                .ForMember(dest => dest.CreationTime, opt => opt.MapFrom(src => src.CreationTime))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.PriceWithDiscount, opt => opt.MapFrom(src => src.TotalPrice * Convert.ToDouble(100 - src.Discount) / 100.0))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
                .ForMember(dest => dest.endErliveryTime, opt => opt.MapFrom(src => src.DeliverySpot.EndTime))
                .ForMember(dest => dest.startDeliveryTime, opt => opt.MapFrom(src => src.DeliverySpot.StartTime))
                .ForMember(dest => dest.userEmail, opt => opt.MapFrom(src => src.User.Email));
            Mapper.CreateMap<ChangeDeliveryAddressModel, User>();
            Mapper.CreateMap<UserDetails, ChangeDeliveryAddressModel>();

        }
    }
}