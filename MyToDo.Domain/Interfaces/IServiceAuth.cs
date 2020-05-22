using FluentValidation;
using MyToDo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Domain.Interfaces
{
    public interface IServiceAuth<T> where T : Entity
    {
        IList<T> GetAll(int userId);
        Task<T> GetById(int id, int userId);
        T Create<V>(T obj, int userId) where V : AbstractValidator<T>;
        void Update<V>(T obj, int id, int userId) where V : AbstractValidator<T>;
        Task Delete(int id, int userId);
    }
}
