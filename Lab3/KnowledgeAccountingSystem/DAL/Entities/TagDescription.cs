using System.Collections.Generic;

namespace DAL.Entities
{
    public class TagDescription : BaseEntity
    {
        public string Description { get; set; }

        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}