using MedicalHealth.Fiap.Aplicacao.Consulta;
using MedicalHealth.Fiap.Infraestrutura.DTO;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalHealth.Fiap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaService _consultaService;
        public ConsultaController(IConsultaService consultaService)
        {
            _consultaService = consultaService;
        }
        [Authorize(Roles = "Administrador,Paciente")]
        [HttpPost("CriarConsulta")]
        public async Task<IActionResult> CriarConsulta([FromBody] ConsultaSalvarDTO consultaDTO, [FromQuery] Guid pacienteId)
        {
            var resultado = await _consultaService.SalvarConsulta(consultaDTO, pacienteId);

            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Sucesso == true && resultado.Mensagem.Any() && resultado.Mensagem.Count() > 1)
                return Ok(resultado.Mensagem);
            else if (resultado.Sucesso == false && resultado.Objeto is null && resultado.Mensagem.Any())
                return BadRequest(resultado.Mensagem);
            else
                return StatusCode(500, MensagemGenerica.MENSAGEM_ERRO_500);
        }

        [Authorize(Roles = "Administrador,Paciente")]
        [HttpPut("AtualizarJustificativaConsulta")]
        public async Task<IActionResult> AtualizarJustificativaConsulta(ConsultaAtualizarDTO consultaAtualizarDTO)
        {
            var resultado = await _consultaService.AtualizarJustificativaConsulta(consultaAtualizarDTO);

            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Sucesso == true && resultado.Mensagem.Any() && resultado.Mensagem.Count() > 1)
                return Ok(resultado.Mensagem);
            else if (resultado.Sucesso == false && resultado.Objeto is null && resultado.Mensagem.Any())
                return BadRequest(resultado.Mensagem);
            else
                return StatusCode(500, MensagemGenerica.MENSAGEM_ERRO_500);
        }

        [HttpGet("ObterConsultasPorMedico")]
        [Authorize(Roles = "Administrador,Medico")]
        public async Task<IActionResult> ObterConsultasPorMedico(Guid medicoId)
        {
            var resultado = await _consultaService.ObterConsultasPorMedico(medicoId);

            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Sucesso == false && resultado.Objeto is null && resultado.Mensagem.Any())
                return BadRequest(resultado.Mensagem);
            else
                return StatusCode(500, MensagemGenerica.MENSAGEM_ERRO_500);
        }

        [HttpGet("ObterConsultasPorPaciente")]
        [Authorize(Roles = "Administrador,Paciente")]
        public async Task<IActionResult> ObterConsultasPorPaciente(Guid pacienteId)
        {
            var resultado = await _consultaService.ObterConsultasPorPaciente(pacienteId);

            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Sucesso == false && resultado.Objeto is null && resultado.Mensagem.Any())
                return BadRequest(resultado.Mensagem);
            else
                return StatusCode(500, MensagemGenerica.MENSAGEM_ERRO_500);
        }

        [HttpPost("AceiteConsultaMedica")]
        [Authorize(Roles = "Administrador,Medico")]
        public async Task<IActionResult> AceiteConsultaMedica(AceiteConsultaMedicoRequestModel aceiteConsultaMedica)
        {
            var resultado = await _consultaService.AceiteConsultaMedica(aceiteConsultaMedica);

            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Sucesso == false && resultado.Objeto is null && resultado.Mensagem.Any(x => string.IsNullOrEmpty(x)))
                return StatusCode(500, MensagemGenerica.MENSAGEM_ERRO_500);
            else
                return NotFound(resultado);
        }
    }
}
