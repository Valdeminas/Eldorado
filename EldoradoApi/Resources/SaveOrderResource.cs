using System;
using System.ComponentModel.DataAnnotations;

namespace EldoradoApi.Resources
{
    public class SaveOrderResource
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        public long SellerId { get; set; }
    }
}
