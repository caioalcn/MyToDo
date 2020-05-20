using FluentValidation;
using MyToDo.Domain.Entities;
using MyToDo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Services.Services
{
    public class ToDoService : IService<Todo>
    {
        private readonly IRepository<Todo> _repository;

        public ToDoService(IRepository<Todo> repository)
        {
            _repository = repository;
        }
        public Todo Create<V>(Todo obj) where V : AbstractValidator<Todo>
        {

            Validate(obj, Activator.CreateInstance<V>());

            return _repository.Create(obj);
        }

        public async Task Delete(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("The ID can't be zero.");
            }

            await _repository.Delete(id);
        }

        public IList<Todo> GetAll()
        {
            return _repository.GetAll();
        }

        public async Task<Todo> GetById(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("The ID can't be zero.");
            }

            return await _repository.GetById(id);
        }

        public void Update<V>(Todo obj) where V : AbstractValidator<Todo>
        {
            Validate(obj, Activator.CreateInstance<V>());

            _repository.Update(obj);
        }
        private void Validate(Todo obj, AbstractValidator<Todo> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");

            validator.ValidateAndThrow(obj);
        }
    }
}
