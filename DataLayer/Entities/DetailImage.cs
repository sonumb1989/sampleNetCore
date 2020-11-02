using System;

namespace DataLayer.Entities
{
    public class DetailImage : BaseEntity
    {
        public string Image { get; set; } // Image (length: 100)
        public int ProductId { get; set; } // ProductId
        public int Order { get; set; } // Order
        public string ProductCode { get; set; } // ProductCode (length: 10)
    }
}
