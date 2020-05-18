using Microsoft.Extensions.DependencyInjection;
using SQLiteLibrary.CrudServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace SQLiteLibrary
{
    public static class Util
    {
        public static IServiceProvider ConfigureServices()
        {
           return new ServiceCollection()
                .AddSingleton<IDBUtills, DBUtills>()
                .AddSingleton<IPersonService, PersonService>()
                .BuildServiceProvider();
        }

        public static IServiceProvider ConfigureServicesDapper()
        {
            return new ServiceCollection()
                 .AddSingleton<IPersonService, PersonServiceDapper>()
                 .BuildServiceProvider();
        }
        public static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
