using System;
using System.Collections.Generic;
using System.Text;

namespace SQLiteLibrary
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DoB { get; set; }

        public override string ToString()
        {
            return $"{Id}: {FirstName} {LastName} {DoB}";
        }
    }
}
