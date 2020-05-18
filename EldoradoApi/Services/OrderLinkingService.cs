using System;
using System.Collections.Generic;
using EldoradoApi.Domain.Models;
using EldoradoApi.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EldoradoApi.Services
{
	public class OrderLinkingService : IOrderLinkingService
	{

		private readonly IUrlHelper _urlHelper;

		public OrderLinkingService(IUrlHelper urlHelper)
		{
			_urlHelper = urlHelper;
		}

		public OrderResource LinkCreated(OrderResource orderResource)
		{
			//orderResource.Links.Add(new Link(
			//	_urlHelper.Link("GetAllUserOrders", new { userId=_urlHelper.ActionContext.RouteData.Values["userId"] }),
			//	"parent",
			//	"GET"));


			return orderResource;
		}

		
	}
}
