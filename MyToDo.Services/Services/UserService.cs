using FluentValidation;
using MyToDo.Domain.Crypto;
using MyToDo.Domain.Entities;
using MyToDo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyToDo.Services.Services
{
    public class UserService : IService<User>
    {
        private readonly IRepository<User> _repository;

        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public User Create<V>(User obj) where V : AbstractValidator<User>
        {
            Validate(obj, Activator.CreateInstance<V>());
            obj.Password = Security.Encrypt(obj.Password);

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

        public IList<User> GetAll()
        {
            return _repository.GetAll();
        }

        public async Task<User> GetById(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("The ID can't be zero.");
            }

            return await _repository.GetById(id);
        }

        public void Update<V>(User obj) where V : AbstractValidator<User>
        {
            Validate(obj, Activator.CreateInstance<V>());

            _repository.Update(obj);
        }

        private void Validate(User obj, AbstractValidator<User> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");

            validator.ValidateAndThrow(obj);
        }
    }
}
