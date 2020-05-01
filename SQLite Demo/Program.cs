using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SQLite;

namespace SQLite_Demo
{
    class Program
    {
        public static string Connectionstring = "Data Source=.\\DemoSQLiteDb.db ";
        static void Main(string[] args)
        {

            foreach (var person in GetPeople())
            {
                Console.WriteLine(person.ToString());
            }
            Console.ReadLine();

        }

        private static int AddPerson(Person person)
        {
            var query = "INSERT INTO Person(FirstName,LastName, DoB) VALUES (@firstName, @lastName, @dob)";
            var args = new Dictionary<string, object>
            {
                {"@firstName",person.FirstName },
                {"@lastName",person.LastName },
                {"@dob",person.DoB }
            };
            return ExecuteWrite(query, args);
        }
        private static int DeletePerson(Person person)
        {
            var query = "DELETE FROM Person WHERE Id = @id";
            var args = new Dictionary<string, object>
            {
                {"@id",person.Id }
            };
            return ExecuteWrite(query, args);
        }

        private static int UpdatePerson(Person person)
        {
            var query = " UPDATE Person SET FirstName = @firstname, LastName = @lastName, DoB = @dob WHERE Id = @id";
            var args = new Dictionary<string, object>
            {
                {"@firstName",person.FirstName },
                {"@lastName",person.LastName },
                {"@dob",person.DoB }
            };
            return ExecuteWrite(query, args);
        }
        private static Person GetPersonById(int id)
        {
            var query = "SELECT Id,FirstName, LastName, DoB FROM Person WHERE Id = @id";

            var args = new Dictionary<string, object>
            {
                {"@id",id }
            };

            DataTable dt = ExecuteRead(query, args);
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

        private static List<Person> GetPeople()
        {
            var query = "SELECT Id,FirstName, LastName, DoB FROM Person";
            DataTable dt = ExecuteRead(query);
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

        private static DataTable ExecuteRead(string query, Dictionary<string, object> args = null)
        {
            if (string.IsNullOrEmpty(query.Trim()))
                return null;

            using var con = new SQLiteConnection(Connectionstring);
            con.Open();

            using var cmd = CreateCommandWithParameters(query, con, args);

            using var da = new SQLiteDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        private static int ExecuteWrite(string query, Dictionary<string, object> args = null)
        {
            using var con = new SQLiteConnection(Connectionstring);
            con.Open();

            using var cmd = CreateCommandWithParameters(query, con, args);

            return cmd.ExecuteNonQuery();
        }

        private static SQLiteCommand CreateCommandWithParameters(string query, SQLiteConnection con, Dictionary<string, object> args = null)
        {
            var cmd = new SQLiteCommand(query, con);
            if (args != null)
            {
                foreach (KeyValuePair<string, object> entry in args)
                {
                    cmd.Parameters.AddWithValue(entry.Key, entry.Value);
                }
            }
            return cmd;
        }
    }
}
