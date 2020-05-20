using System;
using System.Collections.Generic;
using System.Text;

namespace MyToDo.Domain.Entities
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public List<Todo> todos { get; set; }

    }
}
