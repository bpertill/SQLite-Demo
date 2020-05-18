using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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
    }
}
