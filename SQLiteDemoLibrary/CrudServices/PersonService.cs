using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SQLiteLibrary.CrudServices
{
    public class PersonService : IPersonService
    {
        private readonly IDBUtills _dBUtills;
        public PersonService(IDBUtills dBUtills)
        {
            _dBUtills = dBUtills;
        }

        public int AddPerson(Person person)
        {
            var query = "INSERT INTO Person(FirstName,LastName, DoB) VALUES (@firstName, @lastName, @dob)";
            var args = new Dictionary<string, object>
            {
                {"@firstName",person.FirstName },
                {"@lastName",person.LastName },
                {"@dob",person.DoB }
            };
            return _dBUtills.ExecuteWrite(query, args);
        }
        public int DeletePerson(Person person)
        {
            var query = "DELETE FROM Person WHERE Id = @id";
            var args = new Dictionary<string, object>
            {
                {"@id",person.Id }
            };
            return _dBUtills.ExecuteWrite(query, args);
        }

        public int UpdatePerson(Person person)
        {
            var query = "UPDATE Person SET FirstName = @firstname, LastName = @lastName, DoB = @dob WHERE Id = @id";
            var args = new Dictionary<string, object>
            {
                {"@firstName",person.FirstName },
                {"@lastName",person.LastName },
                {"@dob",person.DoB }
            };
            return _dBUtills.ExecuteWrite(query, args);
        }
        public Person GetPersonById(int id)
        {
            var query = "SELECT Id,FirstName, LastName, DoB FROM Person WHERE Id = @id";

            var args = new Dictionary<string, object>
            {
                {"@id",id }
            };

            DataTable dt = _dBUtills.ExecuteRead(query, args);
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            var person = new Person
            {
                Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                FirstName = Convert.ToString(dt.Rows[0]["FirstName"]),
                LastName = Convert.ToString(dt.Rows[0]["LastName"]),
                DoB = Convert.ToDateTime(dt.Rows[0]["DoB"])
            };

            return person;
        }

        public List<Person> GetPeople()
        {
            var query = "SELECT Id,FirstName, LastName, DoB FROM Person";
            DataTable dt = _dBUtills.ExecuteRead(query);
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            List<Person> people = new List<Person>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                people.Add(
                    new Person
                    {
                        Id = Convert.ToInt32(dt.Rows[i]["Id"]),
                        FirstName = Convert.ToString(dt.Rows[i]["FirstName"]),
                        LastName = Convert.ToString(dt.Rows[i]["LastName"]),
                        DoB = Convert.ToDateTime(dt.Rows[i]["DoB"])
                    });
            }
            return people;
        }
    }
}
