using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models.Out
{
    public class UserTokenModel
    {
        public int Id { get; set; }
        public string Roles { get; set; }
        public string Token { get; set; }
    }
}
