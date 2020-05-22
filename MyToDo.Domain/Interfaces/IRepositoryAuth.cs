using MyToDo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Domain.Interfaces
{
    public interface IRepositoryAuth<T> where T : Entity
    {
        IList<T> GetAll(int userId);
        Task<T> GetById(int id, int userId);
        T Create(T obj, int userId);
        void Update(T obj, int id, int userId);
        Task Delete(int id, int userId);
    }
}
