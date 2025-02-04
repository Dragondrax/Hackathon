using MedicalHealth.Fiap.SharedKernel.Filas;
using MedicalHealth.Fiap.SharedKernel.Utils;
using Microsoft.AspNetCore.Mvc;

namespace MedicalHealth.Fiap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IEnviarMensagemServiceBus _enviarMensagemServiceBus;
        public PacienteController(IEnviarMensagemServiceBus enviarMensagemServiceBus)
        {
            _enviarMensagemServiceBus = enviarMensagemServiceBus;
        }
        [HttpPost("Criar")]
        public async Task<IActionResult> SalvarNovoPaciente([FromQuery] string novoPaciente)
        {
            await _enviarMensagemServiceBus.EnviarMensagemParaFila(PersistenciaUsuario.FILA_PERSISTENCIA_CRIAR_USUARIO, novoPaciente);

            if (novoPaciente == null)
            {
                return BadRequest("Os dados do paciente não foram fornecidos.");
            }

            return Ok();
        }

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
