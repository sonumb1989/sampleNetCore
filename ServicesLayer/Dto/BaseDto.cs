using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesLayer.Dto
{
    public class BaseDto
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedUser { get; set; }
        public string UpdatedUser { get; set; }
    }
}
