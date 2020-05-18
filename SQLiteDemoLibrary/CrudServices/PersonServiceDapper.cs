using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace SQLiteLibrary.CrudServices
{
    public class PersonServiceDapper : IPersonService
    {
        public int AddPerson(Person person)
        {
            using IDbConnection con = new SQLiteConnection(Util.LoadConnectionString());
            return con.Execute("INSERT INTO Person(FirstName, LastName, DoB) VALUES (@FirstName, @LastName, @Dob)",person);
        }

        public int DeletePerson(Person person)
        {
            using IDbConnection con = new SQLiteConnection(Util.LoadConnectionString());
            return con.Execute("DELETE FROM Person WHERE Id = @Id", person);
        }

        public List<Person> GetPeople()
        {
            using IDbConnection con = new SQLiteConnection(Util.LoadConnectionString());
            return con.Query<Person>("SELECT Id, FirstName, LastName, DoB FROM Person").ToList();
        }

        public Person GetPersonById(int id)
        {
            using IDbConnection con = new SQLiteConnection(Util.LoadConnectionString());
            return con.Query<Person>("SELECT Id,FirstName, LastName, DoB FROM Person WHERE Id = @Id", new {Id = id }).FirstOrDefault();
        }

        public int UpdatePerson(Person person)
        {
            using IDbConnection con = new SQLiteConnection(Util.LoadConnectionString());
            return con.Execute("UPDATE Person SET FirstName = @firstname, LastName = @lastName, DoB = @dob WHERE Id = @Id", person);
        }
    }
}
