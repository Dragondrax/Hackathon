using MedicalHealth.Fiap.Aplicacao;
using MedicalHealth.Fiap.Infraestrutura;
using MedicalHealth.Fiap.Infraestrutura.DTO;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;
using MedicalHealth.Fiap.SharedKernel.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalHealth.Fiap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IEnviarMensagemServiceBus _enviarMensagemServiceBus;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }        

        [HttpPost("Autenticar")]
        [AllowAnonymous]
        public async Task<IActionResult> Autenticar(AutenticarUsuarioDTO usuario)
        {
            var resultado = await _usuarioService.AutenticarUsuario(usuario);

            if (resultado.Sucesso)
                return Ok(resultado);
            else if (!resultado.Sucesso && resultado.Objeto is null && resultado.Mensagem.Any(x => String.IsNullOrEmpty(x)))
                return StatusCode(500, MensagemGenerica.MENSAGEM_ERRO_500);
            else
                return BadRequest(resultado);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost("Criar")]
        public async Task<IActionResult> SalvarNovoUsuario([FromBody] CriarAlteraUsuarioDTO usuarioDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await _usuarioService.SalvarNovoUsuario(usuarioDTO);

            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Sucesso == false && resultado.Objeto is null && resultado.Mensagem.Any())
                return BadRequest(resultado.Mensagem);
            else
                return StatusCode(500, MensagemGenerica.MENSAGEM_ERRO_500);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("BuscarPorEmail")]
        public async Task<IActionResult> BuscarUsuarioPorEmail([FromQuery] BuscarEmailDTO emailDTO)
        {
            var resultado = await _usuarioService.BuscarUsuarioPorEmail(emailDTO);

            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Sucesso == false && resultado.Objeto is null && resultado.Mensagem.Any(x => string.IsNullOrEmpty(x)))
                return StatusCode(500, MensagemGenerica.MENSAGEM_ERRO_500);
            else
                return NotFound(resultado);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("Atualizar")]
        public async Task<IActionResult> AtualizarUsuario([FromBody] CriarAlteraUsuarioDTO atualizarUsuarioDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await _usuarioService.AtualizarUsuario(atualizarUsuarioDTO);

            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Sucesso == false && resultado.Objeto is null && resultado.Mensagem.Any())
                return BadRequest(resultado.Mensagem);
            else
                return StatusCode(500, MensagemGenerica.MENSAGEM_ERRO_500);
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("Remover")]
        public async Task<IActionResult> RemoverUsuario(Guid usuarioId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await _usuarioService.ExcluirUsuario(usuarioId);

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
