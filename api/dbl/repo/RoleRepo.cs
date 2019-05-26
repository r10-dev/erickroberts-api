namespace api.dbl.repo
{
    using api.models;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Configuration;
    using Dapper;
    using System.Data;
    using Npgsql;
    using Microsoft.Extensions.Logging;

    public class RoleRepo : IRepository<Role>
    {
        private string connectionString;

        public RoleRepo(IConfiguration configuration)
        {
            connectionString = configuration.GetValue<string>("ConnectionStrings:developmentConnection");

        }
        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }

        public void Add(Role item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute(@"INSERT INTO er.Role (title, permission) 
                                    VALUES(@role, @title, @permission)", item);
            }

        }
        public string getConnectionString()
        {
            return $"ConnectionString: {connectionString}";
        }
        public IEnumerable<Role> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {

                dbConnection.Open();
                return dbConnection.Query<Role>("SELECT * FROM er.Role");
            }
        }

        public Role FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Role>("SELECT * FROM Role WHERE Roleid = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM Role WHERE Roleid=@Id", new { Id = id });
            }
        }

        public void Update(Role item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE Role SET title = @title,  permission = @permission WHERE Roleid = @Id", item);
            }
        }
    }
}