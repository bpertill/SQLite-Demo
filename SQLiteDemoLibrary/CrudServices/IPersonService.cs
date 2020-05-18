using System.Collections.Generic;

namespace SQLiteDemoLibrary
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