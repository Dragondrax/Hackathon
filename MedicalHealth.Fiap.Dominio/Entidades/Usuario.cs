using MedicalHealth.Fiap.Dominio.Enum;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;
using MedicalHealth.Fiap.SharedKernel.Utils;

namespace MedicalHealth.Fiap.Dominio.Entidades
{
    public class Usuario : EntidadeBase
    {
        public UsuarioRoleEnum Role { get; private set; }
        public Guid GrupoUsuarioId { get; private set; }
        public string Senha {  get; private set; }
        public bool PrimeiroAcesso { get; private set; }
        public bool UsuarioBloqueado {  get; private set; }
        public int TentativasDeLogin { get; private set; }
        private Usuario()
        {
            
        }
        public Usuario(UsuarioRoleEnum role, Guid id, string senha)
        {
            Role = role;
            GrupoUsuarioId = id;
            Senha = GerarSenha.Aleatoria();
            PrimeiroAcesso = true;
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

        public void LoginSucesso()
        {
            TentativasDeLogin = 0;
        }
    }
}
