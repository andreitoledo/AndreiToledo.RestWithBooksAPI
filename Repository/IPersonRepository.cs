using AndreiToledo.RestWithBooksAPI.Model;

namespace AndreiToledo.RestWithBooksAPI.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Disable(long id);
        
    }
}
