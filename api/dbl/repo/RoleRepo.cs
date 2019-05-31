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
    using Newtonsoft.Json;
    using api.dbl.repo.interfaces;
    public class RoleRepo : IRoleRepo
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

            using (NpgsqlConnection dbConnection = new NpgsqlConnection(connectionString))
            {

                SqlMapper.AddTypeHandler(new RolePermissionTypeHandler());

                dbConnection.Open();
                dbConnection.Execute(@"INSERT INTO er.Role (title, permissions) 
                                    VALUES(@_title, @_permissions::json)", new { _title = item.title, _permissions = item.permissions });
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
                return dbConnection.Query<Role>("SELECT * FROM er.Role WHERE Roleid = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM er.Role WHERE Roleid=@Id", new { Id = id });
            }
        }

        public void Update(Role item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                SqlMapper.AddTypeHandler(new RolePermissionTypeHandler());
                dbConnection.Open();
                dbConnection.Query("UPDATE er.Role SET title = @_title,  permissions = @_permission::json WHERE Roleid = @roleid", new { _title = item.title, _permission = item.permissions });
            }
        }

        public string ConvertPermissions(List<RolePermissions> list)
        {
            return JsonConvert.SerializeObject(list);
        }

        public class RolePermissionTypeHandler : SqlMapper.TypeHandler<List<RolePermissions>>
        {
            public override List<RolePermissions> Parse(object value)
            {
                return JsonConvert.DeserializeObject<List<RolePermissions>>(value.ToString());
            }
            public override void SetValue(System.Data.IDbDataParameter parameter, List<RolePermissions> value)
            {
                parameter.Value = JsonConvert.SerializeObject(value);
            }
        }
    }
}