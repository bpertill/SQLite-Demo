using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SQLite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SQLite_Demo
{
    public class Program
    {
        private static IServiceProvider _services;
        static void Main(string[] args)
        {
            ConfigureServices();

            var personService = _services.GetService<IPersonService>();
            foreach (var person in personService.GetPeople())
            {
                Console.WriteLine(person.ToString());
            }
            Console.ReadLine();
        }
        public static void ConfigureServices()
        {
            _services = new ServiceCollection()
                .AddSingleton<IDBUtills, DBUtills>()
                .AddSingleton<IPersonService, PersonService>()
                .BuildServiceProvider();
        }
    }
}
