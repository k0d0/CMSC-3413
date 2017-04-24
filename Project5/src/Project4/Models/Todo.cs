using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization.Infrastructure;

namespace Project5.Models
{
    public class Todo
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public DateTime DueDate { get; set; }

        public string Tags { get; set; }

        public string UserName { get; set; }

    }
}
