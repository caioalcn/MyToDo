using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDo.Api.Models
{
    public class Todo
    {
        public string Title { get; set; }
        public bool IsDone { get; set; }
        public User User { get; set; }
    }
}

