using Azure.Messaging.ServiceBus;
using MedicalHealth.Fiap.Data.Persistencia.PacientePersistenciaRepository;
using MedicalHealth.Fiap.Data.Persistencia.UsuarioRepository;
using MedicalHealth.Fiap.Dominio.Entidades;
using MedicalHealth.Fiap.SharedKernel.Utils;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MedicalHealth.Fiap.Function.Pesistencia.Criar.Paciente
{
    public class FunctionPersistenciaCriarPaciente
    {
        private readonly ILogger<FunctionPersistenciaCriarPaciente> _logger;
        private readonly IPacientePersistenciaRepository _pacientePersistenciaRepository;
        private readonly IUsuarioPersistenciaRepository _usuarioPersistenciaRepository;

        public FunctionPersistenciaCriarPaciente(
            ILogger<FunctionPersistenciaCriarPaciente> logger, 
            IPacientePersistenciaRepository pacientePersistenciaRepository, 
            IUsuarioPersistenciaRepository usuarioPersistenciaRepository)
        {
            _logger = logger;
            _pacientePersistenciaRepository = pacientePersistenciaRepository;
            _usuarioPersistenciaRepository = usuarioPersistenciaRepository;
        }

        [Function(nameof(FunctionPersistenciaCriarPaciente))]
        public async Task Run(
            [ServiceBusTrigger("persistencia.paciente.criar", Connection = "ServiceBusConnection")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            var json = Tratamentos.TratarBinaryDataAzureFunction(message.Body);

            var paciente = JsonConvert.DeserializeObject<Dominio.Entidades.Paciente>(json);
            var usuarioPaciente = new Usuario(Dominio.Enum.UsuarioRoleEnum.Paciente, paciente.Id, paciente.Email);

            var success = await _pacientePersistenciaRepository.PersistirCriacaoPaciente(paciente);

            if (success)
            {
                var usuarioSucess = await _usuarioPersistenciaRepository.PersistirCriacaoUsuario(usuarioPaciente);

                if (usuarioSucess)
                    await messageActions.CompleteMessageAsync(message);
                else
                    await messageActions.DeadLetterMessageAsync(message);
            }
            else
                await messageActions.DeadLetterMessageAsync(message);
        }
    }
}
