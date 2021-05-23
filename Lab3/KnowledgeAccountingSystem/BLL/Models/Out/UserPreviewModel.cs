using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models.Out
{
    public class UserPreviewModel
    {
        public int Id { get; set; }
        public string ImageLink { get; set; }
        public string FullName { get; set; }
        public string Status { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public double Rating { get; set; }
    }
}