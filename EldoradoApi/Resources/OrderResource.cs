using System;
namespace EldoradoApi.Resources
{
    public class OrderResource
    {
        public int Id { get; set; }
        public bool Paid { get; set; }
        public bool Completed { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
