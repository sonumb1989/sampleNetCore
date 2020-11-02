using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    [Table("ProductColors")]
    public class ProductColor : BaseEntity
    {
        public int ProductId { get; set; } // Id (Primary key)
        public int ColorId { get; set; } // Id (Primary key)
    }
}
