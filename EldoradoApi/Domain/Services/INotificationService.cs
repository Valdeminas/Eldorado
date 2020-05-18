using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EldoradoApi.Domain.Models;
using EldoradoApi.Domain.Services.Communication;
using EldoradoApi.Models;

namespace EldoradoApi.Domain.Services
{
    public interface INotificationService
    {
        void SendNotification(BaseResponse UpdatedObject);
    }
}
