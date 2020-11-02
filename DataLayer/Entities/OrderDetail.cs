namespace DataLayer.Entities
{
    public class OrderDetail : BaseEntity
    {
        public int OrderId { get; set; } // OrderId
        public int? ProductId { get; set; } // ProductId
        public string Size { get; set; } // Size
        public string Color { get; set; } // Color
        public int Quantity { get; set; } // Quantity
        public decimal UnitPrice { get; set; } // UnitPrice
        public string Status { get; set; } // Status

    }
}
