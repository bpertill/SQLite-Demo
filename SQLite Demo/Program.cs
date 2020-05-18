using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SQLite;
using Microsoft.Extensions.DependencyInjection;
using SQLiteLibrary;
using SQLiteLibrary.CrudServices;
using System.Globalization;

namespace SQLiteConsoleApplication
{
    public class Program
    {
        private static IServiceProvider _services;
        private static IPersonService _personService;
        static void Main()
        {
            _services = Util.ConfigureServicesDapper();
            _personService = _services.GetService<IPersonService>();

            BusinessLogic();
        }

        private static void BusinessLogic()
        {
            bool exit = false;

            PrintUsage();
            while (!exit)
            {
                switch (Console.ReadLine())
                {
                    case "print person":
                        PrintPerson();
                        break;
                    case "print people":
                        PrintPeople();
                        break;
                    case "add person":
                        AddPerson();
                        break;
                    case "delete person":
                        DeletePerson();
                        break;
                    case "update person":
                        UpdatePerson();
                        break;
                    case "clear screen":
                        Console.Clear();
                        break;
                    case "exit":
                        exit = true;
                        break;
                    default:
                        PrintUsage();
                        break;
                }
            }
        }
        private static void PrintPerson()
        {
           Console.WriteLine(_personService.GetPersonById(ReadoIdFromConsole()));
        }

        private static void PrintPeople()
        {
            foreach (var person in _personService.GetPeople())
            {
                Console.WriteLine(person.ToString());
            }
        }
        private static void AddPerson()
        {
            _personService.AddPerson(ReadPersonFromConsole());
        }
        private static void DeletePerson()
        {
            _personService.DeletePerson(new Person { Id = ReadoIdFromConsole() });
        }
        private static void UpdatePerson()
        {
            int id = ReadoIdFromConsole();
            var p = ReadPersonFromConsole();
            p.Id = id;

            _personService.UpdatePerson(p);
        }
 
        private static int ReadoIdFromConsole()
        {
            string input;
            do
            {
                Console.WriteLine("Enter numeric Id");
                input = Console.ReadLine();
            } while (!string.IsNullOrEmpty(input) && input.Any(c => !Char.IsDigit(c)));
            return Int32.Parse(input);
        }
        private static Person ReadPersonFromConsole()
        {
            string input;
            Console.WriteLine("Enter first name");
            string firstname = Console.ReadLine();

            Console.WriteLine("Enter last name");
            string lastname = Console.ReadLine();

            DateTime dob;
            do
            {
                Console.WriteLine("Enter date of birth (ddmmyyyy)");
                input = Console.ReadLine();
            }
            while (!DateTime.TryParseExact(input, "ddmmyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dob));

            return new Person { FirstName = firstname, LastName = lastname, DoB = dob };
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Usage: [print person] [print people] [add person] [delete person] [update person] [clear screen] [exit]");
        }
    }
}
