using System.Collections.Generic;

namespace SQLiteLibrary.CrudServices
{
    public interface IPersonService
    {
        int AddPerson(Person person);
        int DeletePerson(Person person);
        List<Person> GetPeople();
        Person GetPersonById(int id);
        int UpdatePerson(Person person);
    }
}