using Azure.Messaging.ServiceBus;
using MedicalHealth.Fiap.Data.Persistencia.AgendaMedicoPersistenciaRepository;
using MedicalHealth.Fiap.SharedKernel.Utils;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MedicalHealth.Fiap.Function.Persistencia.AgendaMedico
{
    public class FunctionPersistenciaCriarAgendaMedico
    {
        private readonly ILogger<FunctionPersistenciaCriarAgendaMedico> _logger;
        private readonly IAgendaMedicoPersistenciaRepository _agendaMedicoPersistenciaRepository;

        public FunctionPersistenciaCriarAgendaMedico(ILogger<FunctionPersistenciaCriarAgendaMedico> logger, IAgendaMedicoPersistenciaRepository agendaMedicoPersistenciaRepository)
        {
            _logger = logger;
            _agendaMedicoPersistenciaRepository = agendaMedicoPersistenciaRepository;
        }

        [Function(nameof(FunctionPersistenciaCriarAgendaMedico))]
        public async Task Run(
            [ServiceBusTrigger("persistencia.agenda_medico.criar", Connection = "ServiceBusConnection")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            var json = Tratamentos.TratarBinaryDataAzureFunction(message.Body);

            var agendaMedico = JsonConvert.DeserializeObject<List<Dominio.Entidades.AgendaMedico>>(json);

            var success = await _agendaMedicoPersistenciaRepository.PersistirCriacaoAgendaMedico(agendaMedico);

            if(success)
                await messageActions.CompleteMessageAsync(message);
            else
                await messageActions.DeadLetterMessageAsync(message);
        }
    }
}
