using FluentValidation;
using MyToDo.Domain.Entities;
using MyToDo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Services.Services
{
    public class UserService : IService<User>
    {
        private readonly IRepository<User> _repositoy;

        public UserService(IRepository<User> repository)
        {
            _repositoy = repository;
        }

        public User Create<V>(User obj) where V : AbstractValidator<User>
        {
            Validate(obj, Activator.CreateInstance<V>());

            return _repositoy.Create(obj);
        }

        public async Task Delete(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("The ID can't be zero.");
            }

            await _repositoy.Delete(id);
        }

        public IList<User> GetAll()
        {
            return _repositoy.GetAll();
        }

        public async Task<User> GetById(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("The ID can't be zero.");
            }

            return await _repositoy.GetById(id);
        }

        public void Update<V>(User obj) where V : AbstractValidator<User>
        {
            Validate(obj, Activator.CreateInstance<V>());

            _repositoy.Update(obj);
        }

        private void Validate(User obj, AbstractValidator<User> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");

            validator.ValidateAndThrow(obj);
        }
    }
}
