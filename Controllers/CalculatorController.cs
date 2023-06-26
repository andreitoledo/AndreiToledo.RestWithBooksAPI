using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace AndreiToledo.RestWithBooksAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        
        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet("sum/{firstNumber}/{secondNumber}")]
        public IActionResult Get(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);
                return Ok(sum.ToString());
            }
            
            return BadRequest("Invalid Input");
        }

        // Converte o numero informado que esta como string para decimal
        private int ConvertToDecimal(string firstNumber)
        {
            throw new NotImplementedException();
        }

        // Valida se o numero informado é numerico
        private bool IsNumeric(string firstNumber)
        {
            throw new NotImplementedException();
        }
    }
}
