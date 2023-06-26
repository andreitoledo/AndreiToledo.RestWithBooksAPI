using AndreiToledo.RestWithBooksAPI.Model;
using System.Collections.Generic;

namespace AndreiToledo.RestWithBooksAPI.Services
{
    public interface IPersonService
    {
        Person Create(Person person);
        Person FindByID(long id);
        List<Person> FindAll();
        Person Update(Person person);
        void Delete(long id);
    }
}
