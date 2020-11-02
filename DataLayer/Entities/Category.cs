using System.Collections.Generic;

namespace DataLayer.Entities
{
    public class Category : BaseEntity
    {
        public int ParentId { get; set; } // ParentId
        public string Name { get; set; } // Name
        public bool IsActive { get; set; } // IsActive
        public string Image { get; set; } // Image (length: 100)
    }
}
