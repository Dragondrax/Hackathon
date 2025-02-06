using MedicalHealth.Fiap.Data.Repository.AgendaMedico;
using MedicalHealth.Fiap.Dominio.Entidades;
using MedicalHealth.Fiap.Infraestrutura;
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
        private int QUANTIDADE_LIMITE_DE_DIAS_FUTUROS_QUE_PODE_SER_CRIADO_UMA_AGENDA = 30;
        public AgendaMedicoService(IAgendaMedicoRepository repository, IEnviarMensagemServiceBus enviarMensagemServiceBus)
        {
            _repository = repository;
            _enviarMensagemServiceBus = enviarMensagemServiceBus;
        }
        public async Task<bool> AtualizarAgendaIndisponivel(Guid agendaMedicoId, Guid pacienteId, Guid consultaId)
        {
            var dataHorarioAgenda = await _repository.ObterPorIdAsync(agendaMedicoId);

            dataHorarioAgenda.AtualizarHorarioIndisponivel(pacienteId, consultaId);

            var dataHorarioList = new List<AgendaMedico>();
            dataHorarioList.Add(dataHorarioAgenda);

            await _enviarMensagemServiceBus.EnviarMensagemParaFila("persistencia.agenda_medico.atualizar", JsonConvert.SerializeObject(dataHorarioList));
            _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
            return true;
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

            var limiteDeCriacaoDeData = DateTime.Now.AddDays(QUANTIDADE_LIMITE_DE_DIAS_FUTUROS_QUE_PODE_SER_CRIADO_UMA_AGENDA);

            if (novaAgendaMedico.Data.Order().First().Date < DateTime.Now.Date || novaAgendaMedico.Data.Order().Last().Date > limiteDeCriacaoDeData.Date)
            {
                _mensagem.Add(MensagemAgenda.MENSAGEM_PERIDO_DE_CRIACAO_NAO_PODE_SER_MAIOR_QUE_30_DIAS_E_ANTES_DE_HOJE);
                return new ResponseModel(_mensagem, false, null);
            }
                

            foreach (var data in novaAgendaMedico.Data)
            {
                var agendasMedicoExistentes = await _repository.ObterAgendaMedicoPorIdMedicoDataHora(data,
                                                                                            novaAgendaMedico.HorarioInicio,
                                                                                            novaAgendaMedico.HorarioFim,
                                                                                            novaAgendaMedico.MedicoId);

                if (agendasMedicoExistentes is not null && agendasMedicoExistentes.Any())
                {
                    foreach (var agendaMedicoExistente in agendasMedicoExistentes)
                    {
                        _mensagem.Add(MensagemAgenda.MENSAGEM_AGENDA_JA_EXISTENTE);
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
        public async Task<ResponseModel> ApagarAgendaMedico(RemoverAgendaMedicoRequestModel removerAgenda)
        {
            var agendasMedicosExcluidas = new List<AgendaMedico>();
            var agendasMedicos = await _repository.ObterPorListaIdAsync(removerAgenda.Id);

            foreach(var agendaMedico in agendasMedicos)
            {
                if (!agendaMedico.Disponivel)
                {
                    _mensagem.Add($"{MensagemAgenda.MENSAGEM_DIA_HORARIO_JA_OCUPADOS_NAO_PODEM_SER_EXCLUIDOS} - Dia: {agendaMedico.Data} - Horario: {agendaMedico.HorarioInicio - agendaMedico.HorarioFim}");
                }

                agendaMedico.Excluir();
                agendasMedicosExcluidas.Add(agendaMedico);
            }

            await _enviarMensagemServiceBus.EnviarMensagemParaFila("persistencia.agenda_medico.atualizar", JsonConvert.SerializeObject(agendasMedicosExcluidas));

            if (_mensagem.Count > 0)
            {
                _mensagem.Add(MensagemAgenda.MENSAGEM_SUCESSO_PARCIAL_EXCLUSAO_HORARIOS);
                return new ResponseModel(_mensagem, true, null);
            }

            _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
            return new ResponseModel(_mensagem, true, null);
        }

        public async Task<ResponseModel> AtualizarAgendaMedico(ListaAtualizacoesRequestModel listaAgendaMedica)
        {
            var existeConflitoNaSolicitacaoInicialDoMedico = ValidaSeNaoExisteConflitoNaAtualizacaoSolicitadaPeloMedico(listaAgendaMedica);

            if(existeConflitoNaSolicitacaoInicialDoMedico)
                return new ResponseModel(_mensagem, false, null);

            var agendasParaAtualizar = new List<AgendaMedico>();
            var idsAtualizacao = listaAgendaMedica.DataHorariosParaAtualizar.Select(a => a.Id).ToHashSet();

            foreach (var agendaMedica in listaAgendaMedica.DataHorariosParaAtualizar)
            {
                var agendaParaAtualizar = await _repository.ObterPorIdAsync(agendaMedica.Id);
                if (agendaParaAtualizar == null)
                {
                    _mensagem.Add($"Agenda com Id {agendaMedica.Id} não encontrada.");
                    continue;
                }

                var agendasDoDia = (await _repository.ObterAgendaMedicoPorIdMedicoData(agendaMedica.Data, agendaMedica.MedicoId)).Where(a => !idsAtualizacao.Contains(a.Id)).ToList();

                if (agendasDoDia == null || !agendasDoDia.Any())
                {
                    _mensagem.Add($"Nenhuma agenda encontrada para o médico {agendaMedica.MedicoId} na data {agendaMedica.Data:d}.");
                    continue;
                }

                var agendaSobreposta = agendasDoDia
                    .Where(x =>(agendaMedica.HorarioInicio < x.HorarioFim && x.HorarioInicio < agendaMedica.HorarioFim)).ToList();

                if (agendaSobreposta.Any())
                {
                    _mensagem.Add($"Horário sobreposto encontrado para a agenda {agendaMedica.Id} do médico {agendaMedica.MedicoId} na data {agendaMedica.Data:d}.");
                    continue;
                }

                var resultadoAtualizacao = agendaParaAtualizar.AtualizarAgendaMedico(
                                                agendaMedica.Data,
                                                agendaMedica.HorarioInicio,
                                                agendaMedica.HorarioFim,
                                                true);
                if (resultadoAtualizacao == MensagemGenerica.MENSAGEM_SUCESSO)
                    agendasParaAtualizar.Add(agendaParaAtualizar);
                else
                    _mensagem.Add($"Falha ao atualizar a agenda {agendaMedica.Id}: {resultadoAtualizacao}");
            }

            if (agendasParaAtualizar.Any())
            {
                await _enviarMensagemServiceBus.EnviarMensagemParaFila("persistencia.agenda_medico.atualizar", JsonConvert.SerializeObject(agendasParaAtualizar));
                _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
                return new ResponseModel(_mensagem, true, agendasParaAtualizar);
            }

            return new ResponseModel(_mensagem, false, null);

        }

        private bool ValidaSeNaoExisteConflitoNaAtualizacaoSolicitadaPeloMedico(ListaAtualizacoesRequestModel listaAgendaMedica)
        {
            foreach (var grupo in listaAgendaMedica.DataHorariosParaAtualizar.GroupBy(a => new { a.MedicoId, a.Data }))
            {
                var horarios = grupo.OrderBy(a => a.HorarioInicio).ToList();
                for (int i = 1; i < horarios.Count; i++)
                {
                    var anterior = horarios[i - 1];
                    var atual = horarios[i];
                    if (atual.HorarioInicio < anterior.HorarioFim)
                    {
                        _mensagem.Add($"Sobreposição de horários detectada para o médico {grupo.Key.MedicoId} na data {grupo.Key.Data:d} entre os horários " +
                                      $"{anterior.HorarioInicio} - {anterior.HorarioFim} e {atual.HorarioInicio} - {atual.HorarioFim}.");
                        return true;
                    }
                }
            }

            return false;
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
