using System;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Reflection;
using DbUp;

namespace migration
{
    class Program
    {
        static int Main(string[] args)
        {
            var connectionString = getConnectionString("developmentConnection");
            EnsureDatabase.For.PostgresqlDatabase(connectionString);
            var upgrader =
                DeployChanges.To
                    .PostgresqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
#if DEBUG
        Console.ReadLine();
#endif
                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            return 0;
        }

        public static string getConnectionString(string connectionType)
        {
            JObject config = JObject.Parse(File.ReadAllText(@"../api/appsettings.json"));
            return (string)config["ConnectionStrings"][connectionType];
        }
    }


}
