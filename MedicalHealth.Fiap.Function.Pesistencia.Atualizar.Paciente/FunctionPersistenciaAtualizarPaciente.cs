using Azure.Messaging.ServiceBus;
using MedicalHealth.Fiap.Data.Persistencia.PacientePersistenciaRepository;
using MedicalHealth.Fiap.SharedKernel.Utils;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MedicalHealth.Fiap.Function.Pesistencia.Atualizar.Paciente
{
    public class FunctionPersistenciaAtualizarPaciente
    {
        private readonly ILogger<FunctionPersistenciaAtualizarPaciente> _logger;

        private readonly IPacientePersistenciaRepository _pacientePersistenciaRepository;

        public FunctionPersistenciaAtualizarPaciente(ILogger<FunctionPersistenciaAtualizarPaciente> logger, IPacientePersistenciaRepository pacientePersistenciaRepository)
        {
            _logger = logger;
            _pacientePersistenciaRepository = pacientePersistenciaRepository;
        }

        [Function(nameof(FunctionPersistenciaAtualizarPaciente))]
        public async Task Run(
            [ServiceBusTrigger("persistencia.paciente.atualizar", Connection = "ServiceBusConnection")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            var json = Tratamentos.TratarBinaryDataAzureFunction(message.Body);

            var paciente = JsonConvert.DeserializeObject<Dominio.Entidades.Paciente>(json);

            var success = await _pacientePersistenciaRepository.PersistirAtualizacaoPaciente(paciente);

            if (success)
                await messageActions.CompleteMessageAsync(message);
            else
                await messageActions.DeadLetterMessageAsync(message);
        }
    }
}
