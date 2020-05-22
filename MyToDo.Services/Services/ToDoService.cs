using FluentValidation;
using MyToDo.Domain.Entities;
using MyToDo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Services.Services
{
    public class ToDoService : IServiceAuth<Todo>
    {
        private readonly IRepositoryAuth<Todo> _repository;

        public ToDoService(IRepositoryAuth<Todo> repository)
        {
            _repository = repository;
        }

        public IList<Todo> GetAll(int userId)
        {
            return _repository.GetAll(userId);
        }

        public async Task<Todo> GetById(int id, int userId)
        {
            if (id == 0)
            {
                throw new ArgumentException("The ID can't be zero.");
            }

            return await _repository.GetById(id, userId);
        }

        public Todo Create<V>(Todo obj, int userId) where V : AbstractValidator<Todo>
        {

            Validate(obj, Activator.CreateInstance<V>());

            return _repository.Create(obj, userId);
        }

        public async Task Delete(int id, int userId)
        {
            if (id == 0)
            {
                throw new ArgumentException("The ID can't be zero.");
            }

            await _repository.Delete(id, userId);
        }

        public void Update<V>(Todo obj, int id, int userId) where V : AbstractValidator<Todo>
        {
            Validate(obj, Activator.CreateInstance<V>());

            _repository.Update(obj, id, userId);
        }
        private void Validate(Todo obj, AbstractValidator<Todo> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");

            validator.ValidateAndThrow(obj);
        }
    }
}
