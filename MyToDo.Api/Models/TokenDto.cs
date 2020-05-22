using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDo.Api.Models
{
    public class TokenDto
    {
        public string Value { get; set; }
        public DateTime DateCreated { get; }
        public DateTime DateExpiration { get; private set; }

        public TokenDto()
        {
            DateCreated = DateTime.Now;
        }

        public void SetExpiration(TimeSpan time)
        {
            DateExpiration = DateCreated + time;
        }
    }
}
