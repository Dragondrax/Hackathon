using Azure.Messaging.ServiceBus;
using MedicalHealth.Fiap.Data.Persistencia.MedicoPersistenciaRepository;
using MedicalHealth.Fiap.Data.Persistencia.UsuarioRepository;
using MedicalHealth.Fiap.Dominio.Entidades;
using MedicalHealth.Fiap.SharedKernel.Utils;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MedicalHealth.Fiap.Function.Pesistencia.Criar.Medico
{
    public class FunctionPersistenciaCriarMedico
    {
        private readonly ILogger<FunctionPersistenciaCriarMedico> _logger;
        private readonly IMedicoPersistenciaRepository _medicoPersistenciaRepository;
        private readonly IUsuarioPersistenciaRepository _usuarioPersistenciaRepository;

        public FunctionPersistenciaCriarMedico(
            ILogger<FunctionPersistenciaCriarMedico> logger,
            IMedicoPersistenciaRepository medicoPersistenciaRepository,
            IUsuarioPersistenciaRepository usuarioPersistenciaRepository)
        {
            _logger = logger;
            _medicoPersistenciaRepository = medicoPersistenciaRepository;
            _usuarioPersistenciaRepository = usuarioPersistenciaRepository;
        }

        [Function(nameof(FunctionPersistenciaCriarMedico))]
        public async Task Run(
            [ServiceBusTrigger("persistencia.medico.criar", Connection = "ServiceBusConnection")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            var json = Tratamentos.TratarBinaryDataAzureFunction(message.Body);

            var medico = JsonConvert.DeserializeObject<Dominio.Entidades.Medico>(json);
            var usuarioMedico = new Usuario(Dominio.Enum.UsuarioRoleEnum.Medico, medico.Id, medico.Email);

            var success = await _medicoPersistenciaRepository.PersistirCriacaoMedico(medico, usuarioMedico);

            if (success)
                await messageActions.CompleteMessageAsync(message);
            else
                await messageActions.DeadLetterMessageAsync(message);
        }
    }
}
