using MedicalHealth.Fiap.Aplicacao.Paciente;
using MedicalHealth.Fiap.Infraestrutura;
using MedicalHealth.Fiap.Infraestrutura.DTO;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalHealth.Fiap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;
        public PacienteController(IPacienteService pacienteService)
            => _pacienteService = pacienteService;


        [AllowAnonymous]
        [HttpPost("Criar")]
        public async Task<IActionResult> SalvarNovoPaciente([FromBody] CriarAlterarPacienteDTO pacienteDTO)
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

        [Authorize(Roles = "Administrador,Paciente")]
        [HttpGet("BuscarPorEmail")]
        public async Task<IActionResult> BuscarPacientePorEmail([FromQuery] BuscarEmailDTO emailDTO)
        {
            var resultado = await _pacienteService.BuscarPacientePorEmail(emailDTO);

            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Sucesso == false && resultado.Objeto is null && resultado.Mensagem.Any(x => string.IsNullOrEmpty(x)))
                return StatusCode(500, MensagemGenerica.MENSAGEM_ERRO_500);
            else
                return NotFound(resultado);
        }

        [Authorize(Roles = "Administrador,Paciente")]
        [HttpPut("Atualizar")]
        public async Task<IActionResult> AtualizarPaciente([FromBody] CriarAlterarPacienteDTO atualizarPacienteDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await _pacienteService.AtualizarPaciente(atualizarPacienteDTO);

            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Sucesso == false && resultado.Objeto is null && resultado.Mensagem.Any())
                return NotFound(resultado.Mensagem);
            else
                return StatusCode(500, MensagemGenerica.MENSAGEM_ERRO_500);
        }

        [Authorize(Roles = "Administrador,Paciente")]
        [HttpDelete("Remover")]
        public async Task<IActionResult> RemoverPaciente([FromBody] CriarAlterarPacienteDTO removerPacienteDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await _pacienteService.ExcluirPaciente(removerPacienteDTO);

            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Sucesso == true && resultado.Mensagem.Any() && resultado.Mensagem.Count() > 1)
                return Ok(resultado.Mensagem);
            else if (resultado.Sucesso == false && resultado.Objeto is null && resultado.Mensagem.Any())
                return BadRequest(resultado.Mensagem);
            else
                return StatusCode(500, MensagemGenerica.MENSAGEM_ERRO_500);
        }
    }
}
