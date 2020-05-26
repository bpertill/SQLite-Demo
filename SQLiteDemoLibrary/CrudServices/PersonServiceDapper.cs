using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteLibrary.CrudServices
{
    public class PersonServiceDapper : IPersonService
    {
        private readonly IConfiguration _configuration;
        public PersonServiceDapper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> AddPerson(Person person)
        {
            using IDbConnection con = new SQLiteConnection(_configuration.GetConnectionString("Default"));
            return await con.ExecuteAsync("INSERT INTO Person(FirstName, LastName, DoB) VALUES (@FirstName, @LastName, @Dob)",person);
        }

        public async Task<int> DeletePerson(Person person)
        {
            using IDbConnection con = new SQLiteConnection(_configuration.GetConnectionString("Default"));
            return await con.ExecuteAsync("DELETE FROM Person WHERE Id = @Id", person);
        }

        public async Task<IEnumerable<Person>> GetPeople()
        {
            using IDbConnection con = new SQLiteConnection(_configuration.GetConnectionString("Default"));
            var output = await con.QueryAsync<Person>("SELECT Id, FirstName, LastName, DoB FROM Person");
            return output;
        }

        public async Task<Person> GetPersonById(int id)
        {
            using IDbConnection con = new SQLiteConnection(_configuration.GetConnectionString("Default"));
            var output = await con.QueryAsync<Person>("SELECT Id,FirstName, LastName, DoB FROM Person WHERE Id = @Id", new { Id = id });
            return output.FirstOrDefault();
        }
        
        public async Task<int> UpdatePerson(Person person)
        {
            using IDbConnection con = new SQLiteConnection(_configuration.GetConnectionString("Default"));
            return await con.ExecuteAsync("UPDATE Person SET FirstName = @firstname, LastName = @lastName, DoB = @dob WHERE Id = @Id", person);
        }
    }
}
