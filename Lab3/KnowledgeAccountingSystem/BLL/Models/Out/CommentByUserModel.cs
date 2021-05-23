using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models.Out
{
    public class CommentByUserModel
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
        public int UserSenderId { get; set; }
        public int UserReceiverId { get; set; }
        public string ImageLinkUserReceiver { get; set; }
        public string FullNameUserReceiver { get; set; }

        public ICollection<TagModel> Tags { get; set; } = new List<TagModel>();
    }
}
