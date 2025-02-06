using FluentValidation;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;

namespace MedicalHealth.Fiap.Infraestrutura.DTO
{
    public class ConsultaSalvarDTO
    {
        public double Valor { get; private set; }
        public bool? Aceite { get; private set; }
        public string? Justificativa { get; private set; }
        public Guid AgendaMedicoId { get; private set; }

        public ConsultaSalvarDTO() { }
    }

    public class ConsultaSalvarDTOValidator : AbstractValidator<ConsultaSalvarDTO>
    {
        public ConsultaSalvarDTOValidator()
        {
            RuleFor(x => x.Valor)
                .NotEmpty()
                .WithMessage(MensagemAgenda.MENSAGEM_VALOR_NAO_PODE_SER_VAZIO)
                .NotNull()
                .WithMessage(MensagemAgenda.MENSAGEM_VALOR_NAO_PODE_SER_NULO);

            RuleFor(x => x.Justificativa)
                .NotEmpty()
                .WithMessage(MensagemAgenda.MENSAGEM_AGENDA_NAO_PODE_SER_VAZIA)
                .NotNull()
                .WithMessage(MensagemAgenda.MENSAGEM_AGENDA_NAO_PODE_SER_NULA);

        }

    }




}
