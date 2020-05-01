using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SQLite;

namespace SQLite_Demo
{
    class Program
    {
        private static PersonService _personService;
        static void Main(string[] args)
        {
            _personService = new PersonService();

            foreach (var person in _personService.GetPeople())
            {
                Console.WriteLine(person.ToString());
            }
            Console.ReadLine();
        }
    }
}
