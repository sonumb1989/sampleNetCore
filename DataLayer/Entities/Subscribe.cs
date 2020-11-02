using System;

namespace DataLayer.Entities
{
    public class Subscribe : BaseEntity
    {
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
