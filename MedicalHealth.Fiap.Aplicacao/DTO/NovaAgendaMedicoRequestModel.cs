using FluentValidation;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;

namespace MedicalHealth.Fiap.Aplicacao.DTO
{
    public class NovaAgendaMedicoRequestModel
    {
        public List<DateTime> Data { get; set; }
        public TimeOnly HorarioInicio { get; set; }
        public TimeOnly HorarioFim { get; set; }
        public int Intervalo { get; set; }
        public int TempoConsulta { get; set; }
        public Guid MedicoId { get; set; }
    }

    public class NovaAgendaMedicoRequestModelValidator : AbstractValidator<NovaAgendaMedicoRequestModel>
    {
        public NovaAgendaMedicoRequestModelValidator()
        {
            RuleFor(x => x.Data)
                .NotNull()
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
