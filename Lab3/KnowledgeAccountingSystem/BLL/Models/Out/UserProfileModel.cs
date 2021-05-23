using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models.Out
{
    public class UserProfileModel
    {
        public int Id { get; set; }
        public string ImageLink { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string School { get; set; }
        public string University { get; set; }
        public string Speciality { get; set; }
        public string Job { get; set; }
        public string Status { get; set; }
        public double Rating { get; set; }
    }
}
