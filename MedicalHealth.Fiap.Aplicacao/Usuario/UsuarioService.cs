using MedicalHealth.Fiap.Dominio;
using MedicalHealth.Fiap.Dominio.Entidades;
using MedicalHealth.Fiap.Infraestrutura.DTO;
using MedicalHealth.Fiap.Infraestrutura.Enum;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;
using MedicalHealth.Fiap.SharedKernel.Model;
using static BCrypt.Net.BCrypt;

namespace MedicalHealth.Fiap.Aplicacao.Usuario
{
    public class UsuarioService(IUsuarioRepository usuarioRepository, ITokenService tokenService) : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
        private readonly ITokenService _tokenService = tokenService;
        private List<string> _mensagem = new List<string>();

        public async Task<ResponseModel> AutenticarUsuario(AutenticarUsuarioDTO usuarioDTO)
        {
            var validacao = new AutenticarUsuarioDTOValidator().Validate(usuarioDTO);
            if (!validacao.IsValid)
            {
                _mensagem = validacao.Errors.Select(x => x.ErrorMessage).ToList();
                return new ResponseModel(_mensagem, false, null);
            }

            if (usuarioDTO.TipoUsuario == Roles.Administrador)
            {
                var usuario = await _usuarioRepository.ObterUsuarioPorEmailAsync(usuarioDTO.Email.ToLower());

                if (usuario != null && Verify(usuarioDTO.Senha, usuario.Senha))
                {
                    var token = await _tokenService.ObterToken(usuarioDTO);

                    _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
                    return new ResponseModel(_mensagem, true, token);
                }
            }
            else if (usuarioDTO.TipoUsuario == Roles.Medico)
            {
                var medico = await _usuarioRepository.ObterMedicoPorCRMAsync(usuarioDTO.CRM);

                if (medico != null)
                {
                    var usuario = await _usuarioRepository.ObterUsuarioPorEmailAsync(medico.Email);

                    if (medico is not null && Verify(usuarioDTO.Senha, usuario.Senha))
                    {
                        var token = await _tokenService.ObterToken(usuarioDTO);

                        _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
                        return new ResponseModel(_mensagem, true, token);
                    }
                }
            }
            else if (usuarioDTO.TipoUsuario == Roles.Paciente)
            {
                var usuario = await _usuarioRepository.ObterUsuarioPorEmailAsync(usuarioDTO.Email.ToLower());

                if (usuario != null && Verify(usuarioDTO.Senha, usuario.Senha))
                    {
                        var token = await _tokenService.ObterToken(usuarioDTO);

                        _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
                        return new ResponseModel(_mensagem, true, token);
                }
            }
            else
            {
                _mensagem.Add(MensagemUsuario.MENSAGEM_USUARIO_LOGIN_SENHA_INCORRETA);
                return new ResponseModel(_mensagem, true, null);
            }

            _mensagem.Add(MensagemUsuario.MENSAGEM_USUARIO_LOGIN_SENHA_INCORRETA);
            return new ResponseModel(_mensagem, true, null);
        }

        public Task<string> GerarHashSenhaUsuario(string senha)
        {
            throw new NotImplementedException();
        }
    }
}
