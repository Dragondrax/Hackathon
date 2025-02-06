using MedicalHealth.Fiap.Aplicacao.Paciente;
using MedicalHealth.Fiap.Infraestrutura.DTO;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalHealth.Fiap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous] //Mudar para Authorize
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;
        public PacienteController(IPacienteService pacienteService)
            => _pacienteService = pacienteService;


        //[Authorize(Roles = "Administrador,Medico,Paciente")]
        [HttpPost("Criar")]
        public async Task<IActionResult> SalvarNovoPaciente([FromBody] CriaAlteraPacienteDTO pacienteDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var resultado = await _pacienteService.SalvarNovoPaciente(pacienteDTO);

            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Sucesso == false && resultado.Objeto is null && resultado.Mensagem.Any())
                return BadRequest(resultado.Mensagem);
            else
                return StatusCode(500, MensagemGenerica.MENSAGEM_ERRO_500);
        }

        //[Authorize(Roles = "Administrador,Medico,Paciente")]
        [HttpGet("BuscarPorEmail")]
        public async Task<IActionResult> BuscarPacientePorEmail([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("O e-mail do paciente é obrigatório.");
            }

            return Ok();
        }
    }
}
