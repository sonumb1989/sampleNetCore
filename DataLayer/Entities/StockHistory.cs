using System;

namespace DataLayer.Entities
{
    public class StockHistory : BaseEntity
    {
        public int StockId { get; set; }
        public int Quantity { get; set; }
        public virtual Stock Stock { get; set; }
    }
}
