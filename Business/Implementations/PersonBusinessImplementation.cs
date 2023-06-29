using AndreiToledo.RestWithBooksAPI.Data.Converter.Implementations;
using AndreiToledo.RestWithBooksAPI.Data.VO;
using AndreiToledo.RestWithBooksAPI.Model;
using AndreiToledo.RestWithBooksAPI.Repository;
using System.Collections.Generic;

namespace AndreiToledo.RestWithBooksAPI.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _repository;

        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        // NESSA CLASSE TAMBÉM IMPLEMENTARIA AS REGRAS DE NEGÓCIO.
       
        public List<PersonVO> FindAll()
        {

            return _converter.Parse(_repository.FindAll());
        }

        // Método responsável por retornar uma pessoa        
        public PersonVO FindByID(long id)
        {
            return _converter.Parse(_repository.FindByID(id));
        }

        // Método responsável por criar uma nova pessoa.
        // A variavel chega em PersonVO e não da para persistir na base de dados
        // É preciso parsear para uma a variavel personEntity e posso persistir
        // O resultado é devolvido para a variavel gerado um autoincremental
        // Depois converte a entidade para VO e devolve a resposta.

        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }

        // Método responsável por atualizar uma pessoa para        
        public PersonVO Update(PersonVO person)
        {   
            {
                var personEntity = _converter.Parse(person);
                personEntity = _repository.Update(personEntity);
                return _converter.Parse(personEntity);
            } 
        }

        // Método responsável por desabilitar uma pessoa de um ID
        public PersonVO Disable(long id)
        {
            var personEntity = _repository.Disable(id);
            return _converter.Parse(personEntity);
        }

        // Método responsável por deletar uma pessoa de um ID
        public void Delete(long id)
        {
            _repository.Delete(id);

        }


    }
}
