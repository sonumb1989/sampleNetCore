using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    [Table("Products")]
    public class Product : BaseEntity
    {
        public string Code { get; set; } // Code (length: 10)
        public string Name { get; set; } // Name (length: 100)
        public string FriendlyName { get; set; } // FriendlyName (length: 150)

        public decimal Price { get; set; } // Price

        public decimal? PriceVN { get; set; } // Price

        public decimal DiscountedPrice { get; set; } // DiscountedPrice

        public decimal? DiscountedPriceVN { get; set; } // DiscountedPrice

        public string Description { get; set; } // Description (length: 1000)

        public string Image { get; set; } // Image (length: 100)

        public string FlickImage { get; set; }

        public bool IsNew { get; set; } // IsNew

        public bool IsSale { get; set; } // IsSale

        public bool? IsBest { get; set; }

        public bool IsActive { get; set; } // IsActive

        public bool? IsCart { get; set; } // IsActive

        public bool? IsFlicker { get; set; }
        public DateTime CreatedOn { get; set; } // CreatedOn

        public DateTime UpdatedOn { get; set; } // UpdatedOn

        //public virtual ICollection<Cart> Carts { get; set; } // Carts.FK_Carts_Products
        //public virtual ICollection<DetailImage> DetailImages { get; set; } // DetailImages.FK_dbo.DetailImages_dbo.Products_ProductId
        //public virtual ICollection<OrderDetail> OrderDetails { get; set; } // OrderDetails.FK_dbo.OrderDetails_dbo.Products_ProductId
        //public virtual ICollection<Color> Colors { get; set; } // ProductColors.FK_dbo.ProductColors_dbo.Products_ProductId
        //public virtual ICollection<Size> Sizes { get; set; } // ProductSizes.FK_dbo.ProductSizes_dbo.Products_ProductId
        //public virtual ICollection<Category> Categories { get; set; }
        //public virtual ICollection<Stock> Stocks { get; set; }
    }
}
