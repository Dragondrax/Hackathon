using Microsoft.AspNetCore.Mvc;

namespace MedicalHealth.Fiap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        [HttpPost("Criar")]
        public async Task<IActionResult> SalvarNovoMedico([FromQuery] string novoMedico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            return Ok();
        }

        [HttpGet("BuscarPorCRM")]
        public async Task<IActionResult> BuscarMedicoPorCRM([FromQuery] string crm)
        {
            if (string.IsNullOrEmpty(crm))
            {
                return BadRequest("CRM é obrigatório.");
            }



            return Ok();
        }
    }
}
