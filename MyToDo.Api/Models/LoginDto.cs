using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyToDo.Api.Models
{
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
