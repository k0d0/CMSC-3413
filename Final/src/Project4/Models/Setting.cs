using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization.Infrastructure;

namespace Project5.Models
{
    public class Setting
    {
        public int Id { get; set; }

        public int warningWindow { get; set; }

        public string UserName { get; set;  }
    }
}
