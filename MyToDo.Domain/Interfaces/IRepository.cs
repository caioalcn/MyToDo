using MyToDo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Domain.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> GetById(int id);
        IList<T> GetAll();
        T Create(T obj);
        void Update(T obj);
        Task Delete(int id);
    }
}
