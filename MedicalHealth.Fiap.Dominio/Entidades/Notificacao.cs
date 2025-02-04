namespace MedicalHealth.Fiap.Dominio.Entidades
{
    public class Notificacao : EntidadeBase
    {
        public Guid UsuarioDestinatarioId { get; private set; }
        public string Mensagem { get; private set; }

        public Notificacao()
        {
            
        }
        public Notificacao(Guid usuarioDestinatarioId, string mensagem)
        {
            UsuarioDestinatarioId = usuarioDestinatarioId;
            Mensagem = mensagem;
        }

        public void Exluir()
        {
            Desativar();
        }
    }
}
