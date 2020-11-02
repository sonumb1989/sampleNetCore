using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public class Order : BaseEntity
    {
        public int BillingId { get; set; } // BillingId
        public int ShippingId { get; set; } // ShippingId
        public decimal Total { get; set; } // Total
        public DateTime OrderDate { get; set; } // OrderDate
        public string Status { get; set; } // Status
        public string Description { get; set; } // Description
        public string Currency { get; set; }
        public string UserId { get; set; }
        public bool IsCoupon { get; set; }
        public bool IsActive { get; set; }
        public int? PaymentMethod { get; set; }

    }
}
