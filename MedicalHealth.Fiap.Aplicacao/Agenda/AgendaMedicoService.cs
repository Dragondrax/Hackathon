using MedicalHealth.Fiap.Aplicacao.DTO;
using MedicalHealth.Fiap.Data.Repository;
using MedicalHealth.Fiap.Dominio.Entidades;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;
using MedicalHealth.Fiap.SharedKernel.Model;
using MedicalHealth.Fiap.SharedKernel.Utils;
using Newtonsoft.Json;

namespace MedicalHealth.Fiap.Aplicacao.Agenda
{
    public class AgendaMedicoService : IAgendaMedicoService
    {
        private List<string> _mensagem = [];
        private readonly IAgendaMedicoRepository _repository;
        private readonly IEnviarMensagemServiceBus _enviarMensagemServiceBus;
        public AgendaMedicoService(IAgendaMedicoRepository repository, IEnviarMensagemServiceBus enviarMensagemServiceBus)
        {
            _repository = repository;
            _enviarMensagemServiceBus = enviarMensagemServiceBus;
        }
        public async Task<ResponseModel> SalvarNovaAgendaParaOMedico(NovaAgendaMedicoRequestModel novaAgendaMedico)
        {
            var validacao = new NovaAgendaMedicoRequestModelValidator().Validate(novaAgendaMedico);
            if (!validacao.IsValid)
            {
                _mensagem = validacao.Errors.Select(x => x.ErrorMessage).ToList();
                return new ResponseModel(_mensagem, false, null);
            }

            var listaHorariosParaCriarAgendaMedico = new List<AgendaMedico>();

            foreach (var data in novaAgendaMedico.Data)
            {
                var agendasMedicoExistentes = await _repository.ObterAgendaMedicoPorIdMedicoEData(data,
                                                                                            novaAgendaMedico.HorarioInicio,
                                                                                            novaAgendaMedico.HorarioFim,
                                                                                            novaAgendaMedico.MedicoId);

                if (agendasMedicoExistentes is not null && agendasMedicoExistentes.Any())
                {
                    foreach (var agendaMedicoExistente in agendasMedicoExistentes)
                    {
                        _mensagem.Add($"Dia: {agendaMedicoExistente.Data} e Horario: {agendaMedicoExistente.HorarioInicio} - {agendaMedicoExistente.HorarioFim} já existente na sua agenda");
                    }

                    return new ResponseModel(_mensagem, false, null);
                }

                var horariosCriados = GerarHorariosBaseadoNoHorarioInicialEFinal(novaAgendaMedico.HorarioInicio, novaAgendaMedico.HorarioFim, novaAgendaMedico.TempoConsulta, novaAgendaMedico.Intervalo);

                foreach(var horarioCriado in horariosCriados)
                {
                    listaHorariosParaCriarAgendaMedico.Add(new AgendaMedico(data, horarioCriado.Item1, horarioCriado.Item2, novaAgendaMedico.MedicoId));
                }
            }

            await _enviarMensagemServiceBus.EnviarMensagemParaFila("persistencia.agenda_medico.criar", JsonConvert.SerializeObject(listaHorariosParaCriarAgendaMedico));
            _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
            return new ResponseModel(_mensagem, true, null);
        }

        private List<(TimeOnly, TimeOnly)> GerarHorariosBaseadoNoHorarioInicialEFinal(TimeOnly horarioInicial, TimeOnly horarioFinal, int tempoConsulta, int intervalo)
        {
            var listaHorarios = new List<(TimeOnly inicio, TimeOnly fim)>();
            TimeOnly horarioAtual = horarioInicial;
            while (horarioAtual < horarioFinal)
            {
                TimeOnly proximoHorario = horarioAtual.AddMinutes(tempoConsulta);

                if (proximoHorario > horarioFinal)
                    proximoHorario = horarioFinal;

                listaHorarios.Add((horarioAtual, proximoHorario));

                horarioAtual = proximoHorario.AddMinutes(intervalo);
            }

            return listaHorarios;
        }
    }
}
