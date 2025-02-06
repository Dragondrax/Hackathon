using MedicalHealth.Fiap.Aplicacao;
using MedicalHealth.Fiap.Dominio.Enum;
using MedicalHealth.Fiap.Infraestrutura.DTO;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;
using Microsoft.AspNetCore.Mvc;

namespace MedicalHealth.Fiap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController(IMedicoService medicoService) : ControllerBase
    {
        private readonly IMedicoService _medicoService = medicoService;

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

        [HttpPost("AceiteConsultaMedica")]
        public async Task<IActionResult> AceiteConsultaMedica(AceiteConsultaMedicoRequestModel aceiteConsultaMedica)
        {
            var resultado = await _medicoService.AceiteConsultaMedica(aceiteConsultaMedica);

            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Sucesso == false && resultado.Objeto is null && resultado.Mensagem.Any(x => string.IsNullOrEmpty(x)))
                return StatusCode(500, MensagemGenerica.MENSAGEM_ERRO_500);
            else
                return NotFound(resultado);
        }
    }
}
