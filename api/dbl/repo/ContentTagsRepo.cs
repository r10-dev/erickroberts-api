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
    using api.dbl.repo.interfaces;
    public class ContentTagsRepo : IContentTagsRepo
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
                dbConnection.Execute(@"INSERT INTO er.contenttags(url, description, tags)
                                    VALUES(@url,@description, @tags)", item);
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
                return dbConnection.Query<ContentTags>("SELECT * FROM er.ContentTags WHERE contenttagid = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM er.ContentTags WHERE contenttagid=@Id", new { Id = id });
            }
        }

        public void Update(ContentTags item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE er.ContentTags SET url = @url, description = @description, tags = @tags WHERE contenttagid = @contenttagid", item);
            }
        }
    }
}