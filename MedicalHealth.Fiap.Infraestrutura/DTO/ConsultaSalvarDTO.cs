using FluentValidation;
using MedicalHealth.Fiap.SharedKernel.Mensagens;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;

namespace MedicalHealth.Fiap.Infraestrutura.DTO
{
    public class ConsultaSalvarDTO
    {
        public double Valor { get; set; }
        public Guid AgendaMedicoId { get; set; }
        public Guid MedicoId { get; set; }

        public ConsultaSalvarDTO() { }
    }

    public class ConsultaSalvarDTOValidator : AbstractValidator<ConsultaSalvarDTO>
    {
        public ConsultaSalvarDTOValidator()
        {
            RuleFor(x => x.Valor)
                .NotEmpty()
                .WithMessage(MensagemConsulta.MENSAGEM_VALOR_NAO_PODE_SER_VAZIO)
                .NotNull()
                .WithMessage(MensagemConsulta.MENSAGEM_VALOR_NAO_PODE_SER_NULO);
        }
    }
}
