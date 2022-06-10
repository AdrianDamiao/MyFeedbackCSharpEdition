using System;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace MyFeedback.Webapi.ExtensionMethods
{
    public static class ConectorHeroku 
    {
        // teste
        public static string GetHerokuConnectionString(
            this IConfiguration configuration)
        {
            if(IsEnvironmentProduction())
            {
                var host = Environment.GetEnvironmentVariable("Host");
                var port = Environment.GetEnvironmentVariable("Port");
                var username = Environment.GetEnvironmentVariable("Username");
                var password = Environment.GetEnvironmentVariable("Password");
                var database = Environment.GetEnvironmentVariable("Database");

                // var databaseUri = new Uri(databaseUrl);
                // var userInfo = databaseUri.UserInfo.Split(':');

                var builder = new NpgsqlConnectionStringBuilder
                {
                    Host = host,
                    Port = port,
                    Username = username,
                    Password = password,
                    Database = database
                };

                return builder.ToString();
            }

            return configuration.GetConnectionString("Default");
        }

        public static string GetHerokuUrl()
        {
            if(IsEnvironmentProduction())
                return "http://*:" + Environment.GetEnvironmentVariable("PORT");
            
            
            return "http://localhost:5001";
        }

        public static bool IsEnvironmentProduction()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Production");
        }
    }
}