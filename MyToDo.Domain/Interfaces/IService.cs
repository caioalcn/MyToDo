﻿using FluentValidation;
using MyToDo.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyToDo.Domain.Interfaces
{
    public interface IService<T> where T : Entity
    {
        IList<T> GetAll();
        Task<T> GetById(int id);
        T Create<V>(T obj) where V : AbstractValidator<T>;
        void Update<V>(T obj) where V : AbstractValidator<T>;
        Task Delete(int id);
    }
}
