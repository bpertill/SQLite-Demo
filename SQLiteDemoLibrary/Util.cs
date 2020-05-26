using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SQLiteLibrary.CrudServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace SQLiteLibrary
{
    //TODO Configure services should be in the respective project, Console Application e.g.
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
            // Build configuration
          var  configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();



            return new ServiceCollection()
                 .AddSingleton<IPersonService, PersonServiceDapper>()
                 .AddSingleton<IConfigurationRoot>(configuration)
                 .BuildServiceProvider();
        }
    }
}
