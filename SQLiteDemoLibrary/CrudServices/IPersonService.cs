using System.Collections.Generic;
using System.Threading.Tasks;

namespace SQLiteLibrary.CrudServices
{
    public interface IPersonService
    {
        Task<int> AddPerson(Person person);
        Task<int> DeletePerson(Person person);
        Task<IEnumerable<Person>> GetPeople();
        Task<Person> GetPersonById(int id);
        Task<int> UpdatePerson(Person person);
    }
}