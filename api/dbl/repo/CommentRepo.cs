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

    public class CommentRepo : IRepository<Comment>
    {
        private string connectionString;

        public CommentRepo(IConfiguration configuration)
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

        public void Add(Comment item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute(@"INSERT INTO er.comment(shorttext, body, userid, contentid)
                                    VALUES(@shorttext,@body,@userid,@contentid", item);
            }

        }
        public string getConnectionString()
        {
            return $"ConnectionString: {connectionString}";
        }
        public IEnumerable<Comment> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {

                dbConnection.Open();
                return dbConnection.Query<Comment>("SELECT * FROM er.Comment");
            }
        }

        public Comment FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Comment>("SELECT * FROM Comment WHERE commentid = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM Comment WHERE commentid=@Id", new { Id = id });
            }
        }

        public void Update(Comment item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE Comment SET shorttext = @, body = @body, userid = @userid, contentid = @contentid WHERE commentid = @Id", item);
            }
        }
    }
}