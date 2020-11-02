using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    [Table("ProductSizes")]
    public class ProductSize : BaseEntity
    {
        public int ProductId { get; set; } // Id (Primary key)
        public int SizeId { get; set; } // Id (Primary key)
    }
}
