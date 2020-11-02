using System.ComponentModel.DataAnnotations.Schema;
namespace DataLayer.Entities
{
    [Table("ProductCategories")]
    public class ProductCategory : BaseEntity
    {
        public int ProductId { get; set; } // Id (Primary key)
        public int CategoryId { get; set; } // Id (Primary key)
    }
}
