using System;
namespace EldoradoApi.Domain.Models
{
    public class OrderParameters : QueryStringParameters
    {
        public long? UserId { get; set; }
    }
}
