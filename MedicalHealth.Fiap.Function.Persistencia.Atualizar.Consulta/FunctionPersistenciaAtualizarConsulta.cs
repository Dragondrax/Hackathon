using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using MedicalHealth.Fiap.SharedKernel.Utils;
using Newtonsoft.Json;
using MedicalHealth.Fiap.Data.Persistencia.AgendaMedicoPersistenciaRepository;
using MedicalHealth.Fiap.Infraestrutura.DTO;

namespace MedicalHealth.Fiap.Function.Persistencia.Atualizar.Consulta
{
    public class FunctionPersistenciaAtualizarConsulta
    {
        private readonly ILogger<FunctionPersistenciaAtualizarConsulta> _logger;
        private readonly IAgendaMedicoPersistenciaRepository _agendaMedicoPersistenciaRepository;

        public FunctionPersistenciaAtualizarConsulta(ILogger<FunctionPersistenciaAtualizarConsulta> logger, IAgendaMedicoPersistenciaRepository agendaMedicoPersistenciaRepository)
        {
            _logger = logger;
            _agendaMedicoPersistenciaRepository = agendaMedicoPersistenciaRepository;
        }

        [Function(nameof(FunctionPersistenciaAtualizarConsulta))]
        public async Task Run(
            [ServiceBusTrigger("persistencia.consulta.atualizar", Connection = "ServiceBusConnection")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            var json = Tratamentos.TratarBinaryDataAzureFunction(message.Body);

            var consulta = JsonConvert.DeserializeObject<ConsultaAtualizarDTO>(json);

            var success = await _agendaMedicoPersistenciaRepository.PersistirAtualizacaoConsulta(consulta);

            if (success)
                await messageActions.CompleteMessageAsync(message);
            else
                await messageActions.DeadLetterMessageAsync(message);
        }
    }
}
