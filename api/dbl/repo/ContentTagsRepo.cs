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

    public class ContentTagsRepo : IRepository<ContentTags>
    {
        private string connectionString;

        public ContentTagsRepo(IConfiguration configuration)
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

        public void Add(ContentTags item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute(@"INSERT INTO contenttags(url, contentid, description, tags)
                                    VALUES(@url,@contentid,@description, @tags)", item);
            }

        }
        public string getConnectionString()
        {
            return $"ConnectionString: {connectionString}";
        }
        public IEnumerable<ContentTags> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {

                dbConnection.Open();
                return dbConnection.Query<ContentTags>("SELECT * FROM er.ContentTags");
            }
        }

        public ContentTags FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<ContentTags>("SELECT * FROM ContentTags WHERE contenttagsid = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM ContentTags WHERE ContentTagsid=@Id", new { Id = id });
            }
        }

        public void Update(ContentTags item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE ContentTags SET url = @url contentid = @contentid, description = @description, tags = @tags WHERE contenttagsid = @Id", item);
            }
        }
    }
}