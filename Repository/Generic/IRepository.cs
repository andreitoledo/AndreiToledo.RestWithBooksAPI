using AndreiToledo.RestWithBooksAPI.Model;
using AndreiToledo.RestWithBooksAPI.Model.Base;
using System.Collections.Generic;

namespace AndreiToledo.RestWithBooksAPI.Repository

{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);
        T FindByID(long id);
        List<T> FindAll();
        T Update(T item);
        void Delete(long id);
    }
}
