using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Status : BaseEntity
    {
        public string UserStatus { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
