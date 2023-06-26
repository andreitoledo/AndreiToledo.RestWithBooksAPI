using AndreiToledo.RestWithBooksAPI.Model;
using System.Collections.Generic;
using System.Threading;

namespace AndreiToledo.RestWithBooksAPI.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        // Contador responsável por gerar um ID falso
        // já que não estamos acessando nenhum banco de dados
        private volatile int count;

        // Método responsável por retornar uma pessoa
        // como não acessamos nenhum banco de dados estamos retornando um mock
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

        // Método responsável por criar uma nova pessoa.
        // Se tivéssemos um banco de dados este seria o momento de persistir os dados
        public Person Create(Person person)
        {
            return person;
        }

        // Método responsável por atualizar uma pessoa para
        // sendo mock retornamos a mesma informação passada
        public Person Update(Person person)
        {
            return person;
        }

        // Método responsável por deletar uma pessoa de um ID
        public void Delete(long id)
        {
            // Nossa lógica de exclusão viria aqui
        }

        // Método responsável por retornar todas as pessoas,
        // novamente esta informação é um mocks
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
