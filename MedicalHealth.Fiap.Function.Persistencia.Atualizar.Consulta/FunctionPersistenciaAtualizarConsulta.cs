using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using MedicalHealth.Fiap.Data.Persistencia.ConsultaPersistenciaRepository;
using MedicalHealth.Fiap.SharedKernel.Utils;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MedicalHealth.Fiap.Function.Persistencia.Atualizar.Consulta
{
    public class FunctionPersistenciaAtualizarConsulta
    {
        private readonly ILogger<FunctionPersistenciaAtualizarConsulta> _logger;
        private readonly IConsultaPersistenciaRepository _consultaPersistenciaRepository;

        public FunctionPersistenciaAtualizarConsulta(ILogger<FunctionPersistenciaAtualizarConsulta> logger,
                                                 IConsultaPersistenciaRepository consultaPersistenciaRepositor)
        {
            _logger = logger;
            _consultaPersistenciaRepository = consultaPersistenciaRepositor;
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

            var consulta = JsonConvert.DeserializeObject<Dominio.Entidades.Consulta>(json);

            var success = await _consultaPersistenciaRepository.PersistirAtualizacaoConsulta(consulta);

            if (success)
                await messageActions.CompleteMessageAsync(message);
            else
                await messageActions.DeadLetterMessageAsync(message);
        }
    }
}
