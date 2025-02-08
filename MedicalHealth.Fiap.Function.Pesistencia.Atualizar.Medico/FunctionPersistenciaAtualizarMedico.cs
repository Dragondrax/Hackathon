using Azure.Messaging.ServiceBus;
using MedicalHealth.Fiap.Data.Persistencia.MedicoPersistenciaRepository;
using MedicalHealth.Fiap.SharedKernel.Utils;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MedicalHealth.Fiap.Function.Pesistencia.Atualizar.Medico
{
    public class FunctionPersistenciaAtualizarMedico
    {
        private readonly ILogger<FunctionPersistenciaAtualizarMedico> _logger;

        private readonly IMedicoPersistenciaRepository _medicoPersistenciaRepository;

        public FunctionPersistenciaAtualizarMedico(ILogger<FunctionPersistenciaAtualizarMedico> logger, IMedicoPersistenciaRepository medicoPersistenciaRepository)
        {
            _logger = logger;
            _medicoPersistenciaRepository = medicoPersistenciaRepository;
        }

        [Function(nameof(FunctionPersistenciaAtualizarMedico))]
        public async Task Run(
            [ServiceBusTrigger("persistencia.medico.atualizar", Connection = "ServiceBusConnection")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            var json = Tratamentos.TratarBinaryDataAzureFunction(message.Body);

            var medico = JsonConvert.DeserializeObject<Dominio.Entidades.Medico>(json);

            var success = await _medicoPersistenciaRepository.PersistirAtualizacaoMedico(medico);

            if (success)
                await messageActions.CompleteMessageAsync(message);
            else
                await messageActions.DeadLetterMessageAsync(message);
        }
    }
}
