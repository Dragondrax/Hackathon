using MedicalHealth.Fiap.Aplicacao;
using MedicalHealth.Fiap.Dominio.Enum;
using MedicalHealth.Fiap.Infraestrutura.DTO;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalHealth.Fiap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous] //Mudar para Authorize
    public class MedicoController(IMedicoService medicoService) : ControllerBase
    {
        private readonly IMedicoService _medicoService = medicoService;

        //[Authorize(Roles = "Administrador,Medico")]
        [HttpPost("Criar")]
        public async Task<IActionResult> SalvarNovoMedico(CriarAlteraMedicoDTO medicoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await _medicoService.SalvarNovoMedico(medicoDTO);

            if (resultado.Sucesso)
                return Ok();
            else if (resultado.Sucesso == false && resultado.Objeto is null && resultado.Mensagem.Any())
                return BadRequest(resultado.Mensagem);
            else
                return StatusCode(500, MensagemGenerica.MENSAGEM_ERRO_500);
        }

        //[Authorize(Roles = "Administrador,Medico,Paciente")]
        [HttpGet("BuscarPorCRM")]
        public async Task<IActionResult> BuscarMedicoPorCRM([FromQuery] BuscarCRMDTO crmDTO)
        {
            var resultado = await _medicoService.BuscarMedicoPorCRM(crmDTO);

            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Sucesso == false && resultado.Objeto is null && resultado.Mensagem.Any(x => string.IsNullOrEmpty(x)))
                return StatusCode(500, MensagemGenerica.MENSAGEM_ERRO_500);
            else
                return NotFound(resultado);
        }

        //[Authorize(Roles = "Administrador,Medico,Paciente")]
        [HttpGet("BuscarPorEspecialidade")]
        public async Task<IActionResult> BuscarMedicoPorEspecialidade([FromQuery] EspecialidadeMedica especialidade)
        {
            var resultado = await _medicoService.BuscarMedicosPorEspecialidade(especialidade);

            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Sucesso == false && resultado.Objeto is null && resultado.Mensagem.Any(x => string.IsNullOrEmpty(x)))
                return StatusCode(500, MensagemGenerica.MENSAGEM_ERRO_500);
            else
                return NotFound(resultado);

        }
    }
}
