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
           IDBUtills dbu = new DBUtills();
            DBUtills dbu2 = new DBUtills();

            dbu.LoadConnectionString();
            ((IDBUtills)dbu2).LoadConnectionString();
           return new ServiceCollection()
                .AddSingleton<IDBUtills, DBUtills>()
                .AddSingleton<IPersonService, PersonService>()
                .BuildServiceProvider();
        }
    }
}
