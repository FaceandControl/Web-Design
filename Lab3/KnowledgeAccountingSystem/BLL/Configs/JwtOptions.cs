using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Configs
{
    public class JwtOptions
    {
        public string Secret { get; set; }
        public TimeSpan LifeTime { get; set; }
    }
}
