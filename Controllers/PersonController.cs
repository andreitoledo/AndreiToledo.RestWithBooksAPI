using AndreiToledo.RestWithBooksAPI.Model;
using AndreiToledo.RestWithBooksAPI.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }

        // Mapeia as solicitações GET para https://localhost:{port}/api/person/{id}
        // recebendo um ID como no Request Path
        // Obter com parâmetros para FindById -> Pesquisar por ID
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindByID(id);
            if (person == null) return NotFound();
            
            return Ok(person);
        }

        // Mapeia as solicitações POST para https://localhost:{port}/api/person/
        // [FromBody] consome o objeto JSON enviado no corpo da requisição
        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {            
            if (person == null) return BadRequest();
            
            return Ok(_personBusiness.Create(person));
        }

        // Mapeia as solicitações PUT para https://localhost:{port}/api/person/
        // [FromBody] consome o objeto JSON enviado no corpo da requisição
        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {
            if (person == null) return BadRequest();

            return Ok(_personBusiness.Update(person));
        }

        // Mapeia solicitações DELETE para https://localhost:{port}/api/person/{id}
        // recebendo um ID como no Request Path
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }

    }
}
