using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using MedicalHealth.Fiap.SharedKernel.Utils;
using Newtonsoft.Json;
using MedicalHealth.Fiap.Data.Persistencia.AgendaMedicoPersistenciaRepository;

namespace MedicalHealth.Fiap.Function.Persistencia.Criar.Consulta
{
    public class FunctionPersistenciaCriarConsulta
    {
        private readonly ILogger<FunctionPersistenciaCriarConsulta> _logger;
        private readonly IAgendaMedicoPersistenciaRepository _agendaMedicoPersistenciaRepository;

        public FunctionPersistenciaCriarConsulta(ILogger<FunctionPersistenciaCriarConsulta> logger, IAgendaMedicoPersistenciaRepository agendaMedicoPersistenciaRepository)
        {
            _logger = logger;
            _agendaMedicoPersistenciaRepository = agendaMedicoPersistenciaRepository;

        }

        [Function(nameof(FunctionPersistenciaCriarConsulta))]
        public async Task Run(
            [ServiceBusTrigger("persistencia.consulta.criar", Connection = "ServiceBusConnection")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            var json = Tratamentos.TratarBinaryDataAzureFunction(message.Body);

            var consulta = JsonConvert.DeserializeObject<Dominio.Entidades.Consulta>(json);

            var success = await _agendaMedicoPersistenciaRepository.PersistirCriacaoConsulta(consulta);

            if (success)
                await messageActions.CompleteMessageAsync(message);
            else
                await messageActions.DeadLetterMessageAsync(message);
        }
    }
}
