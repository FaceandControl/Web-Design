using System.Collections.Generic;

namespace DAL.Entities
{
    public class Tag : BaseEntity
    {
        public string TagName { get; set; }

        public virtual TagDescription TagDescription { get; set; }

        public int CommentId { get; set; }
        public virtual Comment Comment { get; set; }
    }
}