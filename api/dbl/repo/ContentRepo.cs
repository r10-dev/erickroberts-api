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

    public class ContentRepo : IRepository<Content>
    { 
        private string connectionString;

        public ContentRepo(IConfiguration configuration)
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

        public void Add(Content item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute(@"INSERT INTO er.content
                                        (slug, authorid, title, headerimage, tabimage, views, stars, body, published, staged, draft, created_on, published_on)
                                        VALUES(@slug, @authorid, @title, @headerimage, @tabimage, @views, @stars, @body, @published, @staged, @draft, @created_on, @published_on)", item);
                                                    }

        }
        public string getConnectionString()
        {
            return $"ConnectionString: {connectionString}";
        }
        public IEnumerable<Content> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {

                dbConnection.Open();
                return dbConnection.Query<Content>("SELECT * FROM er.Content");
            }
        }

        public Content FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Content>("SELECT * FROM er.content WHERE contentid = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM er.Content WHERE contentid=@Id", new { Id = id });
            }
        }

        public void Update(Content item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE er.Content SET slug = @slug, authorid = @authorid, title = @title, headerimage = @headerimage, tabimage = @tabimage, views = @views, stars = @stars, body = @body, published = @published, staged = @staged, draft = @draft, created_on = @created_on, published_on = @created_on WHERE contentid = @contentid", item);
            }
        }


    }

}