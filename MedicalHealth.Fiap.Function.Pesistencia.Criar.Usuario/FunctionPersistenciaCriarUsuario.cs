using Azure.Messaging.ServiceBus;
using MedicalHealth.Fiap.Data.Persistencia.UsuarioRepository;
using MedicalHealth.Fiap.SharedKernel.Utils;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MedicalHealth.Fiap.Function.Pesistencia.Criar.Usuario
{
    public class FunctionPersistenciaCriarUsuario
    {
        private readonly ILogger<FunctionPersistenciaCriarUsuario> _logger;
        private readonly IUsuarioPersistenciaRepository _usuarioPersistenciaRepository;

        public FunctionPersistenciaCriarUsuario(ILogger<FunctionPersistenciaCriarUsuario> logger, IUsuarioPersistenciaRepository usuarioPersistenciaRepository)
        {
            _logger = logger;
            _usuarioPersistenciaRepository = usuarioPersistenciaRepository;
        }

        [Function(nameof(FunctionPersistenciaCriarUsuario))]
        public async Task Run(
            [ServiceBusTrigger("persistencia.usuario.criar", Connection = "ServiceBusConnection")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            var json = Tratamentos.TratarBinaryDataAzureFunction(message.Body);

            var usuario = JsonConvert.DeserializeObject<Dominio.Entidades.Usuario>(json);

            var success = await _usuarioPersistenciaRepository.PersistirCriacaoUsuario(usuario);

            if (success)
                await messageActions.CompleteMessageAsync(message);
            else
                await messageActions.DeadLetterMessageAsync(message);
        }
    }
}
