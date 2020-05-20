﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyToDo.Domain.Entities
{
    public class Todo : Entity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDone { get; set; }
        public User User { get; set; }
    }
}
