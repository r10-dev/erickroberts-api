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

    public class UserRoleRepo : IRepository<UserRole>
    {
        private string connectionString;

        public UserRoleRepo(IConfiguration configuration)
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

        public void Add(UserRole item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute(@"INSERT INTO er.UserRole (userid) 
                                    VALUES(@userid)", item);
            }

        }
        public string getConnectionString()
        {
            return $"ConnectionString: {connectionString}";
        }
        public IEnumerable<UserRole> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {

                dbConnection.Open();
                return dbConnection.Query<UserRole>("SELECT * FROM er.UserRole");
            }
        }

        public UserRole FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<UserRole>("SELECT * FROM UserRole WHERE UserRoleid = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM UserRole WHERE UserRoleid=@Id", new { Id = id });
            }
        }

        public void Update(UserRole item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE UserRole SET userid = @userid WHERE UserRoleid = @Id", item);
            }
        }
    }
}