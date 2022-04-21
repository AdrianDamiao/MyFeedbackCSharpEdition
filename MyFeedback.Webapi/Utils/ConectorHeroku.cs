using System;
using System.Threading.Tasks;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace MyFeedback.Webapi.Utils
{
    public static class ConectorHeroku 
    {
        public static string GetHerokuConnectionString(
            this IConfiguration configuration)
        {
            if(IsEnvironmentProduction())
            {
                var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
                var databaseUri = new Uri(databaseUrl);
                var userInfo = databaseUri.UserInfo.Split(':');

                var builder = new NpgsqlConnectionStringBuilder
                {
                    Host = databaseUri.Host,
                    Port = databaseUri.Port,
                    Username = userInfo[0],
                    Password = userInfo[1],
                    Database = databaseUri.LocalPath.TrimStart('/')
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

        private static bool IsEnvironmentProduction()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Production");
        }
    }
}