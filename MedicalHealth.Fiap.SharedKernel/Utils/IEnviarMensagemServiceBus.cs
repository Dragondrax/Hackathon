namespace MedicalHealth.Fiap.SharedKernel.Utils
{
    public interface IEnviarMensagemServiceBus
    {
        Task EnviarMensagemParaFila(string fila, object mensagem);
    }
}
