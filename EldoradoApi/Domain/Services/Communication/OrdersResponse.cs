using System;
using System.Collections.Generic;
using EldoradoApi.Models;

namespace EldoradoApi.Domain.Services.Communication
{
    public class OrdersResponse:BaseResponse
    {
        public IEnumerable<Order> Orders { get; private set; }

        private OrdersResponse(bool success, string message, IEnumerable<Order> orders) : base(success, message)
        {
            Orders = orders;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="order">Saved order.</param>
        /// <returns>Response.</returns>
        public OrdersResponse(IEnumerable<Order> orders) : this(true, string.Empty, orders)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public OrdersResponse(string message) : this(false, message, null)
        { }
    }
}
