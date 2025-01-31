namespace MedicalHealth.Fiap.SharedKernel.Model
{
    public class ResponseModel
    {
        public List<string> Mensagem { get; set; }
        public bool Sucesso { get; set; }
        public object Objeto { get; set; }

        public ResponseModel(List<string> mensagem, bool sucesso, object objeto)
        {
            Mensagem = mensagem;
            Sucesso = sucesso;
            Objeto = objeto;
        }
    }
}
