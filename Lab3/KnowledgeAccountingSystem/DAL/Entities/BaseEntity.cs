using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime LastModified { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
