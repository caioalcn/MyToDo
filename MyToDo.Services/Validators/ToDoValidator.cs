using FluentValidation;
using MyToDo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyToDo.Services.Validators
{
   public class ToDoValidator : AbstractValidator<Todo>
    {
        public ToDoValidator() 
        { 
        }
    }
}
