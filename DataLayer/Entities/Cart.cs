using System;

namespace DataLayer.Entities
{
    public class Cart : BaseEntity
    {
        public string CartId { get; set; } // CartId
        public int ProductId { get; set; } // ProductId
        public int Quantity { get; set; } // Quantity
        public string Size { get; set; } // Size
        public string Color { get; set; } // Color
    }
}
