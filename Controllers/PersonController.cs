using AndreiToledo.RestWithBooksAPI.Model;
using AndreiToledo.RestWithBooksAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace AndreiToledo.RestWithBooksAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {        
        private readonly ILogger<PersonController> _logger;

        // Declaração do serviço utilizado
        private IPersonService _personService;

        // Injeção de uma instância de IPersonService
        // ao criar uma instância de PersonController
        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        // Mapeia requisições GET para https://localhost:{port}/api/person
        // Não obtém parâmetros para FindAll -> Search All
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personService.FindAll());
        }

        // Mapeia as solicitações GET para https://localhost:{port}/api/person/{id}
        // recebendo um ID como no Request Path
        // Obter com parâmetros para FindById -> Pesquisar por ID
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var person = _personService.FindByID(id);
            if (person == null) return NotFound();
            
            return Ok(person);
        }

        // Mapeia as solicitações POST para https://localhost:{port}/api/person/
        // [FromBody] consome o objeto JSON enviado no corpo da requisição
        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {            
            if (person == null) return BadRequest();
            
            return Ok(_personService.Create(person));
        }

        // Mapeia as solicitações PUT para https://localhost:{port}/api/person/
        // [FromBody] consome o objeto JSON enviado no corpo da requisição
        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {
            if (person == null) return BadRequest();

            return Ok(_personService.Update(person));
        }

        // Mapeia solicitações DELETE para https://localhost:{port}/api/person/{id}
        // recebendo um ID como no Request Path
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personService.Delete(id);
            return NoContent();
        }

    }
}
