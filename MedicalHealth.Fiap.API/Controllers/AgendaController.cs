using MedicalHealth.Fiap.Aplicacao.Agenda;
using MedicalHealth.Fiap.Aplicacao.DTO;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;
using Microsoft.AspNetCore.Mvc;

namespace MedicalHealth.Fiap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        private readonly IAgendaMedicoService _agendaService;
        public AgendaController(IAgendaMedicoService agendaService)
        {
            _agendaService = agendaService;
        }
        [HttpPost("Criar")]
        public async Task<IActionResult> SalvarNovaAgenda([FromBody] NovaAgendaMedicoRequestModel novaAgenda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await _agendaService.SalvarNovaAgendaParaOMedico(novaAgenda);

            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Sucesso == false && resultado.Objeto is null && resultado.Mensagem.Any(x => string.IsNullOrEmpty(x)))
                return BadRequest(resultado.Mensagem);
            else
                return StatusCode(500, MensagemGenerica.MENSAGEM_ERRO_500);
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
