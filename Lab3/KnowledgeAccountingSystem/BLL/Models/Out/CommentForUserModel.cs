using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models.Out
{
    public class CommentForUserModel
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
        public int UserSenderId { get; set; }
        public int UserReceiverId { get; set; }
        public string ImageLinkUserSender { get; set; }
        public string FullNameUserSender { get; set; }

        public ICollection<TagModel> Tags { get; set; } = new List<TagModel>();
    }
}
