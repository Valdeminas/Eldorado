using System;
using System.Collections.Generic;
using EldoradoApi.Domain.Models;

namespace EldoradoApi.Resources
{
        public abstract class LinkedResourceBase
        {
            public List<Link> Links { get; set; } = new List<Link>();
        }
}
