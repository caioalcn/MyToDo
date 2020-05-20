using FluentValidation;
using MyToDo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyToDo.Services.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(c => c)
                .NotNull()
                .OnAnyFailure(x =>
                {
                    throw new ArgumentNullException("Object Not Found.");
                });

            RuleFor(c => c.FirstName)
                .NotNull().WithMessage("FirstName Required")
                .NotEmpty().WithMessage("FirstName Required")
                .Length(3, 100).WithMessage("FirstName Required");

            RuleFor(c => c.LastName)
                .NotNull().WithMessage("LastName Required")
                .NotEmpty().WithMessage("LastName Required")
                .Length(3, 100).WithMessage("LastName Required");

            RuleFor(c => c.Age)
                .NotNull().WithMessage("Age Required")
                .NotEmpty().WithMessage("Age Required");
        }
    }
}
