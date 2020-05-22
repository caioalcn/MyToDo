using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MyToDo.Domain.Entities
{
    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
