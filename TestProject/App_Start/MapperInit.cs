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
            Mapper.CreateMap<User, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Address2, opt => opt.MapFrom(src => src.Address2))
                .ForMember(dest => dest.Carts, opt => opt.MapFrom(src => src.Carts))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.DeliverySpots, opt => opt.MapFrom(src => src.DeliverySpots))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Phone2, opt => opt.MapFrom(src => src.Phone2))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.Sessions, opt => opt.MapFrom(src => src.Sessions))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Zip, opt => opt.MapFrom(src => src.Zip))
                                .ForAllMembers(opt => opt.Ignore());

            Mapper.CreateMap<Cart, Cart>()
                .ForMember(dest=> dest.Count, opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
                .ForAllMembers(opt => opt.Ignore());


            Mapper.CreateMap<Order, Order>()
                .ForMember(dest => dest.Buyes, opt => opt.MapFrom(src => src.Buyes))
                .ForMember(dest => dest.DeliverySpot, opt => opt.MapFrom(src => src.DeliverySpot))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForAllMembers(opt=>opt.Ignore());



            Mapper.CreateMap<Product, Product>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Carts, opt => opt.MapFrom(src => src.Carts))
                .ForMember(dest => dest.SellByWeight, opt => opt.MapFrom(src => src.SellByWeight))
                .ForMember(dest => dest.AverageWeight, opt => opt.MapFrom(src => src.AverageWeight))
                .ForMember(dest => dest.UnitOfMeasure, opt => opt.MapFrom(src => src.UnitOfMeasure))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                                .ForAllMembers(opt => opt.Ignore());


            Mapper.CreateMap<Session, Session>()
                .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForAllMembers(opt => opt.Ignore());

            Mapper.CreateMap<DeliverySpot, DeliverySpot>()
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users))
                .ForAllMembers(opt => opt.Ignore());
            
            Mapper.CreateMap<Zip,Zip>()
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.SubCity, opt => opt.MapFrom(src => src.SubCity))
                .ForAllMembers(opt => opt.Ignore());


            Mapper.CreateMap<Category, Category>()
                .ForMember(src => src.ChildCategories, opt=>opt.MapFrom(dest =>dest.ChildCategories))
                .ForMember(src => src.Name, opt => opt.MapFrom(dest => dest.Name))
                .ForMember(src => src.Parent, opt => opt.MapFrom(dest => dest.Parent))
                .ForMember(src => src.ParentCategory, opt => opt.MapFrom(dest => dest.ParentCategory))
                .ForMember(src => src.Products, opt => opt.MapFrom(dest => dest.Products))
                .ForAllMembers(opt=>opt.Ignore());


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
                .ForMember(dest => dest.EndErliveryTime, opt => opt.MapFrom(src => src.DeliverySpot.EndTime))
                .ForMember(dest => dest.StartDeliveryTime, opt => opt.MapFrom(src => src.DeliverySpot.StartTime))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email));
            Mapper.CreateMap<ChangeDeliveryAddressModel, User>();
            Mapper.CreateMap<UserDetails, ChangeDeliveryAddressModel>();

        }
    }
}