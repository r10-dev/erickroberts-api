using System;
using Microsoft.Extensions.Configuration;
namespace test.helpers
{
    public class GeneralHelpers
    {


       public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
                return config;
        }

       
    }
}