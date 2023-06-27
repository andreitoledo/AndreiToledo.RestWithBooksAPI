using AndreiToledo.RestWithBooksAPI.Model;
using AndreiToledo.RestWithBooksAPI.Model.Context;
using AndreiToledo.RestWithBooksAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AndreiToledo.RestWithBooksAPI.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {

        private readonly IPersonRepository _repository;

        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
        }

        // NESSA CLASSE TAMBÉM IMPLEMENTARIA AS REGRAS DE NEGÓCIO.
       
        public List<Person> FindAll()
        {

            return _repository.FindAll();
        }

        // Método responsável por retornar uma pessoa        
        public Person FindByID(long id)
        {
            return _repository.FindByID(id);
        }

        // Método responsável por criar uma nova pessoa.        
        public Person Create(Person person)
        {
            return _repository.Create(person);  
        }

        // Método responsável por atualizar uma pessoa para        
        public Person Update(Person person)
        {   
            {
                return _repository.Update(person);
            } 
        }        

        // Método responsável por deletar uma pessoa de um ID
        public void Delete(long id)
        {
            _repository.Delete(id);

        }

    }
}
