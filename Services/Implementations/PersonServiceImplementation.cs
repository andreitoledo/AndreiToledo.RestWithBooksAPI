﻿using AndreiToledo.RestWithBooksAPI.Model;
using System.Collections.Generic;
using System.Threading;

namespace AndreiToledo.RestWithBooksAPI.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private volatile int count;

        public Person FindByID(long id)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "Joaquim",
                LastName = "Silva",
                Address = "São Paulo - Capital - Brasil",
                Gender = "Male"
            };
        }

        public List<Person> FindAll()
        {
            List<Person> persons = new List<Person>();
            for (int i = 0; i < 8; i++)
            {
                Person person = MockPerson(i);
                persons.Add(person);
            }
            return persons;
        }

        public Person Create(Person person)
        {
            return person;
        }

        public Person Update(Person person)
        {
            return person;
        }

        public void Delete(long id)
        {
            
        }        

        private Person MockPerson(int i)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "Person Name" + i,
                LastName = "Person LastName" + i,
                Address = "Address" + i,
                Gender = "Male"
            };
        }

        // toda vez que chmama o método, incrementa um numero a mais
        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }
    }
}
