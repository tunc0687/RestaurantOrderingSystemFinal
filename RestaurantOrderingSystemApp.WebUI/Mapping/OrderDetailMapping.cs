﻿using AutoMapper;
using RestaurantOrderingSystemApp.EntityLayer.Entities;
using RestaurantOrderingSystemApp.WebUI.Dtos.OrderDetailDtos;

namespace RestaurantOrderingSystemApp.WebUI.Mapping
{
    public class OrderDetailMapping : Profile
    {
        public OrderDetailMapping()
        {
            CreateMap<OrderDetail, ResultOrderDetailWithProductDto>()
                .ForMember(e => e.Price, o => o.MapFrom(d => d.Product.Price))
                .ForMember(e => e.ProductName, o => o.MapFrom(d => d.Product.ProductName))
                .ReverseMap();
            CreateMap<OrderDetail, CreateOrderDetailDto>().ReverseMap();
            CreateMap<OrderDetail, UpdateOrderDetailDto>().ReverseMap();
            CreateMap<OrderDetail, GetOrderDetailDto>().ReverseMap();
        }
    }
}