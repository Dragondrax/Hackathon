using Newtonsoft.Json;

namespace MedicalHealth.Fiap.Dominio.Entidades
{
    public class Notificacao : EntidadeBase
    {
        [JsonProperty("UsuarioDestinatarioId")]
        public Guid UsuarioDestinatarioId { get; private set; }
        [JsonProperty("Mensagem")]
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
