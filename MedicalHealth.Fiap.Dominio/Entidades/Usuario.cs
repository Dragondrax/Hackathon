using MedicalHealth.Fiap.Dominio.Enum;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;
using MedicalHealth.Fiap.SharedKernel.Utils;
using Newtonsoft.Json;

namespace MedicalHealth.Fiap.Dominio.Entidades
{
    public class Usuario : EntidadeBase
    {
        [JsonProperty("Role")]
        public UsuarioRoleEnum Role { get; private set; }
        [JsonProperty("GrupoUsuarioId")]
        public Guid? GrupoUsuarioId { get; private set; }
        [JsonProperty("Email")]
        public string Email {  get; private set; }
        [JsonProperty("Senha")]
        public string Senha {  get; private set; }
        [JsonProperty("PrimeiroAcesso")]
        public bool PrimeiroAcesso { get; private set; }
        [JsonProperty("UsuarioBloqueado")]
        public bool UsuarioBloqueado {  get; private set; }
        [JsonProperty("TentativasDeLogin")]
        public int TentativasDeLogin { get; private set; }
        public Usuario()
        {
            
        }
        public Usuario(UsuarioRoleEnum role, Guid? id, string email)
        {
            Role = role;
            GrupoUsuarioId = id;
            Email = email;
            Senha = GerarSenha.Aleatoria();
            PrimeiroAcesso = true;
        }

        public void Atualizar(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }

        public void AtualizarSenhaPrimeiroAcesso(string senha)
        {
            Senha = senha;
            PrimeiroAcesso = false;
        }

        public void AtualizarTentativaLogin() 
        {
            TentativasDeLogin = ++TentativasDeLogin;

            if (TentativasDeLogin > 2)
                UsuarioBloqueado = true;
        }

        public string AtualizarSenha(string senha)
        {
            if (!UsuarioBloqueado)
            {
                TentativasDeLogin = 0;
                Senha = senha;
                return MensagemGenerica.MENSAGEM_SUCESSO;
            }
                

            return MensagemUsuario.USUARIO_BLOQUEADO;
        }

        public void Excluir()
        {
            Desativar();
        }

        public void LoginSucesso()
        {
            TentativasDeLogin = 0;
        }
    }
}
