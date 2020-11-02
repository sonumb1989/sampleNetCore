using System;

namespace DataLayer.Entities
{
    public class StaticPage : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsActive { get; set; }
        public bool Status { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
    }
}
