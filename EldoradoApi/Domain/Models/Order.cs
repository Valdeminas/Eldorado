using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EldoradoApi.Models
{
    public class Order
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long SellerId { get; set; }

        public bool Paid { get; set; }
        public bool Completed { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set; }

    }
}
