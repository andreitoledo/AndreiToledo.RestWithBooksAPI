using AndreiToledo.RestWithBooksAPI.Model;
using System.Collections.Generic;

namespace AndreiToledo.RestWithBooksAPI.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Disable(long id);
        List<Person> FindByName(string firstName, string secondName);

    }
}
