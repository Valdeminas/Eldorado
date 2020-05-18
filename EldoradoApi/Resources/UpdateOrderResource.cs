using System;
using System.ComponentModel.DataAnnotations;

namespace EldoradoApi.Resources
{
    public class UpdateOrderResource
    {

        [Required]
        public bool Paid { get; set; }

        [Required]
        public bool Completed { get; set; }
    }
}
