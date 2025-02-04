using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace MedicalHealth.Fiap.Function.Persistencia.Usuario
{
    public class FunctionPersistenciaUsuarios
    {
        private readonly ILogger<FunctionPersistenciaUsuarios> _logger;

        public FunctionPersistenciaUsuarios(ILogger<FunctionPersistenciaUsuarios> logger)
        {
            _logger = logger;
        }

        [Function(nameof(FunctionPersistenciaUsuarios))]
        public async Task Run(
            [ServiceBusTrigger("persistencia.usuarios", Connection = "ServiceBusConnection")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            // Complete the message
            await messageActions.CompleteMessageAsync(message);
        }
    }
}
