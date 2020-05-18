using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EldoradoApi.Models;
using AutoMapper;
using EldoradoApi.Resources;
using EldoradoApi.Domain.Services;
using EldoradoApi.Domain.Models;
using Newtonsoft.Json;
using EldoradoApi.Extensions;
using System;
using Microsoft.AspNetCore.Http;

namespace EldoradoApi.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/Orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public OrdersController(IOrderService orderService, IMapper mapper,INotificationService notificationService)
        {
            _orderService = orderService;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        /// <summary>
        /// Gets a list of orders. Supports pagination and userId as optional query parameter, for returning only users orders.
        /// </summary>
        /// <remarks>
        /// Sample response:
        ///
        ///     [
        ///     {
        ///         "id": 100,
        ///         "paid": false,
        ///         "completed": false,
        ///         "expiryDate": "2020-05-17T22:40:05.695258Z"
        ///     },
        ///     {
        ///         "id": 101,
        ///         "paid": false,
        ///         "completed": false,
        ///         "expiryDate": "2020-05-17T22:40:05.695402Z"
        ///     },
        ///     ] 
        ///
        /// </remarks>
        /// <response code="200">Returns (possibly empty) list of Orders</response>
        [HttpGet]
        public async Task<IEnumerable<OrderResource>> ListAsync([FromQuery] OrderParameters orderParameters)
        {
            var orders = await _orderService.ListAllAsync(orderParameters);
            var resources = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResource>>(orders);
            var metadata = new
            {
                orders.TotalCount,
                orders.PageSize,
                orders.CurrentPage,
                orders.TotalPages,
                orders.HasNext,
                orders.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return resources;
        }
        /// <summary>
        /// Creates an Order.
        /// </summary>
        /// <remarks>
        /// Sample response:
        ///
        ///     {
        ///         "id": 102,
        ///         "paid": false,
        ///         "completed": false,
        ///         "expiryDate": "2020-05-17T22:41:29.636684Z"
        ///     }   
        ///
        /// </remarks>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>     
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveOrderResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var order = _mapper.Map<SaveOrderResource, Order>(resource);
            order.CreationDate = DateTime.UtcNow;
            order.ExpiryDate = order.CreationDate.AddHours(2);
            order.Paid = false;
            order.Completed = false;

            var result = await _orderService.SaveAsync(order);

            if (!result.Success)
                return BadRequest(result.Message);

            var orderResource = _mapper.Map<Order, OrderResource>(result.Order);
            return Ok(orderResource);
        }


        /// <summary>
        /// Updates an Order.
        /// </summary>
        /// <remarks>
        /// Sample response:
        ///
        ///     {
        ///         "id": 102,
        ///         "paid": false,
        ///         "completed": false,
        ///         "expiryDate": "2020-05-17T22:41:29.636684Z"
        ///     }   
        ///
        /// </remarks>
        /// <response code="200">Returns updated response</response>
        /// <response code="400">If update failed</response> 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(long id, [FromBody] UpdateOrderResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var order = _mapper.Map<UpdateOrderResource, Order>(resource);
            var result = await _orderService.UpdateAsync(id, order);

            if (!result.Success)
                return BadRequest(result.Message);

            _notificationService.SendNotification(result);
            var orderResource = _mapper.Map<Order, OrderResource>(result.Order);
            return Ok(orderResource);
        }

    }
}
