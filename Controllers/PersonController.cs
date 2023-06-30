using AndreiToledo.RestWithBooksAPI.Business;
using AndreiToledo.RestWithBooksAPI.Data.VO;
using AndreiToledo.RestWithBooksAPI.Hypermedia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace AndreiToledo.RestWithBooksAPI.Controllers
/**
O cotroller roteia a requisição do Client, define o método através da rota(Route)
O método invoca o Businnes e o Businnes valida as regras de negócio.
Assim que as regras de negócio são validades, ele faz a chamada para o repository
O repository acessa base de dados persistindo ou recuperando as informações.     
 */
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : ControllerBase
    {        
        private readonly ILogger<PersonController> _logger;

        // Declaração do serviço utilizado
        private IPersonBusiness _personBusiness;

        // Injeção de uma instância de IPersonService
        // ao criar uma instância de PersonController
        public PersonController(ILogger<PersonController> logger, IPersonBusiness personService)
        {
            _logger = logger;
            _personBusiness = personService;
        }

        // Mapeia requisições GET para https://localhost:{port}/api/person
        // Não obtém parâmetros para FindAll -> Search All
        [HttpGet]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }

        // Mapeia as solicitações GET para https://localhost:{port}/api/person/{id}
        // recebendo um ID como no Request Path
        // Obter com parâmetros para FindById -> Pesquisar por ID
        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindByID(id);
            if (person == null) return NotFound();
            
            return Ok(person);
        }

        [HttpGet("findPersonByName")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var person = _personBusiness.FindByName(firstName, lastName);
            if (person == null) return NotFound();
            return Ok(person);
        }

        // Mapeia as solicitações POST para https://localhost:{port}/api/person/
        // [FromBody] consome o objeto JSON enviado no corpo da requisição
        [HttpPost]
        [ProducesResponseType((200), Type = typeof(PersonVO))]        
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] PersonVO person)
        {            
            if (person == null) return BadRequest();
            
            return Ok(_personBusiness.Create(person));
        }

        // Mapeia as solicitações PUT para https://localhost:{port}/api/person/
        // [FromBody] consome o objeto JSON enviado no corpo da requisição
        [HttpPut]
        [ProducesResponseType((200), Type = typeof(PersonVO))]        
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();

            return Ok(_personBusiness.Update(person));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Patch(long id)
        {
            var person = _personBusiness.Disable(id);
            return Ok(person);
        }

        // Mapeia solicitações DELETE para https://localhost:{port}/api/person/{id}
        // recebendo um ID como no Request Path
        [HttpDelete("{id}")]        
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }

    }
}
