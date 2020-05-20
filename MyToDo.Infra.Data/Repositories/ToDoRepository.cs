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
    public class ToDoRepository : Repository, IRepository<Todo>
    {
        public ToDoRepository(IConfiguration configuration) : base("dbo.Todos", configuration) { }

        public IList<Todo> GetAll()
        {
            using (var conn = new SqlConnection(Connection))
            {
                //   string sql = $"SELECT Id, Title, IsDone, UserId FROM {Table}";
                //var result = conn.Query<Todo>(sql).ToList();

                string sql = $"SELECT * FROM {Table} t INNER JOIN dbo.Users u ON u.Id = t.userId";

                var result = conn.Query<Todo, User, Todo>(sql, map: (todo, user) =>
                    {
                        todo.User = user;
                        return todo;
                    }, splitOn: "Id,Id").ToList();

                return result;
            }
        }
        public async Task<Todo> GetById(int id)
        {
            await using (var conn = new SqlConnection(Connection))
            {
                //string sql = $"SELECT Id, Title, IsDone, UserId FROM {Table} WHERE Id = {id}";
                string sql = $"SELECT * FROM {Table} E INNER JOIN dbo.Users R ON R.Id = E.userId WHERE Id = {id}";

               // var result = conn.Query<Todo>(sql).FirstOrDefault();

                var result = conn.Query<Todo, User, Todo>(sql, map: (todo, user) =>
                {
                    todo.User = user;
                    return todo;
                }, splitOn: "Id,Id").FirstOrDefault();

                return result;
            }
        }
        public Todo Create(Todo obj)
        {
            using (var conn = new SqlConnection(Connection))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", obj.Id);
                parameters.Add("Title", obj.Title);
                parameters.Add("IsDone", obj.IsDone);
                parameters.Add("UserId", obj.Id);

                string sql = $"INSERT INTO {Table} (Title, IsDone, UserId) VALUES (@Title, @IsDone, @UserId)";

                conn.Execute(sql, parameters);

                return obj;
            }
        }
        public void Update(Todo obj)
        {
            using (var conn = new SqlConnection(Connection))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", obj.Id);
                parameters.Add("Title", obj.Title);
                parameters.Add("IsDone", obj.IsDone);

                string sql = $"UPDATE {Table} SET Title = @Title, IsDone = @IsDone WHERE Id = @Id";

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
    }
}
