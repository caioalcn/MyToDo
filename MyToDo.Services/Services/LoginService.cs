using MyToDo.Domain.Entities;
using MyToDo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Services.Services
{
    public class LoginService : IServiceLogin
    {
        private readonly IRepositoryLogin _repositoryLogin;

        public LoginService(IRepositoryLogin repositoryLogin)
        {
            _repositoryLogin = repositoryLogin;
        }
        public async Task<User> FindUser(Login user)
        {
            return await _repositoryLogin.FindUser(user);
        }
    }
}
