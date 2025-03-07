﻿using FluentValidation;
using MedicalHealth.Fiap.Data.CacheService;
using MedicalHealth.Fiap.Data.Repository.AgendaMedico;
using MedicalHealth.Fiap.Dominio.Entidades;
using MedicalHealth.Fiap.Dominio.Interfaces;
using MedicalHealth.Fiap.Infraestrutura;
using MedicalHealth.Fiap.Infraestrutura.DTO;
using MedicalHealth.Fiap.SharedKernel.Filas;
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
        private readonly IMedicoRepository _medicoRepository;
        private int QUANTIDADE_LIMITE_DE_DIAS_FUTUROS_QUE_PODE_SER_CRIADO_UMA_AGENDA = 30;
        private readonly ICacheService _cacheService;
        public AgendaMedicoService(IAgendaMedicoRepository repository,
                                   IEnviarMensagemServiceBus enviarMensagemServiceBus,
                                   IMedicoRepository medicoRepository,
                                   ICacheService cacheService)
        {
            _repository = repository;
            _enviarMensagemServiceBus = enviarMensagemServiceBus;
            _medicoRepository = medicoRepository;
            _cacheService = cacheService;
        }
        public async Task<ResponseModel> BuscarAgendaPorData(DateOnly data)
        {
            var agendasMedico = await _repository.ObterAgendaMedicoPorData(data);

            if (agendasMedico == null || !agendasMedico.Any())
            {
                _mensagem.Add(MensagemAgenda.MENSAGEM_MEDICO_SEM_AGENDA_DISPONIVEL);
                return new ResponseModel(_mensagem, false, null);
            }

            var idMedico = agendasMedico.Select(x => x.MedicoId);

            var medico = await _medicoRepository.ObterMedicoPorListaId(idMedico);

            if (medico is not null && !medico.Any())
            {
                _mensagem.Add(MensagemMedico.MENSAGEM_MEDICO_NAO_ENCONTRADO);
                return new ResponseModel(_mensagem, false, null);
            }

            var agendaMedicoDTO = MapearParaAgendaMedicoDTO(agendasMedico, medico);

            _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
            return new ResponseModel(_mensagem, true, agendaMedicoDTO);
        }
        public async Task<bool> AtualizarAgendaDisponivel(Guid agendaMedicoId)
        {
            var dataHorarioAgenda = await _repository.ObterPorIdAsync(agendaMedicoId);

            dataHorarioAgenda.AtualizarHorarioDisponivel();

            var dataHorarioList = new List<AgendaMedico>();
            dataHorarioList.Add(dataHorarioAgenda);

            await _enviarMensagemServiceBus.EnviarMensagemParaFila(PersistenciaAgendaMedico.PERSISTENCIA_AGENDA_MEDICO_ATUALIZAR, JsonConvert.SerializeObject(dataHorarioList));
            _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
            return true;
        }
        public async Task<bool> AtualizarAgendaIndisponivel(Guid agendaMedicoId)
        {
            var dataHorarioAgenda = await _repository.ObterPorIdAsync(agendaMedicoId);

            dataHorarioAgenda.AtualizarHorarioIndisponivel();

            var dataHorarioList = new List<AgendaMedico>();
            dataHorarioList.Add(dataHorarioAgenda);

            await _enviarMensagemServiceBus.EnviarMensagemParaFila(PersistenciaAgendaMedico.PERSISTENCIA_AGENDA_MEDICO_ATUALIZAR, JsonConvert.SerializeObject(dataHorarioList));
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

            DateOnly hoje = DateOnly.FromDateTime(DateTime.Now);
            DateOnly limite = DateOnly.FromDateTime(limiteDeCriacaoDeData);

            if (novaAgendaMedico.Data.Order().First() < hoje || novaAgendaMedico.Data.Order().Last() > limite)
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

            await _enviarMensagemServiceBus.EnviarMensagemParaFila(PersistenciaAgendaMedico.PERSISTENCIA_AGENDA_MEDICO_CRIAR, JsonConvert.SerializeObject(listaHorariosParaCriarAgendaMedico));
            _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
            return new ResponseModel(_mensagem, true, null);
        }
        public async Task<ResponseModel> ApagarAgendaMedico(RemoverAgendaMedicoRequestModel removerAgenda)
        {
            var validacao = new RemoverAgendaMedicoRequestModelValidator().Validate(removerAgenda);
            if (!validacao.IsValid)
            {
                _mensagem = validacao.Errors.Select(x => x.ErrorMessage).ToList();
                return new ResponseModel(_mensagem, false, null);
            }

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

            await _enviarMensagemServiceBus.EnviarMensagemParaFila(PersistenciaAgendaMedico.PERSISTENCIA_AGENDA_MEDICO_ATUALIZAR, JsonConvert.SerializeObject(agendasMedicosExcluidas));

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
            var validacao = ValidaSeTodosOsDadosForamPreenchidos(listaAgendaMedica);

            if(!validacao)
                return new ResponseModel(_mensagem, false, null);

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

                agendaParaAtualizar.AtualizarAgendaMedico(
                                                agendaMedica.Data,
                                                agendaMedica.HorarioInicio,
                                                agendaMedica.HorarioFim,
                                                true);

                agendasParaAtualizar.Add(agendaParaAtualizar);
            }

            if (agendasParaAtualizar.Any())
            {
                await _enviarMensagemServiceBus.EnviarMensagemParaFila(PersistenciaAgendaMedico.PERSISTENCIA_AGENDA_MEDICO_ATUALIZAR, JsonConvert.SerializeObject(agendasParaAtualizar));
                _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
                return new ResponseModel(_mensagem, true, agendasParaAtualizar);
            }

            return new ResponseModel(_mensagem, false, null);

        }

        public async Task<ResponseModel> BuscarAgendaPorMedico(Guid medicoId)
        {

            var agendaMedico = await _repository.ObterAgendaPorMedico(medicoId);

            if (agendaMedico == null || !agendaMedico.Any())
            {
                _mensagem.Add(MensagemAgenda.MENSAGEM_MEDICO_SEM_AGENDA_DISPONIVEL);
                return new ResponseModel(_mensagem, false, null);
            }

            var valorConsulta = await _medicoRepository.ObterValorDaConsulta(medicoId);

            if(valorConsulta <= 0)
            {
                _mensagem.Add(MensagemAgenda.MENSAGEM_AGENDA_SEM_VALOR_DE_CONSULTA_DEFINIDO);
                return new ResponseModel(_mensagem, false, null);
            }

            var agendaMedicoDTO = MapearParaAgendaMedicoDTO(agendaMedico, valorConsulta);

            _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
            return new ResponseModel(_mensagem, true, agendaMedicoDTO);
        }
        public async Task<AgendaMedico> ObterAgendaMedicoPorId(Guid id)
        {
            var agenda = await _cacheService.GetAsync<AgendaMedico>($"agenda:{id}");

            if(agenda is null)
            {
                agenda = await _repository.ObterPorIdAsync(id);

                if(agenda is not null)
                    await _cacheService.SetAsync($"agenda:{agenda.Id}", agenda);
            }
                
            return agenda;
        }
        private IEnumerable<AgendaMedicoDTO> MapearParaAgendaMedicoDTO(IEnumerable<AgendaMedico> agendasMedico, List<Dominio.Entidades.Medico> medicos)
        {
            var agendaMedicoLista = new List<AgendaMedicoDTO>();

            foreach (var agendaMedico in agendasMedico)
            {
                var medico = medicos.FirstOrDefault(x => x.Id == agendaMedico.MedicoId);
                agendaMedicoLista.Add(new AgendaMedicoDTO
                {
                    Id = agendaMedico.Id,
                    Data = agendaMedico.Data,
                    HorarioInicio = agendaMedico.HorarioInicio,
                    HorarioFim = agendaMedico.HorarioFim,
                    Disponivel = agendaMedico.Disponivel,
                    MedicoId = agendaMedico.MedicoId,
                    ValorConsulta = medico.ValorConsulta
                });
            }

            return agendaMedicoLista;
        }
        private IEnumerable<AgendaMedicoDTO> MapearParaAgendaMedicoDTO(IEnumerable<AgendaMedico> agendasMedico, double valorConsulta)
        {
            var agendaMedicoLista = new List<AgendaMedicoDTO>();

            foreach (var agendaMedico in agendasMedico)
            {
                agendaMedicoLista.Add(new AgendaMedicoDTO
                {
                    Id = agendaMedico.Id,
                    Data = agendaMedico.Data,
                    HorarioInicio = agendaMedico.HorarioInicio,
                    HorarioFim = agendaMedico.HorarioFim,
                    Disponivel = agendaMedico.Disponivel,
                    MedicoId = agendaMedico.MedicoId,
                    ValorConsulta = valorConsulta
                });
            }

            return agendaMedicoLista;
        }
        private bool ValidaSeTodosOsDadosForamPreenchidos(ListaAtualizacoesRequestModel listaAgendaMedica)
        {
            foreach(var item in listaAgendaMedica.DataHorariosParaAtualizar)
            {
                var validacao = new AtualizarAgendaMedicoRequestModelValidator().Validate(item);
                if (!validacao.IsValid)
                {
                    _mensagem = validacao.Errors.Select(x => x.ErrorMessage).ToList();
                    return false;
                }
            }

            return true;
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
