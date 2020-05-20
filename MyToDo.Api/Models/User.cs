using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MyToDo.Api.Models
{
    public class User
    {
        //public int Id { get; set; }
        public string FirstName { get; set; }
       // public string LastName { get; set; }
        public int Age { get; set; }
    }
}
