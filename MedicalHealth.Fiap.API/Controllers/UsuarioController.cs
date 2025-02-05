using MedicalHealth.Fiap.Aplicacao;
using MedicalHealth.Fiap.Infraestrutura.DTO;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalHealth.Fiap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
            => _usuarioService = usuarioService;
        

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
    }
}
