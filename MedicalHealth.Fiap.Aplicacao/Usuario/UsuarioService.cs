using MedicalHealth.Fiap.Data;
using MedicalHealth.Fiap.Dominio.Enum;
using MedicalHealth.Fiap.Infraestrutura.DTO;
using MedicalHealth.Fiap.Infraestrutura.Enum;
using MedicalHealth.Fiap.SharedKernel.Filas;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;
using MedicalHealth.Fiap.SharedKernel.Model;
using MedicalHealth.Fiap.SharedKernel.Utils;
using Newtonsoft.Json;
using static BCrypt.Net.BCrypt;

namespace MedicalHealth.Fiap.Aplicacao.Usuario
{
    public class UsuarioService(IUsuarioRepository usuarioRepository, ITokenService tokenService, IEnviarMensagemServiceBus enviarMensagemServiceBus) : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IEnviarMensagemServiceBus _enviarMensagemServiceBus = enviarMensagemServiceBus;
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

        public async Task<ResponseModel> SalvarNovoUsuario(CriarAlteraUsuarioDTO usuarioDTO)
        {
            var validacao = new CriarAlteraUsuarioDTOValidator().Validate(usuarioDTO);
            if (!validacao.IsValid)
            {
                _mensagem = validacao.Errors.Select(x => x.ErrorMessage).ToList();
                return new ResponseModel(_mensagem, false, null);
            }

            var novoUsuario = new Dominio.Entidades.Usuario((UsuarioRoleEnum)usuarioDTO.Role, null, usuarioDTO.Email, usuarioDTO.Senha);

            await _enviarMensagemServiceBus.EnviarMensagemParaFila(PersistenciaUsuario.FILA_PERSISTENCIA_CRIAR_USUARIO, JsonConvert.SerializeObject(novoUsuario));
            _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
            return new ResponseModel(_mensagem, true, null);

        }

        public async Task<ResponseModel> BuscarUsuarioPorEmail(BuscarEmailDTO emailDTO)
        {
            var validacao = new BuscarEmailDTOValidator().Validate(emailDTO);

            if (!validacao.IsValid)
            {
                _mensagem = validacao.Errors.Select(x => x.ErrorMessage).ToList();
                return new ResponseModel(_mensagem, false, null);
            }

            var usuario = await _usuarioRepository.ObterUsuarioPorEmailAsync(emailDTO.Email);

            if (usuario == null)
            {
                _mensagem.Add(MensagemUsuario.MENSAGEM_USUARIO_NAO_ENCONTRADO);
                return new ResponseModel(_mensagem, false, null);
            }

            _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
            return new ResponseModel(_mensagem, true, usuario);
        }

        public async Task<ResponseModel> AtualizarUsuario(CriarAlteraUsuarioDTO usuarioDTO)
        {
            var validacao = new CriarAlteraUsuarioDTOValidator().Validate(usuarioDTO);
            if (!validacao.IsValid)
            {
                _mensagem = validacao.Errors.Select(x => x.ErrorMessage).ToList();
                return new ResponseModel(_mensagem, false, null);
            }

            var usuarioParaAtualizar = await _usuarioRepository.ObterUsuarioPorEmailAsync(usuarioDTO.Email);

            if (usuarioParaAtualizar != null)
            {
                usuarioParaAtualizar.Atualizar(usuarioDTO.Email, usuarioDTO.Senha);

                await _enviarMensagemServiceBus.EnviarMensagemParaFila(PersistenciaUsuario.FILA_PERSISTENCIA_ATUALIZAR_USUARIO, JsonConvert.SerializeObject(usuarioParaAtualizar));
                _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
                return new ResponseModel(_mensagem, true, null);
            }

            _mensagem.Add(MensagemUsuario.MENSAGEM_USUARIO_NAO_ENCONTRADO);
            return new ResponseModel(_mensagem, false, null);
        }

        public async Task<ResponseModel> ExcluirUsuario(CriarAlteraUsuarioDTO usuarioDTO)
        {
            var validacao = new CriarAlteraUsuarioDTOValidator().Validate(usuarioDTO);
            if (!validacao.IsValid)
            {
                _mensagem = validacao.Errors.Select(x => x.ErrorMessage).ToList();
                return new ResponseModel(_mensagem, false, null);
            }

            var usuarioParaAtualizar = await _usuarioRepository.ObterUsuarioPorEmailAsync(usuarioDTO.Email);

            if (usuarioParaAtualizar != null)
            {
                usuarioParaAtualizar.Excluir();

                await _enviarMensagemServiceBus.EnviarMensagemParaFila(PersistenciaUsuario.FILA_PERSISTENCIA_ATUALIZAR_USUARIO, JsonConvert.SerializeObject(usuarioParaAtualizar));
                _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
                return new ResponseModel(_mensagem, true, null);
            }

            _mensagem.Add(MensagemUsuario.MENSAGEM_USUARIO_NAO_ENCONTRADO);
            return new ResponseModel(_mensagem, false, null);
        }
    }
}