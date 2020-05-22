using MyToDo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Domain.Interfaces
{
    public interface IRepositoryLogin
    {
        Task<User> FindUser(Login user);
    }
}
