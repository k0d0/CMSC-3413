using Project4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public Project Project { get; set; }
    }
}
