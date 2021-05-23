using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
    public class User : IdentityUser<int>
    {
        public string ImageLink { get; set; } = "https://i.pinimg.com/originals/51/f6/fb/51f6fb256629fc755b8870c801092942.png";
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string School { get; set; }
        public string University { get; set; }
        public string Speciality { get; set; }
        public string Job { get; set; }

        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

        public virtual ICollection<TagDescription> TagDescriptions { get; set; } = new List<TagDescription>();

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}