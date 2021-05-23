using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models.In
{
    public class AddCommentModel
    {
        public int Rating { get; set; }
        public string Text { get; set; }
        public int UserReceiverId { get; set; }

        public ICollection<TagModel> Tags { get; set; } = new List<TagModel>();
    }
}
