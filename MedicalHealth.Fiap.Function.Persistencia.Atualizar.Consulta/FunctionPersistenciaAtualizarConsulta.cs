using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using MedicalHealth.Fiap.Data.Persistencia.AgendaMedicoPersistenciaRepository;
using MedicalHealth.Fiap.SharedKernel.Utils;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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
            var json = Tratamentos.TratarBinaryDataAzureFunction(message.Body);

            var agendaMedico = JsonConvert.DeserializeObject<List<Dominio.Entidades.AgendaMedico>>(json);

            var success = await _agendaMedicoPersistenciaRepository.PersistirAtualizacaoAgendaMedico(agendaMedico);

            if (success)
                await messageActions.CompleteMessageAsync(message);
            else
                await messageActions.DeadLetterMessageAsync(message);
        }
    }
}
