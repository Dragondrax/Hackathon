using Azure.Messaging.ServiceBus;
using MedicalHealth.Fiap.Data.Persistencia.AgendaMedicoPersistenciaRepository;
using MedicalHealth.Fiap.Data.Persistencia.ConsultaPersistenciaRepository;
using MedicalHealth.Fiap.SharedKernel.Utils;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MedicalHealth.Fiap.Function.Persistencia.Criar.Consulta
{
    public class FunctionPersistenciaCriarConsulta
    {
        private readonly ILogger<FunctionPersistenciaCriarConsulta> _logger;
        private readonly IConsultaPersistenciaRepository _consultaPersistenciaRepository;

        public FunctionPersistenciaCriarConsulta(ILogger<FunctionPersistenciaCriarConsulta> logger,
                                                 IConsultaPersistenciaRepository consultaPersistenciaRepositor)
        {
            _logger = logger;
            _consultaPersistenciaRepository = consultaPersistenciaRepositor;
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

            var success = await _consultaPersistenciaRepository.PersistirCriacaoConsulta(consulta);

            if (success)
                await messageActions.CompleteMessageAsync(message);
            else
                await messageActions.DeadLetterMessageAsync(message);
        }
    }
}
