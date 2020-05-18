using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EldoradoApi.Domain.Services.Communication;
using EldoradoApi.Models;
using EldoradoApi.Resources;

namespace EldoradoApi.Services
{
    public interface IOrderLinkingService
    {
        OrderResource LinkCreated(OrderResource orderResource);
    }
}
