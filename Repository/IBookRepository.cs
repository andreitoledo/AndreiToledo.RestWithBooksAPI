﻿using RestWithASPNETUdemy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndreiToledo.RestWithBooksAPI.Repository
{
    public interface IBookRepository
    {
        Book Create(Book book);
        Book FindByID(long id);
        List<Book> FindAll();
        Book Update(Book book);
        void Delete(long id);
    }
}
