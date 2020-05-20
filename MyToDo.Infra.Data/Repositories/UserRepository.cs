using Dapper;
using Microsoft.Extensions.Configuration;
using MyToDo.Domain.Entities;
using MyToDo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Infra.Data.Repositories
{
    public class UserRepository : Repository, IRepository<User>
    {
        public UserRepository(IConfiguration configuration) : base("dbo.Users", configuration) { }

        public User Create(User obj)
        {
            using (var conn = new SqlConnection(Connection))
            {
                var parameters = new DynamicParameters();
                parameters.Add("FirstName", obj.FirstName);
                parameters.Add("LastName", obj.LastName);
                parameters.Add("Age", obj.Age);

                string sql = $"INSERT INTO {Table} (FirstName, LastName, Age) VALUES (@FirstName, @LastName, @Age)";

                conn.Execute(sql, parameters);

                return obj;
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

        public IList<User> GetAll()
        {
            using (var conn = new SqlConnection(Connection))
            {
                string sql = $"SELECT Id, FirstName, LastName, Age FROM {Table}";

                var result = conn.Query<User>(sql).ToList();

                return result;
            }
        }

        public async Task<User> GetById(int id)
        {
           await using (var conn = new SqlConnection(Connection))
            {
                string sql = $"SELECT Id, FirstName, LastName, Age FROM {Table} WHERE Id = {id}";

                var result = conn.Query<User>(sql).FirstOrDefault();

                return result;
            }
        }

        public void Update(User obj)
        {
            using (var conn = new SqlConnection(Connection))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", obj.Id);
                parameters.Add("FirstName", obj.FirstName);
                parameters.Add("LastName", obj.LastName);
                parameters.Add("Age", obj.Age);

                string sql = $"UPDATE {Table} SET FirstName = @FirstName, LastName = @LastName, Age = @Age WHERE Id = @Id";

                conn.Execute(sql, parameters);
            }
        }
    }
}
