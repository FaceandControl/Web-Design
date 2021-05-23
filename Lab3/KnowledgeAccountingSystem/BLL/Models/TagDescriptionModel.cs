using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class TagDescriptionModel
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public TagModel Tag { get; set; }

        public int UserId { get; set; }
    }
}
