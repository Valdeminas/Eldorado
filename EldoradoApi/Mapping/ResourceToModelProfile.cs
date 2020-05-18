using System;
using AutoMapper;
using EldoradoApi.Models;
using EldoradoApi.Resources;

namespace EldoradoApi.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveOrderResource, Order>();
            CreateMap<UpdateOrderResource, Order>();
        }
    }
}
