using AndreiToledo.RestWithBooksAPI.Model;
using AndreiToledo.RestWithBooksAPI.Model.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AndreiToledo.RestWithBooksAPI.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {

        private MySQLContext _context;

        public PersonServiceImplementation(MySQLContext context)
        {
            _context = context;
        }

        // Método responsável por retornar uma pessoa
        // como não acessamos nenhum banco de dados estamos retornando um mock
        public Person FindByID(long id)
        {
            return new Person
            {
                Id = 1,
                FirstName = "Joaquim",
                LastName = "Silva",
                Address = "São Paulo - Capital - Brasil",
                Gender = "Male"
            };
        }

        public List<Person> FindAll()
        {

            return _context.Persons.ToList();
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

    }
}
