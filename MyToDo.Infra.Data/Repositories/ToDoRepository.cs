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
    public class ToDoRepository : Repository, IRepositoryAuth<Todo>
    {
        public ToDoRepository(IConfiguration configuration) : base("dbo.Todos", configuration) { }

        public IList<Todo> GetAll(int userId)
        {
            using (var conn = new SqlConnection(Connection))
            {
                string sql = $"SELECT * FROM {Table} WHERE UserId = { userId }";

                var result = conn.Query<Todo>(sql).ToList();

                return result;
            }
        }
        public async Task<Todo> GetById(int id, int userId)
        {
            await using (var conn = new SqlConnection(Connection))
            {
                string sql = $"SELECT * FROM {Table} WHERE Id = {id} AND UserId = { userId }";

                var result = conn.Query<Todo>(sql).FirstOrDefault();

                return result;
            }
        }
        public Todo Create(Todo obj, int userId)
        {
            using (var conn = new SqlConnection(Connection))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Title", obj.Title);
                parameters.Add("IsDone", obj.IsDone);
                parameters.Add("UserId", userId);

                string sql = $"INSERT INTO {Table} (Title, IsDone, UserId) VALUES (@Title, @IsDone, @UserId)";

                conn.Execute(sql, parameters);

                return obj;
            }
        }
        public void Update(Todo obj, int id, int userId)
        {
            using (var conn = new SqlConnection(Connection))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Title", obj.Title);
                parameters.Add("IsDone", obj.IsDone);

                string sql = $"UPDATE {Table} SET Title = @Title, IsDone = @IsDone WHERE Id = { id } AND UserId = { userId } ";
                conn.Execute(sql, parameters);
            }
        }
        public async Task Delete(int id, int userId)
        {
            await using (var conn = new SqlConnection(Connection))
            {
                string sql = $"DELETE FROM {Table} Where @Id = Id AND UserId = { userId }";
                conn.Execute(sql, new { Id = id });
            }
        }
    }
}
