using FluentValidation;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;

namespace MedicalHealth.Fiap.Infraestrutura
{
    public class ListaAtualizacoesRequestModel
    {
        public List<AtualizarAgendaMedicoRequestModel> DataHorariosParaAtualizar {  get; set; }
    }
    public class AtualizarAgendaMedicoRequestModel
    {
        public Guid Id { get; set; }
        public DateOnly Data { get; set; }
        public TimeOnly HorarioInicio { get; set; }
        public TimeOnly HorarioFim { get; set; }
        public int Intervalo { get; set; }
        public int TempoConsulta { get; set; }
        public Guid MedicoId { get; set; }
    }

    public class AtualizarAgendaMedicoRequestModelValidator : AbstractValidator<AtualizarAgendaMedicoRequestModel>
    {
        public AtualizarAgendaMedicoRequestModelValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage(MensagemAgenda.MENSAGEM_ID_NAO_PODE_SER_NULO_OU_VAZIO);

            RuleFor(x => x.Data)
                .NotNull()
                .NotEmpty()
                .WithMessage(MensagemAgenda.MENSAGEM_DATA_NAO_PODE_SER_NULO_OU_VAZIO);

            RuleFor(x => x.HorarioInicio)
                .NotNull()
                .WithMessage(MensagemAgenda.MENSAGEM_HORARIO_INICIO_NAO_PODE_SER_NULO_OU_VAZIO)
                .NotNull()
                .WithMessage(MensagemAgenda.MENSAGEM_HORARIO_INICIO_NAO_PODE_SER_NULO_OU_VAZIO);

            RuleFor(x => x.HorarioFim)
                .NotNull()
                .WithMessage(MensagemAgenda.MENSAGEM_HORARIO_FIM_NAO_PODE_SER_NULO_OU_VAZIO)
                .NotNull()
                .WithMessage(MensagemAgenda.MENSAGEM_HORARIO_FIM_NAO_PODE_SER_NULO_OU_VAZIO);

            RuleFor(x => x.Intervalo)
                    .NotNull()
                    .WithMessage(MensagemAgenda.MENSAGEM_INTERVALO_NAO_PODE_SER_NULO);

            RuleFor(x => x.TempoConsulta)
                    .NotNull()
                    .WithMessage(MensagemAgenda.MENSAGEM_INTERVALO_NAO_PODE_SER_NULO);

            RuleFor(x => x.MedicoId)
                    .NotEmpty()
                    .WithMessage(MensagemAgenda.MENSAGEM_MEDICO_ID_NAO_PODE_SER_NULO_OU_VAZIO)
                    .NotNull()
                    .WithMessage(MensagemAgenda.MENSAGEM_MEDICO_ID_NAO_PODE_SER_NULO_OU_VAZIO);
        }
    }
}
