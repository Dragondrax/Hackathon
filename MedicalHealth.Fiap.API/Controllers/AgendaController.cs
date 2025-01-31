using Microsoft.AspNetCore.Mvc;

namespace MedicalHealth.Fiap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        [HttpPost("Criar")]
        public async Task<IActionResult> SalvarNovaAgenda([FromQuery] string novaAgenda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpGet("BuscarPorData")]
        public async Task<IActionResult> BuscarAgendaData([FromQuery] string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return BadRequest("Data é obrigatório.");
            }



            return Ok();
        }
    }
}
