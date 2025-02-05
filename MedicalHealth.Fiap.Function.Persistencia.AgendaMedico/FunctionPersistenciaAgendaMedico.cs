using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using MedicalHealth.Fiap.Data.Persistencia.AgendaMedicoPersistenciaRepository;
using MedicalHealth.Fiap.Dominio.Entidades;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MedicalHealth.Fiap.Function.Persistencia.AgendaMedico
{
    public class FunctionPersistenciaAgendaMedico
    {
        private readonly ILogger<FunctionPersistenciaAgendaMedico> _logger;
        private readonly IAgendaMedicoPersistenciaRepository _agendaMedicoPersistenciaRepository;

        public FunctionPersistenciaAgendaMedico(ILogger<FunctionPersistenciaAgendaMedico> logger, IAgendaMedicoPersistenciaRepository agendaMedicoPersistenciaRepository)
        {
            _logger = logger;
            _agendaMedicoPersistenciaRepository = agendaMedicoPersistenciaRepository;
        }

        [Function(nameof(FunctionPersistenciaAgendaMedico))]
        public async Task Run(
            [ServiceBusTrigger("persistencia.agenda_medico.criar", Connection = "ServiceBusConnection")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            string jsonRaw = Encoding.UTF8.GetString(message.Body.ToArray());
            _logger.LogInformation("Conteúdo bruto do JSON: {jsonRaw}", jsonRaw);

            List<Dominio.Entidades.AgendaMedico> agendaMedico = null;

            if (jsonRaw.StartsWith("\"") && jsonRaw.EndsWith("\""))
            {
                var innerJson = JsonConvert.DeserializeObject<string>(jsonRaw);
                _logger.LogInformation("JSON interno: {innerJson}", innerJson);
                agendaMedico = JsonConvert.DeserializeObject<List<Dominio.Entidades.AgendaMedico>>(innerJson);
            }
            else
            {
                agendaMedico = JsonConvert.DeserializeObject<List<Dominio.Entidades.AgendaMedico>>(jsonRaw);
            }

            var success = await _agendaMedicoPersistenciaRepository.PersistirDadosAgendaMedico(agendaMedico);

            if(success)
                await messageActions.CompleteMessageAsync(message);
            else
                await messageActions.DeadLetterMessageAsync(message);
        }
    }
}
