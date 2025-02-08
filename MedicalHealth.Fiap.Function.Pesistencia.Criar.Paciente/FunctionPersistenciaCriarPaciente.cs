using Azure.Messaging.ServiceBus;
using MedicalHealth.Fiap.Data.Persistencia.PacientePersistenciaRepository;
using MedicalHealth.Fiap.Data.Persistencia.UsuarioRepository;
using MedicalHealth.Fiap.Dominio.Entidades;
using MedicalHealth.Fiap.SharedKernel.Utils;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MedicalHealth.Fiap.Infraestrutura.DTO;

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

            var paciente = JsonConvert.DeserializeObject<PersistenciaPacienteDTO>(json);
            var novoPaciente = new Dominio.Entidades.Paciente(paciente.Nome, paciente.CPF, paciente.Email);
            var usuarioPaciente = new Usuario(Dominio.Enum.UsuarioRoleEnum.Paciente, novoPaciente.Id, novoPaciente.Email, paciente.Senha);

            var success = await _pacientePersistenciaRepository.PersistirCriacaoPaciente(novoPaciente, usuarioPaciente);

            if (success)
                await messageActions.CompleteMessageAsync(message);
            else
                await messageActions.DeadLetterMessageAsync(message);

        }
    }
}
