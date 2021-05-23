using System.Collections.Generic;

namespace DAL.Entities
{
    public class Comment : BaseEntity
    {
        public int Rating { get; set; }
        public string Text { get; set; }

        public int UserReceiverId { get; set; }
        public virtual User UserReceiver { get; set; }

        public int UserSenderId { get; set; }
        public virtual User UserSender { get; set; }

        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}