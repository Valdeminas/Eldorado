using System;
using AutoMapper;
using EldoradoApi.Models;
using EldoradoApi.Resources;

namespace EldoradoApi.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Order, OrderResource>();
            CreateMap<Order, DeleteOrderResource>();
        }
    }
}
