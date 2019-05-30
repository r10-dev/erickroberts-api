using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace test.helpers
{
    public static class GeneralHelpers
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