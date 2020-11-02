using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class BaseEntity
    {
        [Key]
        [Required]
        [Description("Key")]
        public int Id { get; set; }

        [Description("Boolean(true/false)")]
        public bool IsDeleted { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedUser { get; set; }
        public string UpdatedUser { get; set; }
    }
}
