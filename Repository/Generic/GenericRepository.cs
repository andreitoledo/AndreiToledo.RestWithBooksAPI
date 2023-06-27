using AndreiToledo.RestWithBooksAPI.Model.Base;
using System;
using System.Collections.Generic;

namespace AndreiToledo.RestWithBooksAPI.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        public T Create(T item)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public List<T> FindAll()
        {
            throw new NotImplementedException();
        }

        public T FindByID(long id)
        {
            throw new NotImplementedException();
        }

        public T Update(T item)
        {
            throw new NotImplementedException();
        }
    }
}
