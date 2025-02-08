using Azure.Messaging.ServiceBus;
using MedicalHealth.Fiap.Data.Persistencia.UsuarioRepository;
using MedicalHealth.Fiap.SharedKernel.Utils;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MedicalHealth.Fiap.Function.Pesistencia.Atualizar.Usuario
{
    public class FunctionPersistenciaAtualizarUsuario
    {
        private readonly ILogger<FunctionPersistenciaAtualizarUsuario> _logger;

        private readonly IUsuarioPersistenciaRepository _usuarioPersistenciaRepository;

        public FunctionPersistenciaAtualizarUsuario(ILogger<FunctionPersistenciaAtualizarUsuario> logger, IUsuarioPersistenciaRepository usuarioPersistenciaRepository)
        {
            _logger = logger;
            _usuarioPersistenciaRepository = usuarioPersistenciaRepository;
        }

        [Function(nameof(FunctionPersistenciaAtualizarUsuario))]
        public async Task Run(
            [ServiceBusTrigger("persistencia.usuarios.atualizar", Connection = "ServiceBusConnection")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            var json = Tratamentos.TratarBinaryDataAzureFunction(message.Body);

            var usuario = JsonConvert.DeserializeObject<Dominio.Entidades.Usuario>(json);

            var success = await _usuarioPersistenciaRepository.PersistirAtualizacaoUsuario(usuario);

            if (success)
                await messageActions.CompleteMessageAsync(message);
            else
                await messageActions.DeadLetterMessageAsync(message);
        }
    }
}
