using MyToDo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Domain.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        IList<T> GetAll();
        Task<T> GetById(int id);
        T Create(T obj);
        void Update(T obj);
        Task Delete(int id);
    }
}
