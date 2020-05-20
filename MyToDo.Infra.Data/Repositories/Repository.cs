using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MyToDo.Infra.Data.Repositories
{
    public abstract class Repository
    {
        protected string Table { get; }
        protected string Sql { get; }
        protected string Connection { get; }

        public Repository(string table, IConfiguration configuration)
        {
            Table = table;
            Connection = configuration.GetConnectionString("sqlConnection");
        } 
    }
}
