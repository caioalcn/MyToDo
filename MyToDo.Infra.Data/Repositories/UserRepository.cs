using Dapper;
using Microsoft.Extensions.Configuration;
using MyToDo.Domain.Entities;
using MyToDo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDo.Infra.Data.Repositories
{
    public class UserRepository : Repository, IRepository<User>, IRepositoryLogin
    {
        public UserRepository(IConfiguration configuration) : base("dbo.Users", configuration) { }

        public IList<User> GetAll()
        {
            using (var conn = new SqlConnection(Connection)) {

                string sql = $"SELECT u.id, u.loginName, u.FirstName, u.LastName, u.Age, u.email, t.id as Todos_id, t.title as Todos_title, t.isDone as Todos_isDone, t.userId as Todos_userId FROM { Table } u " +
                    $"LEFT JOIN dbo.Todos t ON u.id = t.userId";

                dynamic flat = conn.Query<dynamic>(sql);

                Slapper.AutoMapper.Configuration.AddIdentifiers(typeof(User), new List<string> { "id" });
                Slapper.AutoMapper.Configuration.AddIdentifiers(typeof(Todo), new List<string> { "id" });

                var result = (Slapper.AutoMapper.MapDynamic<User>(flat) as IEnumerable<User>).ToList();

                return result;
            }
        }

        public async Task<User> GetById(int id)
        {
            await using (var conn = new SqlConnection(Connection))
            {
                string sql = $"SELECT u.id, u.loginName, u.FirstName, u.LastName, u.Age, u.email, t.id as Todos_id, t.title as Todos_title, t.isDone as Todos_isDone, t.userId as Todos_userId FROM { Table } u " +
                    $"LEFT JOIN dbo.Todos t ON u.id = t.userId WHERE u.id = {id}";

                dynamic flat = conn.Query<dynamic>(sql);

                Slapper.AutoMapper.Configuration.AddIdentifiers(typeof(User), new List<string> { "id" });
                Slapper.AutoMapper.Configuration.AddIdentifiers(typeof(Todo), new List<string> { "id" });

                var result = (Slapper.AutoMapper.MapDynamic<User>(flat) as IEnumerable<User>).ToList();

                var userFound = result.Find(u => u.Id == id);

                return userFound;
            }
        }
        public User Create(User obj)
        {
            using (var conn = new SqlConnection(Connection))
            {
                var parameters = new DynamicParameters();
                parameters.Add("LoginName", obj.LoginName);
                parameters.Add("FirstName", obj.FirstName);
                parameters.Add("LastName", obj.LastName);
                parameters.Add("Age", obj.Age);
                parameters.Add("Password", obj.Password);
                parameters.Add("Email", obj.Email);


                string sql = $"INSERT INTO {Table} (LoginName, FirstName, LastName, Age, Password, Email) VALUES (@LoginName, @FirstName, @LastName, @Age, @Password, @Email)";

                conn.Execute(sql, parameters);

                return obj;
            }
        }
        public void Update(User obj)
        {
            using (var conn = new SqlConnection(Connection))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", obj.Id);
                parameters.Add("LoginName", obj.LoginName);
                parameters.Add("FirstName", obj.FirstName);
                parameters.Add("LastName", obj.LastName);
                parameters.Add("Age", obj.Age);
                parameters.Add("Password", obj.Password);
                parameters.Add("Email", obj.Email);

                string sql = $"UPDATE {Table} SET LoginName = @LoginName, FirstName = @FirstName, LastName = @LastName, Age = @Age, Password = @Password, Email = @Email WHERE Id = @Id";

                conn.Execute(sql, parameters);
            }
        }
        public async Task Delete(int id)
        {
            await using (var conn = new SqlConnection(Connection))
            {
                string sql = $"DELETE FROM {Table} Where @Id = Id";
                conn.Execute(sql, new { Id = id });
            }
        }

        public async Task<User> FindUser(Login user)
        {
            await using (var conn = new SqlConnection(Connection))
            {
                string sql = $"SELECT * FROM { Table } WHERE loginName = @loginName";
                return conn.QueryFirstOrDefault<User>(sql, new { loginName = user.Username });
            }
        }
    }
}
